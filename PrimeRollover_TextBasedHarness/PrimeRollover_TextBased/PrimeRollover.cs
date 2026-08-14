//Prime Rollover - Text Based Harness
//(c) 2026 - TimeWarpToaster

//https://www.gnu.org/licenses/gpl-3.0.html

using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeRollover_Example
{
    public class PrimeRollover
    {
        public const string CLASSNAME = "PrimeRollover";

        public enum Directions 
        {
            INCR,
            DECR
        }


        public int increment { get; set; }
        public int offset { get; set; }
        public int startOffset { get; set; }
        public int p { get; set; }
        public List<int> values { get; set; }
        public PrimeRollover.Directions direction { get; set; }

        private Random r { get; set; }

        public bool isRollover = false; // rollover forever?
        public bool isRolloverIncrement = false; // change increments at rollover?


        public long cntIncrementChange = 0;
        public long cntIteration = 0;
        public long cntIterationDirection = 0;
        public long cntDirectionChange = 0;
        public long cntPChange = 0;



        public PrimeRollover()
        {
            const string location = CLASSNAME + ".Constructor";
            try
            {
                if (!this.init(1, 0, -1))
                {
                    L.err(location, "Failed to initialize object.");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        public bool init(int inIncrement, int inOffset, int inP)
        {
            const string location = CLASSNAME + ".init";
            bool retVal = false;
            try
            {
                this.increment = inIncrement;
                this.offset = inOffset;
                this.startOffset = -1;// Wait for list to init start
                this.p = inP;
                this.values = new List<int>();
                this.direction = PrimeRollover.Directions.INCR;

                if (this.offset < 0 && this.p > 0)
                {
                    this.offset = 0;
                }
                if (this.increment < 1 && this.p > 0)
                {
                    this.increment = 1;// Increment cannot be zero or infinite loop
                }

                this.r = new Random();
                this.r.Next(this.r.Next(this.r.Next()));

                // Flag result for completing
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool decr()
        {
            const string location = CLASSNAME + ".decr";
            bool retVal = false;
            try
            {
                if (this.direction != PrimeRollover.Directions.DECR)
                {
                    this.direction = PrimeRollover.Directions.DECR;
                    this.cntDirectionChange++;
                    this.cntIterationDirection = 0;
                }
                if (this.values != null)
                {
                    int tOffset = this.offset;
                    if (tOffset - increment < 0)
                    {
                        tOffset += this.values.Count;
                    }
                    tOffset -= increment;
                    if (tOffset >= 0 && tOffset < this.values.Count)
                    {
                        this.offset = tOffset;
                        this.cntIteration++;
                        retVal = true;
                    }

                    // TODO - Evaluate offset against start, determine rollover status

                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool incr()
        {
            const string location = CLASSNAME + "incr";
            bool retVal = false;
            try
            {
                if (this.direction != PrimeRollover.Directions.INCR)
                {
                    this.direction = PrimeRollover.Directions.INCR;
                    this.cntDirectionChange++;
                    this.cntIteration = 0;
                }
                if (this.values != null)
                {
                    int tOffset = this.offset;
                    if (tOffset + this.increment >= this.values.Count)
                    {
                        tOffset -= this.values.Count;
                    }
                    tOffset += this.increment;
                    if (tOffset >= 0 && tOffset < this.values.Count)
                    {
                        this.offset = tOffset;
                        this.cntIteration++;
                        retVal = true;
                    }

                    // TODO - Evaluate offset against start, determine rollover status

                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public int next()
        {
            const string location = CLASSNAME + ".next";
            int retVal = -1;
            try
            {
                if (this.direction == PrimeRollover.Directions.INCR)
                {
                    if (this.incr()) retVal = this.values[this.offset];
                }
                else if (this.direction == PrimeRollover.Directions.DECR)
                {
                    if (this.decr()) retVal = this.values[this.offset];
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public string formatSeriesForUi(List<int> series)
        {
            const string location = CLASSNAME + ".formatSeriesForUi";
            string retVal = "";
            try
            {
                if (series == null || series.Count == 0)
                {
                    L.err(location, "Input series was null or empty.");
                    return retVal;
                }

                StringBuilder sb = new StringBuilder();

                int idxTerm = series.Count - 1;
                for (int i = 0; i < series.Count; i++)
                {
                    if (i % 10 == 0) sb.Append("\n");
                    sb.Append(series[i]).Append(i == idxTerm ? "." : ", ");
                }

                // Output Result
                retVal = sb.ToString();
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        // Retrieve all items stored, uniquely, according to current increment,
        // forward direction, starting from the current offset.
        public List<int> getCurrentSeries()
        {
            const string location = CLASSNAME + ".getCurrentSeries";
            List<int> retVal = new List<int>();
            try
            {
                // TODO - Add validation method for class object


                int startOffset = this.offset;
                List<int> temp = new List<int>();
                for (int i = startOffset; i < this.values.Count; i++)
                {
                    temp.Add(this.values[i]);

                    if (!this.incr())
                    {
                        int origPosition = this.offset - this.increment;
                        if (origPosition < 0) origPosition += this.values.Count;
                        L.err(location, "Failed to increment by (" + this.increment + ") from (" + origPosition + 
                            ") out of (" + this.values.Count + ").");
                        return retVal;// hard error
                    }
                }
                retVal = temp;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        // getAllSeries - A special case. Increments and offsets are internalized,
        // because this outputs every possible combination supported by a prime.
        public Dictionary<int, List<int>> getAllSeries()
        {
            const string location = CLASSNAME + ".getAllSeries";
            Dictionary<int, List<int>> retVal = new Dictionary<int, List<int>>();
            try
            {
                Dictionary<int, List<int>> temp = new Dictionary<int, List<int>>();

                // Create outer storage
                // Increments supported are 1 -> p-1, 0 and P are infinite loops
                for (int i = 1; i < this.p - 1; i++)
                {
                    temp.Add(i, new List<int>());
                }

                // Iterate all increments, getting lists to output
                // Start at offset=0 (sample code), regardless of increment

                for (int tIncrement = 1; tIncrement < this.p - 1; tIncrement++)
                {
                    int startOffset = 0;
                    List<int> series = new List<int>();
                    for (int i = 0, tOffset = startOffset; /* tOffset is managed */; i++)
                    {
                        if (tOffset == startOffset && i > 0) break;// Failsafe, we completed series
                        if (tOffset < 0 || tOffset >= this.values.Count)
                        {
                            L.err(location, "Offset (" + tOffset + ") was out of bounds(0-" + (this.values.Count - 1) + ").");
                            return retVal;// hard error
                        }

                        series.Add(this.values[tOffset]);
                        tOffset += tIncrement;
                        if (tOffset >= this.values.Count)
                        {
                            tOffset -= this.values.Count;
                        }
                    }

                    // Add series to output
                    temp[tIncrement] = series;
                }

                // Output Result
                retVal = temp;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }


        // loadValues - Pushes (p) sequential numbers into this.values for junk data
        public bool loadValues()
        {
            const string location = CLASSNAME + ".loadValues";
            bool retVal = false;
            try
            {
                // This was probably done by caller, but *should* be done here (too)
                this.values = new List<int>();

                if (this.p <= 0)
                {
                    L.err(location, "Prime not set when attempting to load sample values.");
                    return retVal;
                }

                for (int i = 0; i < p; i++)
                {
                    this.values.Add(i);
                }

                if (this.offset >= this.values.Count)
                {
                    this.offset = r.Next(0, this.values.Count - 1);
                }
                if (this.increment >= this.values.Count)
                {
                    this.increment = r.Next(1, this.values.Count - 1);
                }

                retVal = this.values.Count == this.p;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        // loadValues(ls) - Does not verify P-number of elements. Updates all non-count settings,
        // to comply with new data, including P. If you want counts cleared, call init() first.
        public bool loadValues(List<int> inValues)
        {
            const string location = CLASSNAME + ".loadValues(ls)";
            bool retVal = false;
            try
            {
                if (inValues == null || inValues.Count == 0)
                {
                    L.err(location, "Input values were null or empty.");
                    return retVal;
                }

                // Update P and values
                this.p = inValues.Count;
                this.values = new List<int>();
                this.values.AddRange(inValues);

                // Set offset and increment randomly within range
                if (this.offset >= this.values.Count)
                {
                    this.offset = r.Next(0, this.values.Count - 1);
                }
                if (this.increment >= this.values.Count)
                {
                    this.increment = r.Next(1, this.values.Count - 1);
                }

                // Flag success for completing
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool rolloverIncrement(bool useRandomIncrement) 
        {
            const string location = CLASSNAME + ".rolloverIncrement";
            bool retVal = false;
            try
            {
                // Validate
                if (this.values == null || this.values.Count == 0)
                {
                    L.err(location, "Values was null or empty when selecting random increment.");
                    return retVal;
                }
                if (this.p != this.values.Count)
                {
                    L.err(location, "Mismatch between prime (" + this.p + 
                        "), and number of items in list (" + this.values.Count + ").");
                    return retVal;
                }

                // Choose an increment
                if (useRandomIncrement && this.values.Count > 1)
                {
                    this.increment = this.r.Next(1, this.values.Count - 1);
                }
                else 
                {
                    // If there is room increment, otherwise 1
                    if (this.increment + 1 < this.values.Count)
                        this.increment++;
                    else
                        this.increment = 1;
                }

                // Validate increment as result
                retVal = this.increment >= 0 && this.increment < this.values.Count;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        // setPrime does not validate number is valid prime. Sets only. Validation to be 
        // performed by caller.
        public bool setPrime(int prime)
        {
            const string location = CLASSNAME + ".setPrime";
            bool retVal = false;
            try
            {
                if (prime <= 0)
                {
                    L.err(location, "Input value was invalid (" + prime + ").");
                    return retVal;
                }

                // Update P and values
                if (prime != this.p) this.cntPChange++;
                this.p = prime;
                this.values = new List<int>();
                if (!this.loadValues())
                {
                    L.err(location, "Failed to load data for new prime (" + p + ").");
                    return retVal;
                }

                // Reset offset and increment randomly within range
                if (this.offset >= this.values.Count)
                {
                    this.offset = r.Next(0, this.values.Count - 1);// TODO - Randomize
                }
                if (this.increment >= this.values.Count)
                {
                    this.increment = r.Next(1, this.values.Count - 1);// TODO - Randomize
                }

                // Flag success for completing
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

    }
}
