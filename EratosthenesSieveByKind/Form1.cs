using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EratosthenesSieveByKind
{
    public partial class Form1 : Form
    {
        const string CLASSNAME = "Form1";

        BitArray ba = null;

        int maxToProcess = 0;
        int windowStart = 0;
        int windowEnd = 0;
        int windowSize = 0;
        int windowWidth = 0;

        int xType = 1;

        bool pushFileValueLogs = false;
        bool pushGeneratedValueLogs = false;

        public Form1()
        {
            const string location = CLASSNAME + ".Form1";
            try
            {
                InitializeComponent();

                if (!L.logInit(null, lbLogs, false))
                {
                    // Nothing to do yet, normal logs failed
                    L.err(location, "Failed to initialize logs.");
                }
                L.l(location, "Application started...");

                if (!appInit())
                {
                    L.err(location, "Failed to initialize some components.");
                }

            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private bool appInit()
        {
            const string location = "App Init";
            try
            {
                tbCompareInVal.Text = "";
                numType.Value = 1;
                if (!checkAllBoxes())
                {
                    L.err(location, "Failed to check all type boxes.");
                }
                toggleRadioButtons();
                return true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return false;
        }

        public void Begin(bool reloadUi)
        {
            const string location = CLASSNAME + ".Begin";
            try
            {
                L.l(location, "Starting execution...");
                xType = (int)numType.Value;
                maxToProcess = 0;
                try
                {
                    maxToProcess = Convert.ToInt32(tbMaxToProcess.Text);
                }
                catch (Exception ex)
                {
                    L.d(location, "Max to process was not an integer.");
                }

                ba = new BitArray(maxToProcess, true);

                switch (xType)
                {

                    case 1:
                        if (isChecked(cbt1v1x1, false)) x(1, 1);
                        if (isChecked(cbt1v3x7, false)) x(1, 3);
                        if (isChecked(cbt1v7x3, false)) x(1, 7);
                        if (isChecked(cbt1v9x9, false)) x(1, 9);
                        break;
                    case 3:
                        if (isChecked(cbt3v1x3, false)) x(3, 1);
                        if (isChecked(cbt3v3x1, false)) x(3, 3);
                        if (isChecked(cbt3v7x9, false)) x(3, 7);
                        if (isChecked(cbt3v9x7, false)) x(3, 9);
                        break;
                    case 7:
                        if (isChecked(cbt7v1x7, false)) x(7, 1);
                        if (isChecked(cbt7v3x9, false)) x(7, 3);
                        if (isChecked(cbt7v7x1, false)) x(7, 7);
                        if (isChecked(cbt7v9x3, false)) x(7, 9);
                        break;
                    case 9:
                        if (isChecked(cbt9v1x9, false)) x(9, 1);
                        if (isChecked(cbt9v3x3, false)) x(9, 3);
                        if (isChecked(cbt9v7x7, false)) x(9, 7);
                        if (isChecked(cbt9v9x1, false)) x(9, 9);
                        break;
                    default:
                        break;
                }

                        
                // Works, but looks for a space delimited list of plain text primes,
                // on a certain path.
                int unfound = testOutput();
                if (unfound > 0) L.l(location, "Some items not accounted for (" + unfound + ").");
                        

                /*
                // If you want to write primes smaller than index (remember, P is (index * 10) + Kind).
                // Writes on a per-kind basis, only the kind just processed.
                if (!writeFirstXk(50000))
                {
                    L.err(location, "Failed to write first 50K for xType (" + xType + ").");
                }
                */

                if (reloadUi)
                {
                    if (!reloadDataWindow()) L.err(location, "Failed to reload data UI.");

                    if (!enableUi(true))
                    {
                        L.err(location, "Failed to reenable ui when finished.");
                    }
                }
                L.l(location, "Finsihed processing.");


                L.l(location, "Finishing starting task.");
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private int count(bool countValue)
        {
            const string location = CLASSNAME + ".count";
            int retValue = 0;
            try
            {
                if (ba == null)
                    L.err(location, "Data array was null during count.");
                else
                    for (int i = 0; i < ba.Length; i++)
                        if (ba[i] == countValue) retValue++;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retValue;
        }

        public BitArray deepCopy(ref BitArray baIn, bool defVal)
        {
            const string location = CLASSNAME + ".deepCopy";
            BitArray retVal = null;
            try
            {
                if (baIn == null) return retVal;
                retVal = new BitArray(baIn.Length, defVal);
                for (int i = 0; i < baIn.Length; i++)
                    retVal[i] = baIn[i] ? true : false;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool FormMergeList()
        {
            const string location = CLASSNAME + ".FormMergeList";
            /*
             * Executes all four Kinds, upto max index. Reads round-robbin into a merged
             * string, then outputs to file at the end. 
             */
            bool retVal = false;
            try
            {
                L.d(location, "Starting merge list.");
                if (!enableUi(false))
                {
                    L.err(location, "Failed to disable ui before beginning.");
                }
                int size = 100000;
                if (tbMaxToProcess != null)
                {
                    try { size = Convert.ToInt32(tbMaxToProcess.Text); } catch (Exception ex) { }
                }
                this.maxToProcess = size;
                tbMaxToProcess.Text = Convert.ToString(this.maxToProcess);


                Task task = new Task(new Action(() =>
                {
                    try
                    {
                        BitArray ba1 = null;
                        BitArray ba3 = null;
                        BitArray ba7 = null;
                        BitArray ba9 = null;

                        this.xType = 1;
                        if (!numericUpDown(numType, this.xType))
                        {
                            L.err(location, "Failed to update Kind before proceeding.");
                            if (!enableUi(true)) L.err(location, "Failed to enable ui on error.");
                            return;
                        }
                        Begin(false);
                        if (this.ba != null && this.ba.Length > 0)
                        {
                            ba1 = deepCopy(ref this.ba, false);
                        }
                        this.ba = null;

                        this.xType = 3;
                        if (!numericUpDown(numType, this.xType))
                        {
                            L.err(location, "Failed to update Kind before proceeding.");
                            if (!enableUi(true)) L.err(location, "Failed to enable ui on error.");
                            return;
                        }
                        Begin(false);
                        if (this.ba != null && this.ba.Length > 0)
                        {
                            ba3 = deepCopy(ref this.ba, false);
                        }
                        this.ba = null;

                        this.xType = 7;
                        if (!numericUpDown(numType, this.xType))
                        {
                            L.err(location, "Failed to update Kind before proceeding.");
                            if (!enableUi(true)) L.err(location, "Failed to enable ui on error.");
                            return;
                        }
                        Begin(false);
                        if (this.ba != null && this.ba.Length > 0)
                        {
                            ba7 = deepCopy(ref this.ba, false);
                        }
                        this.ba = null;

                        this.xType = 9;
                        if (!numericUpDown(numType, this.xType))
                        {
                            L.err(location, "Failed to update Kind before proceeding.");
                            if (!enableUi(true)) L.err(location, "Failed to enable ui on error.");
                            return;
                        }
                        Begin(true);
                        if (this.ba != null && this.ba.Length > 0)
                        {
                            ba9 = deepCopy(ref this.ba, false);
                        }
                        // leave it

                        // Validate bitarrays 1-9
                        string nullArrs = "";
                        if (ba1 == null || ba1.Length == 0) nullArrs += (nullArrs.Length == 0 ? "" : ", ") + "ba1";
                        if (ba3 == null || ba3.Length == 0) nullArrs += (nullArrs.Length == 0 ? "" : ", ") + "ba3";
                        if (ba7 == null || ba7.Length == 0) nullArrs += (nullArrs.Length == 0 ? "" : ", ") + "ba7";
                        if (ba9 == null || ba9.Length == 0) nullArrs += (nullArrs.Length == 0 ? "" : ", ") + "ba9";
                        if (nullArrs.Length > 0)
                        {
                            L.err(location, "Some xType arrays were null (" + nullArrs + ").");
                            if (!enableUi(true)) L.err(location, "Failed to enable ui on error.");
                            return;// Early Exit
                        }

                        StringBuilder sb = new StringBuilder();
                        for (int idxOuter = 0; idxOuter < size; idxOuter++)
                        {
                            for (int idxArr = 0; idxArr < 4; idxArr++)
                            {
                                switch (idxArr)
                                {
                                    case 0: //1
                                        if (ba1[idxOuter] == true)
                                        {
                                            if (sb.Length > 0) sb.Append(" ");// To match test file, space delimit.
                                            sb.Append((idxOuter * 10) + 1);
                                        }
                                        break;
                                    case 1: //3
                                        if (ba3[idxOuter] == true)
                                        {
                                            if (sb.Length > 0) sb.Append(" ");
                                            sb.Append((idxOuter * 10) + 3);
                                        }
                                        break;
                                    case 2: //7
                                        if (ba7[idxOuter] == true)
                                        {
                                            if (sb.Length > 0) sb.Append(" ");
                                            sb.Append((idxOuter * 10) + 7);
                                        }
                                        break;
                                    case 3: //9
                                        if (ba9[idxOuter] == true)
                                        {
                                            if (sb.Length > 0) sb.Append(" ");
                                            sb.Append((idxOuter * 10) + 9);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        // use tag, overwrite file
                        string fileName = "./" + "MergeList_" + Convert.ToString(size * 10) + ".txt";
                        try
                        {
                            File.WriteAllText(fileName, sb.ToString());
                        }
                        catch (Exception exFileWrite)
                        {
                            L.err(location, "Failed to write file (" + fileName + ") with error: " + exFileWrite.Message);
                        }
                        sb.Length = 0;
                        sb = null;
                    }
                    catch (Exception exTask)
                    {
                        L.err(location, "Task error: " + exTask.Message);
                        try
                        {
                            if (!enableUi(true)) L.err(location, "Failed to enable ui on error.");
                        }
                        catch (Exception exUi) { }
                    }
                }));
                task.Start();

                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        private bool reloadDataWindow()
        {
            const string location = CLASSNAME + ".reloadDataWindow";
            bool retVal = false;
            try
            {
                if (!clearDataWindow())
                {
                    L.err(location, "Failed to clear data window before beginning.");
                }

                // Set up our data-view size
                if (!getWindowSettings())
                {
                    L.err(location, "Failed to get window settings before updating ui.");
                }

                // Push data to view
                // Build UI header
                List<string> uiStrings = new List<string>();
                StringBuilder sb = new StringBuilder();
                sb.Append("( " + "            " + " )");// 12 spaces
                for (int i = 0; i < windowWidth; i++)
                {
                    if (i < 10) sb.Append(i.ToString().PadLeft(4));
                    else if (i < 100) sb.Append(i.ToString().PadLeft(4));
                    else sb.Append(i.ToString().PadLeft(4));
                }
                uiStrings.Add(sb.ToString());
                sb.Clear();

                for (int i = 0; i < windowWidth; i++) sb.Append("---:");
                uiStrings.Add("-----------------:" + sb.ToString());
                sb.Clear();

                // Fill in UI data
                string formatted = "";
                int pos = windowStart;
                for (; pos < windowEnd; pos++)
                {
                    if (sb.Length > 0) sb.Append(" : ");
                    sb.Append(ba[pos] == true ? "#" : " ");

                    int rowNum = 0;
                    if ((pos + 1) % windowWidth == 0 && sb.Length > 0)
                    {
                        formatted = "( " + ((((int)(pos + 1 / windowWidth) | 0) * 10) + xType).ToString().PadLeft(12) + " ) - " + sb.ToString();
                        rowNum++;
                        uiStrings.Add(formatted);
                        sb.Clear();
                    }
                }
                if (sb.Length > 0)
                {
                    pos++;
                    formatted = "( " + ((((int)(pos + 1 / windowWidth) | 0) * 10) + xType).ToString().PadLeft(12) + " ) - " + sb.ToString();
                    uiStrings.Add(formatted);
                }
                sb.Clear();

                if (!updateDataWindow(uiStrings))
                {
                    L.err(location, "Failed to update UI with output data.");
                }
                else retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        private bool sieve(ref BitArray ba, int startOffset, int increment, bool value)
        {
            // This core function simply maps a region of bits. It sets the start bit
            // to value, and proceeds by increment setting value, until end of data is 
            // reached. 
            const string location = CLASSNAME + ".sieve";
            bool retValue = false;
            try
            {
                /*
                L.l(location, "Starting sieve - Value (" + value + "), " +
                    "Start Offset (" + startOffset + "), " +
                    "End Offset (" + endOffset + "), " +
                    "Incremet (" + increment + "), " +
                    "Length (" + ba.Length + ").");
                */

                int cntr = 0;
                int cntIterations = 0;
                for (int i = startOffset;
                    i < ba.Length;
                    i += increment
                    )
                {
                    cntIterations++;
                    ba[i] = value;

                    if (cntr == 10000)
                    {
                        L.d(location, "Performed iteration (" + cntIterations + ").");
                        cntr = 0;
                    }
                }
                // consider returning count
                retValue = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retValue;
        }

        private int testOutput()
        {
            const string location = CLASSNAME + ".testOutput";
            int retValue = -1;
            try
            {
                // Read a file of known values, filter, and compare
                string path = textBoxText(tbCompareInVal);
                if (path == null || path.Length == 0)
                {
                    //L.err(location, "Path to compare file was null or empty.");
                    return retValue;
                }
                if (!File.Exists(path))
                {
                    L.err(location, "Compare file of known values does not exist. Please enter a valid path.");
                    return retValue;
                }

                List<string> vals = new List<string>();
                using (FileStream fs = new FileStream(path, FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        string[] temp = line.Split(' ');
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if (temp[i] != null && temp[i].Length > 0)
                            {
                                vals.Add(temp[i]);
                            }
                        }
                    }
                }

                L.l(location, "Copied (" + vals.Count + ") values to memory from file:" + path);

                // Be wasteful and sort again. Create four buckets for numbers ending
                // in 1, 3, 7, or 9. Use lists for unknown counts.

                List<int> ones = new List<int>();
                List<int> threes = new List<int>();
                List<int> sevens = new List<int>();
                List<int> nines = new List<int>();

                int cntErrors = 0;
                int cnt = 0;
                foreach (string s in vals)
                {
                    if (s == null || s.Length == 0) continue;

                    try
                    {
                        int val = Convert.ToInt32(s);

                        // see what kind of number it is
                        int remainder = val % 10;
                        switch (remainder)
                        {
                            case 1: ones.Add(val); break;
                            case 3: threes.Add(val); break;
                            case 7: sevens.Add(val); break;
                            case 9: nines.Add(val); break;
                            default:
                                L.d(location, "Non-standard value (" + val + ").");//2,5
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        cntErrors++;
                    }
                    cnt++;
                }
                if (cntErrors > 0)
                {
                    L.err(location, "Encountered (" + cntErrors + ") conversion errors.");
                }

                L.l(location, "Separated counts from file: K1 (" + ones.Count + "), K3 (" + threes.Count + 
                    "), K7 (" + sevens.Count + "), K9 (" + nines.Count + ").");


                List<int> myReference =
                    xType == 1 ? ones :
                    xType == 3 ? threes :
                    xType == 7 ? sevens :
                    xType == 9 ? nines :
                    new List<int>();


                // Push type compare list from file to logs
                int cntLog = 0;
                if (pushFileValueLogs)
                {
                    StringBuilder sbFile = new StringBuilder();
                    for (int i = 0; i < myReference.Count; i++, cntLog++)
                    {
                        sbFile.Append(sbFile.Length == 0 ? "" : "   ").Append(myReference[i]);
                        if (cntLog >= 10)
                        {
                            L.d(location, "Known from file: " + sbFile.ToString());
                            sbFile.Clear();
                            cntLog = 0;
                        }
                    }
                    L.d(location, "Known from file: " + sbFile.ToString());
                    sbFile = null;
                }

                // Iterate collection, convert bits to numbers, compare
                if (ba == null)
                {
                    L.err(location, "Data array is null. Skipping compare.");
                    return retValue;
                }

                L.d(location, "Beginning compare against known values.");

                List<string> failedValues = new List<string>();
                int cntMatches = 0;
                int num = xType;// number form always starts as Type (1,3,7,9)
                int cntFalse = 0;
                int idxMatch = -1;
                List<string> matchedValues = new List<string>();
                for (int i = 0; i < ba.Length && num <= 10000; i++, num += 10, idxMatch = -1)// add 10 to digit per place
                {
                    if (ba[i] == false)
                    {
                        cntFalse++;
                        continue;
                    }
                    idxMatch =
                        xType == 1 ? ones.IndexOf(num) :
                        xType == 3 ? threes.IndexOf(num) :
                        xType == 7 ? sevens.IndexOf(num) :
                        xType == 9 ? nines.IndexOf(num) :
                        -1;

                    if (idxMatch < 0) failedValues.Add(num.ToString());
                    else
                    {
                        cntMatches++;
                        matchedValues.Add(num.ToString());
                    }
                }

                List<string> unmatchedValues = new List<string>();
                for (int index = 0; index < myReference.Count; index++)
                {
                    int idx = (int)((myReference[index] / 10) | 0);
                    if (idx < ba.Length && ba[idx] != true)
                        unmatchedValues.Add(myReference[index].ToString());
                }

                L.l(location, "Found (" + cntFalse + ") false values.");
                L.l(location, "Found (" + cntMatches + ") matches.");
                string sFailedVals = "";
                for (int i = 0; i < failedValues.Count; i++)
                    sFailedVals += (sFailedVals.Length > 0 ? ", " : "") + failedValues[i];
                if (sFailedVals.Length > 0)
                {
                    L.err(location, "Failed to locate matches for values: " + sFailedVals);
                }
                L.l(location, "Failure count (" + failedValues.Count + ").");

                if (pushGeneratedValueLogs)
                {
                    string sMatchVals = "";
                    for (int i = 0; i < matchedValues.Count; i++)
                    {
                        sMatchVals += (sMatchVals.Length > 0 ? ", " : "") + matchedValues[i];
                        if (i % 10 == 0)
                        {
                            L.l(location, "Match Values: " + sMatchVals);
                            sMatchVals = "";
                        }
                    }
                    L.d(location, "Match Values: " + sMatchVals);
                }
                L.d(location, "Generated values not found in reference set (" + unmatchedValues.Count + ").");

                string sMissingValues = "";
                for (int i = 0; i < unmatchedValues.Count; i++)
                {
                    sMissingValues = (sMissingValues.Length > 0 ? ", " : "") + unmatchedValues[i];
                    if (i % 10 == 10)
                    {
                        L.err(location, "Missing Values: " + sMissingValues);
                    }
                }
                if (sMissingValues.Length > 0) L.err(location, "Missing Values: " + sMissingValues);
                else L.l(location, "Missing Values: " + sMissingValues);

                L.d(location, "Count (K" + xType + "), False values (" + count(false) +
                    "), True (" + count(true) + ")");


                retValue = cntErrors;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retValue;
        }

        private bool updateDataWindow(List<string> data)
        {
            const string location = CLASSNAME + ".updateDataWindow";
            bool retVal = false;
            try
            {
                if (lbData == null) return retVal;
                if (data == null) return retVal;
                if (data.Count == 0) return true;
                if (lbData.InvokeRequired)
                {
                    lbData.Invoke(new Action(() => { retVal = updateDataWindow(data); }));
                }
                else 
                {
                    for (int i = 0; i < data.Count; i++)
                        lbData.Items.Add(data[i]);
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool writeFirstXk(int writeIdxSmallerThan)
        {
            const string location = "WriteFirstXK";
            bool returnVal = false;
            try
            {
                // use a string builder and don't worry about it. this is not meant for >10M
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < writeIdxSmallerThan && i < this.ba.Length; i++)
                {
                    if (this.ba[i] == true)
                    {
                        if (sb.Length > 0) sb.Append(",");
                        sb.Append((i * 10) + xType);
                    }
                }

                // use location tag, overwrite file
                string fileName = "./" + location + Convert.ToString(xType) + ".txt";
                using (FileStream fs = File.Create(fileName))
                using (BinaryWriter fw = new BinaryWriter(fs))
                {
                    try
                    {
                        fw.Write(sb.ToString());
                        returnVal = true;
                    }
                    catch (Exception exWrite)
                    {
                        L.err(location, "Failed to write file (" + fileName + ") with error: " + exWrite.Message);
                    }
                }
                sb.Length = 0;
                sb = null;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return returnVal;
        }

        private void x(int typeBase, int typeFor)
        {
            const string location = CLASSNAME + ".x";
            try
            {
                int vala = 0;
                int valb = 0;

                int amax = ba.Length / typeFor;

                switch (typeBase)
                {
                    // the value 1 is always rolled by 10
                    // 1xp is still p, leading to p being accidentally flagged
                    case 1:
                        switch (typeFor)
                        {
                            case 1:
                                vala = 11;
                                valb = 11;
                                break;
                            case 3:
                                vala = 3;
                                valb = 7;
                                break;
                            case 7:
                                vala = 7;
                                valb = 3;
                                break;
                            case 9:
                                vala = 9;
                                valb = 9;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        switch (typeFor)
                        {
                            case 1:
                                vala = 11;
                                valb = 3;
                                break;
                            case 3:
                                vala = 3;
                                valb = 11;
                                break;
                            case 7:
                                vala = 7;
                                valb = 9;
                                break;
                            case 9:
                                vala = 9;
                                valb = 7;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 7:
                        switch (typeFor)
                        {
                            case 1:
                                vala = 11;
                                valb = 7;
                                break;
                            case 3:
                                vala = 3;
                                valb = 9;
                                break;
                            case 7:
                                vala = 7;
                                valb = 11;
                                break;
                            case 9:
                                vala = 9;
                                valb = 3;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 9:
                        switch (typeFor)
                        {
                            case 1:
                                vala = 11;
                                valb = 9;
                                break;
                            case 3:
                                vala = 3;
                                valb = 3;
                                break;
                            case 7:
                                vala = 7;
                                valb = 7;
                                break;
                            case 9:
                                vala = 9;
                                valb = 11;
                                break;
                            default:
                                break;
                        }
                        break;
                }

                L.l(location, "Processing K" + xType + " " + (vala == 11 ? "1" : vala.ToString()) + "x" + (valb == 11 ? "1" : valb.ToString()) + ".");


                if (vala == 1) vala = 11;// bump 1 to 11 automatically

                for (int i = vala; i < ba.Length; i += 10)
                {
                    for (int b = valb; b < (ba.Length / i); b += 10)
                    {
                        int increment = i;
                        int startOffset = (i * b) / 10 | 0;

                        if (!sieve(ref ba, startOffset, increment, false))
                        {
                            L.l(location, "Failed to walk increment (" + increment + ") at offset (" + startOffset + ").");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }



        // UI Helpers

        private bool checkAllBoxes()
        {
            bool retVal = false;
            try
            {
                if (cbt1v1x1.InvokeRequired)
                {
                    cbt1v1x1.Invoke(new Action(() => { retVal = checkAllBoxes(); }));
                }
                else
                {
                    cbt1v1x1.Checked = true;
                    cbt1v3x7.Checked = true;
                    cbt1v7x3.Checked = true;
                    cbt1v9x9.Checked = true;
                    cbt3v1x3.Checked = true;
                    cbt3v3x1.Checked = true;
                    cbt3v7x9.Checked = true;
                    cbt3v9x7.Checked = true;
                    cbt7v1x7.Checked = true;
                    cbt7v3x9.Checked = true;
                    cbt7v7x1.Checked = true;
                    cbt7v9x3.Checked = true;
                    cbt9v1x9.Checked = true;
                    cbt9v3x3.Checked = true;
                    cbt9v7x7.Checked = true;
                    cbt9v9x1.Checked = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                L.ex(CLASSNAME + ".checkAllBoxes", ex);
            }
            return false;
        }

        private bool checkbox(CheckBox cb, bool isChecked)
        {
            const string location = CLASSNAME + ".checkbox";
            bool retVal = false;
            try
            {
                if (cb == null) return retVal;
                if (cb.InvokeRequired)
                {
                    cb.Invoke(new Action(() => { retVal = checkbox(cb, isChecked); }));
                }
                else 
                {
                    cb.Checked = isChecked;
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        private bool clearDataWindow()
        {
            const string location = CLASSNAME + ".clearDataWindow";
            bool retVal = false;
            try
            {
                if (lbData.InvokeRequired)
                {
                    lbData.Invoke(new Action(() => { retVal = clearDataWindow(); }));
                }
                else
                {
                    // Clear our existing window
                    lbData.Items.Clear();
                    lbData.Invalidate();
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        private bool enableUi(bool enabled)
        {
            const string location = CLASSNAME + ".enableUi";
            bool retVal = false;
            try
            {
                if (btnGo.InvokeRequired)
                {
                    btnGo.Invoke(new Action(() => { retVal = enableUi(enabled); }));
                }
                else
                {
                    tbMaxToProcess.Enabled = enabled;
                    tbWindowSize.Enabled = enabled;
                    tbWindowStart.Enabled = enabled;
                    tbWindowWidth.Enabled = enabled;
                    btnClearData.Enabled = enabled;
                    btnGo.Enabled = enabled;
                    btnMergeList.Enabled = enabled;
                    numType.Enabled = enabled;

                    cbt1v1x1.Enabled = enabled;
                    cbt1v3x7.Enabled = enabled;
                    cbt1v7x3.Enabled = enabled;
                    cbt1v9x9.Enabled = enabled;
                    cbt3v1x3.Enabled = enabled;
                    cbt3v3x1.Enabled = enabled;
                    cbt3v7x9.Enabled = enabled;
                    cbt3v9x7.Enabled = enabled;
                    cbt7v1x7.Enabled = enabled;
                    cbt7v3x9.Enabled = enabled;
                    cbt7v7x1.Enabled = enabled;
                    cbt7v9x3.Enabled = enabled;
                    cbt9v1x9.Enabled = enabled;
                    cbt9v3x3.Enabled = enabled;
                    cbt9v7x7.Enabled = enabled;
                    cbt9v9x1.Enabled = enabled;

                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        private bool getWindowSettings()
        {
            const string location = CLASSNAME + ".getWindowSettings";
            bool retVal = false;
            try
            {
                if (tbWindowSize.InvokeRequired)
                {
                    tbWindowSize.Invoke(new Action(() => { retVal = getWindowSettings(); }));
                }
                else
                {
                    // Set up our data-view size - TODO - Relocate
                    windowSize = 100000;
                    windowStart = 000000;
                    try
                    {
                        windowSize = Convert.ToInt32(tbWindowSize.Text);
                        windowStart = Convert.ToInt32(tbWindowStart.Text);
                    }
                    catch (Exception ex) { }
                    if (windowSize >= ba.Length) windowSize = ba.Length - 1;
                    if (windowStart >= ba.Length) windowStart = 0;

                    windowEnd = (windowStart + windowSize - 1 < ba.Length ?
                        windowStart + windowSize - 1 : ba.Length - 1);
                    windowWidth = 100;
                    try
                    {
                        windowWidth = Convert.ToInt32(tbWindowWidth.Text);
                    }
                    catch (Exception ex) { }
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        private bool isChecked(CheckBox cb, bool defaultVal)
        {
            bool retVal = defaultVal;
            try
            {
                if (cb == null) return retVal;
                if (cb.InvokeRequired)
                {
                    cb.Invoke(new Action(() => { retVal = isChecked(cb, defaultVal); }));
                }
                else 
                {
                    retVal = cb.Checked;
                }
            }
            catch (Exception ex)
            {
                L.ex(CLASSNAME + ".isChecked", ex);
            }
            return retVal;
        }

        private bool numericUpDown(NumericUpDown numUpDown, int value)
        {
            const string location = CLASSNAME + ".numericUpDown";
            bool retVal = false;
            try
            {
                if (numUpDown == null) return retVal;
                if (numUpDown.InvokeRequired)
                {
                    numUpDown.Invoke(new Action(() => { retVal = numericUpDown(numUpDown, value); }));
                }
                else 
                {
                    if (value < numUpDown.Minimum || value > numUpDown.Maximum)
                    {
                        L.err(location, "Input value (" + value + ") was out of range (" + 
                            numUpDown.Minimum + "-" + numUpDown.Maximum + ").");
                    }
                    else 
                    {
                        numUpDown.Value = value;
                        retVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        private string textBoxText(TextBox tb)
        {
            const string location = CLASSNAME + ".textBoxText";
            string retVal = "";
            try
            {
                if (tb == null) return retVal;
                if (tb.InvokeRequired)
                {
                    tb.Invoke(new Action(() => { retVal = textBoxText(tb); }));
                }
                else 
                {
                    retVal = tb.Text;
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        private bool toggleRadioButtons()
        {
            const string location = CLASSNAME + ".toggleRadioButtons";
            bool retVal = false;
            try
            {
                if (numType.InvokeRequired)
                {
                    numType.Invoke(new Action(() => { retVal = toggleRadioButtons(); }));
                }
                else
                {
                    if (!(xType == 1 || xType == 3 || xType == 7 || xType == 9))
                    {
                        numType.Value = 1;
                        xType = (int)numType.Value;
                    }
                    if (!checkAllBoxes()) L.err(location, "Failed to check all boxes.");

                    switch (xType)
                    {
                        case 1:
                            cbt1v1x1.Visible = true;
                            cbt1v3x7.Visible = true;
                            cbt1v7x3.Visible = true;
                            cbt1v9x9.Visible = true;

                            cbt3v1x3.Visible = false;
                            cbt3v3x1.Visible = false;
                            cbt3v7x9.Visible = false;
                            cbt3v9x7.Visible = false;
                            cbt7v1x7.Visible = false;
                            cbt7v3x9.Visible = false;
                            cbt7v7x1.Visible = false;
                            cbt7v9x3.Visible = false;
                            cbt9v1x9.Visible = false;
                            cbt9v3x3.Visible = false;
                            cbt9v7x7.Visible = false;
                            cbt9v9x1.Visible = false;
                            retVal = true;
                            break;
                        case 3:
                            cbt1v1x1.Visible = false;
                            cbt1v3x7.Visible = false;
                            cbt1v7x3.Visible = false;
                            cbt1v9x9.Visible = false;
                            cbt3v1x3.Visible = true;
                            cbt3v3x1.Visible = true;
                            cbt3v7x9.Visible = true;
                            cbt3v9x7.Visible = true;
                            cbt7v1x7.Visible = false;
                            cbt7v3x9.Visible = false;
                            cbt7v7x1.Visible = false;
                            cbt7v9x3.Visible = false;
                            cbt9v1x9.Visible = false;
                            cbt9v3x3.Visible = false;
                            cbt9v7x7.Visible = false;
                            cbt9v9x1.Visible = false;
                            retVal = true;
                            break;
                        case 7:
                            cbt1v1x1.Visible = false;
                            cbt1v3x7.Visible = false;
                            cbt1v7x3.Visible = false;
                            cbt1v9x9.Visible = false;
                            cbt3v1x3.Visible = false;
                            cbt3v3x1.Visible = false;
                            cbt3v7x9.Visible = false;
                            cbt3v9x7.Visible = false;
                            cbt7v1x7.Visible = true;
                            cbt7v3x9.Visible = true;
                            cbt7v7x1.Visible = true;
                            cbt7v9x3.Visible = true;
                            cbt9v1x9.Visible = false;
                            cbt9v3x3.Visible = false;
                            cbt9v7x7.Visible = false;
                            cbt9v9x1.Visible = false;
                            retVal = true;
                            break;
                        case 9:
                            cbt1v1x1.Visible = false;
                            cbt1v3x7.Visible = false;
                            cbt1v7x3.Visible = false;
                            cbt1v9x9.Visible = false;
                            cbt3v1x3.Visible = false;
                            cbt3v3x1.Visible = false;
                            cbt3v7x9.Visible = false;
                            cbt3v9x7.Visible = false;
                            cbt7v1x7.Visible = false;
                            cbt7v3x9.Visible = false;
                            cbt7v7x1.Visible = false;
                            cbt7v9x3.Visible = false;
                            cbt9v1x9.Visible = true;
                            cbt9v3x3.Visible = true;
                            cbt9v7x7.Visible = true;
                            cbt9v9x1.Visible = true;
                            retVal = true;
                            break;
                        default:
                            break;
                    }

                    gbRuleSet.Invalidate();
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }



        // UI Events

        private void btnClearLogs_MouseClick(object sender, MouseEventArgs e)
        {
            const string location = CLASSNAME + ".btnClearLogs_MouseClick";
            try
            {
                L.l(location, "Clearing logs from button click.");
                long length = L.clearLogs();
                L.l(location, "Cleared (" + length + ") log length.");
            }
            catch (Exception ex)
            {
                L.ex(CLASSNAME + ".btnClearLogs_MouseClick", ex);
            }
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            try
            {
                lbData.Items.Clear();
            }
            catch (Exception ex)
            {
                L.ex(CLASSNAME + ".btnClearData_Click", ex);
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            const string location = CLASSNAME + ".btnGo_Click";
            try
            {
                L.l(location, "Start processing..");

                Task task = new Task(new Action(() =>
                {
                    try
                    {
                        if (!enableUi(false))
                        {
                            L.err(location, "Failed to disable ui before starting.");
                        }
                        Begin(true);
                    }
                    catch (Exception exTask)
                    {
                        L.err(location, "Task error: " + exTask.Message);
                        try
                        {
                            if (!enableUi(true)) L.err(location, "Failed to enable ui on error.");
                        }
                        catch (Exception exUi) { }
                    }
                }));
                task.Start();
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
                try
                {
                    if (!enableUi(true)) L.err(location, "Failed to enable ui on error.");
                }
                catch (Exception exUi) { }
            }
        }

        private void btnMergeList_Click(object sender, EventArgs e)
        {
            const string location = CLASSNAME + ".btnMergeList_Click";
            try
            {
                if (!FormMergeList())
                {
                    L.err(location, "Failed to build a merged list");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private void cbDebugLogs_CheckedChanged(object sender, EventArgs e)
        {
            const string location = CLASSNAME + ".cbDebugLogs_CheckedChanged";
            try
            {
                L.isDebug = cbDebugLogs.Checked;
                L.l(location, (L.isDebug ? "Enabled" : "Disabled") + " debug logging.");
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string location = CLASSNAME + ".exitToolStripMenuItem_Click";
            try
            {
                L.l(location, "App is exiting from menu item.");
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private void numType_ValueChanged(object sender, EventArgs e)
        {
            const string location = CLASSNAME + ".numType_ValueChanged";
            try
            {
                if (numType.Value == 5)
                    numType.Value = (xType > 5) ? 3 : 7;
                xType = (int)numType.Value;
                if (!toggleRadioButtons())
                {
                    L.err(location, "Failed to toggle radio buttons.");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }
    }

}
