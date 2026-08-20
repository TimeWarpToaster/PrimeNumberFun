//Prime Rollover - Circle Diagram
//(c) 2026 - TimeWarpToaster

//https://www.gnu.org/licenses/gpl-3.0.html

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeRollover_CircleDiagram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string CLASSNAME = "MainWindow";

        RichTextBox rtb = new RichTextBox();

        bool keepThreadRunning = false;

        bool isDrawing = false;

        int[] primes = new int[]
        {
            5, 7, 11, 13, 17,
            19, 23, 29, 31, 37,
            41, 43, 47, 53, 59,
            61, 67, 71, 73, 79,
            83, 89, 97, 101, 103,
            107, 109, 113, 127, 131,
            137, 139, 149, 151, 157,
            163, 167, 173, 179, 181,
            191, 197, 199
        };

        public int myBrush = 0;
        public int strokeWidth = 2;

        public int uiIncr = -1;

        public int myPrimeInt = 0;

        public List<int> regularPrimes = new List<int>();
        public int regularPrimesIdx = 23;//101

        private readonly DispatcherTimer drawTimer;

        //private string logPath = @"C:\PR_Log_CircleDiagram.txt";

        public MainWindow()
        {
            const string location = CLASSNAME + ".Constructor";
            try
            {
                InitializeComponent();

                // Init with our without file logging (without)
                if (!L.logInit(null, rtbLogsOut, false))
                //if (!L.logInit(@logPath, rtbLogsOut, true))
                {
                    // Safe to call
                    L.err(location, "Failed to init logging.");
                }
                // Determine if logs pushed with L.d(..) are written
                L.isDebug = true;

                // Start a timer to know if we are actively drawing
                drawTimer = new DispatcherTimer();
                if (!StartDrawingPoll(100))// check every 100ms
                {
                    L.err(location, "Failed to start drawing poll.");
                }

                L.l(location, "App Starting...");
                if (!this.init())
                {
                    L.err(location, "Failed to initialize!");
                }
                else
                {
                    this.myPrimeInt = this.regularPrimes[this.regularPrimesIdx];
                    if (!this.loadPts(this.myPrimeInt))
                    {
                        L.err(location, "Failed to load points.");
                    }
                    drawPrime();
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        public bool init()
        {
            const string location = CLASSNAME + ".init";
            bool retVal = false;
            try
            {

                // Initialize utility items
                U.pts = new PointCollection();
                ColorOpts.init();

                if (6 < ColorOpts.brushes.Count) myBrush = 5;// holdover, to start with a nice diagram of 5

                // Initialize primes list and load the first
                regularPrimes = new List<int>();
                regularPrimes.AddRange(primes);

                for (int i = 0; i < primes.Length; i++)
                    comboPrimeSelect.Items.Add(Convert.ToString(primes[i]));

                int startIdx = regularPrimes.IndexOf(97); 
                this.regularPrimesIdx = startIdx >= 0 ? startIdx : 0;
                lblCurrentValueOut.Content = Convert.ToString(this.regularPrimes[this.regularPrimesIdx]);

                // Setting combo box value will start first diagram
                string matchString = Convert.ToString(this.regularPrimes[startIdx]);
                comboPrimeSelect.SelectedIndex = comboPrimeSelect.Items.IndexOf(matchString);

                // Set default delay between adding lines to the UI
                tbLineDelay.Text = "20";
                if (!visualHost.setTimerDelay(20))
                {
                    L.err(location, "Failed to set line timer delay.");
                }

                // Format UI
                lblCurrentValue.Foreground = Brushes.White;
                lblCurrentValueOut.Foreground = Brushes.White;

                // Load tabs
                if (!Instructions.setInstructions(rtbInstructions))
                {
                    L.err(location, "Failed to set Instructions tab.");
                }
                if (!Primer.setPrimer(rtbPrimer))
                {
                    L.err(location, "Failed to set Primer tab.");
                }
                if (!About.setAbout(rtbAbout))
                {
                    L.err(location, "Failed to set About tab.");
                }

                // Start a timer to poll whether we are actively drawing or not
                int drawingPollDelay = 100;//ms
                if (!StartDrawingPoll(drawingPollDelay))
                {
                    L.err(location, "Failed to poll drawing completing with interval (" + drawingPollDelay + "ms).");
                }

                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }
        public bool StartDrawingPoll(int delayMs)
        {
            const string location = CLASSNAME + ".StartDrawingPoll";
            bool retVal = false;
            try
            {
                drawTimer.Interval = TimeSpan.FromMilliseconds(delayMs);
                drawTimer.Tick += isDrawingPoll;
                drawTimer.Start();

                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public void isDrawingPoll(object sender, EventArgs e)
        {
            const string location = CLASSNAME + ".isDrawingPoll";
            try
            {
                this.isDrawing = !(visualHost.CountQueue == 0);
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }


        public bool toggleButtonsMs(int milliseconds)
        {
            const string location = CLASSNAME + ".toggleButtonsMs";
            bool retVal = false;
            try
            {
                Thread t = new Thread(() =>
                {
                    try
                    {
                        if (!buttonsEnabled(false))
                        {
                            L.err(location, "Failed to toggle buttons off.");
                        }

                        Thread.Sleep(milliseconds);

                        if (!buttonsEnabled(true))
                        {
                            L.err(location, "Failed to toggle buttons on.");
                        }
                    }
                    catch (Exception ex)
                    {
                        L.ex(location, ex);
                    }
                });
                t.Start();
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool buttonsEnabled(bool enabled)
        {
            const string location = CLASSNAME + ".buttonsEnabled";
            bool retVal = false;
            try
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        btnRedoLines.IsEnabled = enabled;
                        btnForward.IsEnabled = enabled;
                        btnBackward.IsEnabled = enabled;
                        cbUseSameColor.IsEnabled = enabled;
                        btnLineDelay.IsEnabled = enabled;
                        btnStrokeWidth.IsEnabled = enabled;
                        btnColorOptions.IsEnabled = enabled;
                        retVal = true;
                    }
                    catch (Exception ex)
                    {
                        L.ex(location, ex);
                    }
                });
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool clearDrawSpace()
        {
            const string location = CLASSNAME + ".clearDrawSpace";
            bool retVal = false;
            try
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        int cntClearedItems = visualHost.clear();
                        L.l(location, "Cleared (" + cntClearedItems + ") items from UI.");
                    }
                    catch (Exception ex)
                    {
                        L.ex(location, ex);
                    }
                });
                GC.Collect();

                // Flag success
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        private async Task drawAllLineSeries()
        {
            const string location = CLASSNAME + ".drawAllLineSeries";
            try
            {
                // There are (P / 2 | 0) + 1 unique patterns that can be formed
                // (direction-independent), we only do about half
                int numSeriesToDraw = 0;
                App.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        numSeriesToDraw = (U.pts.Count / 2) | 0;
                    }
                    catch (Exception ex) { }
                });
                L.l(location, "Series to draw (1 - " + numSeriesToDraw + ").");

                int cntDrawn = 0;
                for (int i = 1; i <= numSeriesToDraw; i++, cntDrawn++)
                {
                    if (!keepThreadRunning)
                    {
                        L.l(location, "Finishing thread by request.");
                        return;
                    }

                    Task drawSeries = drawLineSeries(i, -1);//-1 to start from i position
                    drawSeries.Wait();
                }
                if (keepThreadRunning)
                {
                    L.l(location, "Finished drawing (" + cntDrawn + ") increment series.");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return;
        }

        public async Task drawLineSeries(int increment, int start)
        {
            const string location = CLASSNAME + ".drawLineSeries";
            try
            {
                // Determine if color rotates between series
                bool useNewColor = false;
                App.Current.Dispatcher.Invoke(() =>
                {
                    useNewColor = cbUseSameColor.IsChecked == false;
                });
                if (useNewColor) this.myBrush++;
                if (this.myBrush >= ColorOpts.brushes.Count) this.myBrush = 0;

                // Set some flags
                int startPosition = increment;// using interval as start position for no real reason
                bool started = false;
                bool finished = false;

                // New Note:  You can never not-finish at the point where you started
                // THIS ONLY WORKS FOR PRIME - Infinite/Short loop otherwise
                if (start < 0) start = increment;
                for (int i = start; !finished; i += increment)
                {
                    if (!keepThreadRunning) break;

                    //int numLines = 0;
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            if (!keepThreadRunning) return;

                            if (i >= U.pts.Count)
                            {
                                i -= U.pts.Count;
                            }

                            // Determine if we reached end
                            if (i == startPosition)
                            {
                                if (started)
                                {
                                    // Series completed
                                    finished = true;// for outer loop
                                    return;// quits dispatcher
                                }
                                started = true;
                            }

                            // Draw a line from this position, to the next according to increment
                            int idxNextPoint = (i + startPosition >= U.pts.Count) ?
                                (i + startPosition) - U.pts.Count :
                                i + startPosition;

                            // Only manipulate ui if thread is not about to end
                            if (!keepThreadRunning) return;

                            Pen pen = new Pen(ColorOpts.brushes[this.myBrush], this.strokeWidth);
                            visualHost.DrawLine(U.pts[i], U.pts[idxNextPoint], pen);
                        }
                        catch (Exception ex)
                        {
                            L.ex(location, ex);
                        }
                    }, DispatcherPriority.Normal);

                    if (!keepThreadRunning) return;
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        public async Task drawPrime()
        {
            const string location = CLASSNAME + ".drawPrime";
            try
            {
                // Disable buttons while thread launches
                toggleButtonsMs(100);

                // Update label for current prime
                lblCurrentValueOut.Content = Convert.ToString(U.pts.Count);
                //L.l(location, "Working prime (" + U.pts.Count + ").");


                // Create a thread to start a thread, because the only thread-slot
                // may be full, wait for it
                Thread t = new Thread(() =>
                {
                    try
                    {
                        if (U.graphThread != null)
                        {
                            if (U.graphThread.IsAlive)
                            {
                                // Request existing thread to stop nicely
                                keepThreadRunning = false;
                                for (int i = 0; i < 300; i++)
                                {
                                    if (U.graphThread != null && U.graphThread.IsAlive) Thread.Sleep(10);
                                    else break;
                                }

                                // Request less nice
                                if (U.graphThread != null && U.graphThread.IsAlive)
                                {
                                    U.graphThread.Interrupt();
                                    for (int i = 0; i < 300; i++)
                                    {
                                        if (U.graphThread != null && U.graphThread.IsAlive) Thread.Sleep(10);
                                        else break;
                                    }
                                }

                                // Thread never knew better
                                if (U.graphThread != null && U.graphThread.IsAlive)
                                {
                                    L.err(location, "Failed to wait for old thread to end.");
                                    return;
                                }
                            }
                        }

                        U.graphThread = null;
                        U.graphThread = new Thread(() =>
                        {
                            try
                            {
                                keepThreadRunning = true;

                                // Determine if we are drawing all series or single increment
                                if (uiIncr < 1)
                                {
                                    // Draw series and place them in queue
                                    //L.l(location, "Preparing to draw all line series.");
                                    Task drawSeries = drawAllLineSeries();
                                    drawSeries.Wait();
                                }
                                else 
                                {
                                    //L.l(location, "Preparing to draw increment (" + uiIncr + ") of prime (" + myPrimeInt + ").");
                                    Task drawSeries = drawLineSeries(uiIncr, 0);
                                    drawSeries.Wait();
                                }


                                // Tell queue to begin processing
                                App.Current.Dispatcher.Invoke(() =>
                                {
                                    try
                                    {
                                        if (!visualHost.StartQueueAnimation())
                                        {
                                            L.err(location, "Failed to start queue animation.");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        L.err(location, "Failed to start animation: " + ex.Message);
                                    }
                                });

                                // Update thread flags and finish
                                keepThreadRunning = false;
                            }
                            catch (Exception exThread)
                            {
                                L.err(location, "Thread error: " + exThread.Message);
                                try
                                {
                                    keepThreadRunning = false;
                                }
                                catch (Exception ex) { }
                            }
                        });
                        U.graphThread.Start();
                    }
                    catch (Exception ex)
                    {
                        L.ex(location, ex);
                    }
                });
                t.Start();
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        public bool loadNext()
        {
            const string location = CLASSNAME + ".loadNext";
            bool retVal = false;
            try
            {
                // Reset display
                keepThreadRunning = false;
                if (!clearDrawSpace())
                    L.l(location, "Failed to clear draw-space.");

                this.regularPrimesIdx++;
                if (this.regularPrimesIdx >= this.regularPrimes.Count) this.regularPrimesIdx = 0;

                // Just get a number for P
                string s = Convert.ToString(this.regularPrimes[this.regularPrimesIdx]);
                int idxSelect = comboPrimeSelect.Items.IndexOf(s);
                if (idxSelect < 0) 
                {
                    L.err(location, "Prime not found in select (" + s + ").");
                    return retVal;
                }

                // Callback for selected item changed handles things
                comboPrimeSelect.SelectedItem = comboPrimeSelect.Items[idxSelect];

                // Flag success
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool loadPrev()
        {
            const string location = CLASSNAME + ".loadPrev";
            bool retVal = false;
            try
            {
                // Reset display
                keepThreadRunning = false;
                if (!clearDrawSpace())
                    L.l(location, "Failed to clear draw-space.");

                this.regularPrimesIdx--;
                if (this.regularPrimesIdx < 0) this.regularPrimesIdx = this.regularPrimes.Count - 1;

                // Just get a number for P
                string s = Convert.ToString(this.regularPrimes[this.regularPrimesIdx]);
                int idxSelect = comboPrimeSelect.Items.IndexOf(s);
                if (idxSelect < 0)
                {
                    L.err(location, "Prime not found in select (" + s + ").");
                    return retVal;
                }

                // Callback for selected item changed handles things
                comboPrimeSelect.SelectedItem = comboPrimeSelect.Items[idxSelect];

                // Flag success
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool loadPts(int myPrimeInt)
        {
            const string location = CLASSNAME + ".loadPts";
            bool retVal = false;
            try
            {
                if (myPrimeInt <= 0) return false;//Early Exit

                if (U.pts == null) U.pts = new PointCollection();
                else U.pts.Clear();

                System.Windows.Point centerPoint = new System.Windows.Point(300, 300);

                int distance = 300;

                // get angle from center
                double angle = (double)360 / (double)myPrimeInt;
                L.d(location, "Iterating angle (" + angle + ")");

                int iterations = 0;
                for (
                    double currentAngle = 0d;
                    currentAngle <= 360 && iterations < myPrimeInt;
                    currentAngle += angle
                    )
                {
                    double degree = (currentAngle * Math.PI / 180);
                    U.pts.Add(new System.Windows.Point(
                        (centerPoint.X + distance * Math.Cos(degree)),
                        (centerPoint.Y - distance * Math.Sin(degree))
                        ));
                    iterations++;
                }
                // log number of pts loaded
                L.l(location, "Loaded (" + U.pts.Count + ") points.");
                retVal = U.pts.Count == myPrimeInt;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool loadPrime(int primeIn, bool isDrawPrime)
        {
            const string location = CLASSNAME + ".loadPrime";
            bool retVal = false;
            try
            {
                if (primeIn <= 0)
                {
                    L.err(location, "Input value was negative or zero.");
                    return retVal;
                }

                keepThreadRunning = false;

                if (!clearDrawSpace())
                    L.l(location, "Failed to clear draw-space.");

                // Get a new prime and index in memory
                int tempRegularIdx = this.regularPrimes.IndexOf(primeIn);
                if (tempRegularIdx < 0)
                {
                    L.err(location, "Failed to locate prime in list, input (" + tempRegularIdx + ").");
                    return retVal;
                }
                this.regularPrimesIdx = tempRegularIdx;

                this.myPrimeInt = this.regularPrimes[this.regularPrimesIdx];
                L.l(location, "Loading prime number (" + this.myPrimeInt + ").");
                if (!this.loadPts(this.myPrimeInt))
                {
                    L.err(location, "Failed to load points.");
                    return retVal;
                }

                // Update the selectable increments in combobox
                int numSeriesToDraw = (U.pts.Count / 2) | 0;
                if (!showSelectSingleIncr(1, numSeriesToDraw))
                {
                    L.err(location, "Failed to update selectable increments.");
                }

                // Draw prime
                if (isDrawPrime) drawPrime();

                // Flag success
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool showSelectSingleIncr(int min, int max)
        {
            const string location = CLASSNAME + ".showSelectSingleIncr";
            bool retVal = false;
            try
            {
                // Get currently selected item
                string item = "All";
                if (comboSelectSingle.SelectedItem != null)
                {
                    item = comboSelectSingle.SelectedItem.ToString();
                }

                // Clear items
                comboSelectSingle.Items.Clear();

                // Add all and min-to-max
                comboSelectSingle.Items.Add("All");
                comboSelectSingle.SelectedItem = comboSelectSingle.Items[comboSelectSingle.Items.IndexOf("All")];

                for (int i = min; i <= max; i++)
                {
                    comboSelectSingle.Items.Add(Convert.ToString(i));
                }

                // See if currently selected increment exists
                int idxPrevious = comboSelectSingle.Items.IndexOf(item);
                if (idxPrevious >= 0)
                {
                    comboSelectSingle.SelectedItem = comboSelectSingle.Items[idxPrevious];
                }

                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }



        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            const string location = CLASSNAME + ".btnForward_Click";
            try
            {
                if (!loadPrev())
                {
                    L.err(location, "Failed to load previous prime.");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private void btnClearLog_Click(object sender, RoutedEventArgs e)
        {
            const string location = CLASSNAME + "btnClearLog_Click";
            try
            {
                long lengthCleared = L.clearLogs();
                L.l(location, "Cleared (" + lengthCleared + ") length from UI logs.");
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private void btnColorOptions_Click(object sender, RoutedEventArgs e)
        {
            const string location = CLASSNAME + ".btnColorOptions_Click";
            try
            {
                Window colorOptions = new ColorOptions();
                colorOptions.Show();
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            const string location = CLASSNAME + ".btnForward_Click";
            try
            {
                if (!loadNext())
                {
                    L.err(location, "Failed to load next prime.");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private void btnRedoLines_Click(object sender, RoutedEventArgs e)
        {
            const string location = CLASSNAME + ".btnRedoLines";
            try
            {
                // Reset display
                keepThreadRunning = false;
                if (!clearDrawSpace())
                    L.l(location, "Failed to clear draw-space.");

                drawPrime();
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private void btnStrokeWidth_Click(object sender, RoutedEventArgs e)
        {
            const string location = CLASSNAME + ".btnSetStrokeWidth_Click";
            try
            {
                int temp = this.strokeWidth;
                try
                {
                    string s = tbStrokeWidth.Text;
                    if (s.Length > 0)
                    {
                        temp = Convert.ToInt32(s);
                    }
                }
                catch (Exception exConv)
                {
                    L.err(location, "Failed to convert Stroke Width to integer with error: " + exConv.Message);
                }
                this.strokeWidth = temp;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private void btnLineDelay_Click(object sender, RoutedEventArgs e)
        {
            const string location = CLASSNAME + ".btnPrimeDelay_Click";
            try
            {
                string s = tbLineDelay.Text;
                int currentDelay = visualHost.getTimerDelay();
                int delay = -1;
                try
                {
                    delay = Convert.ToInt32(tbLineDelay.Text);
                }
                catch (Exception ex) { }
                if (delay == currentDelay) return;

                if (delay < 0 || delay > 100000)
                {
                    MessageBox.Show("The line delay must be between 0 - 100000.", "Invalid Line Delay", MessageBoxButton.OK);
                    return;
                }

                L.l(location, "Setting line timer delay to (" + delay + "ms) from (" + currentDelay + "ms).");
                if (!visualHost.setTimerDelay(delay))
                {
                    L.err(location, "Failed to set line timer delay.");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private void comboPrimeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            const string location = CLASSNAME + ".comboPrimeSelect_SelectionChanged";
            try
            {
                string value = comboPrimeSelect.SelectedValue.ToString();

                L.l(location, "Selected value (" + value + ").");

                // Get our current prime
                int prime = 0;
                try
                {
                    prime = Convert.ToInt32(value);
                }
                catch (Exception ex) { }

                if (prime <= 0)
                {
                    L.err(location, "Failed to convert input (" + value + ") to number.");
                    return;
                }

                // Get the selected increment
                string selectIncr = "";
                if (comboSelectSingle.SelectedItem != null)
                    selectIncr = comboSelectSingle.SelectedItem.ToString();

                // Validate, default as needed, load
                if (selectIncr.Equals("All"))
                {
                    if (!loadPrime(prime, true))
                        L.err(location, "Failed to load prime (" + value + ").");
                }
                else
                {
                    int incr = -1;
                    try
                    {
                        incr = Convert.ToInt32(selectIncr);
                    }
                    catch (Exception ex) { }

                    int numSeriesToDraw = (prime / 2) | 0;
                    if (incr < 1 || incr > numSeriesToDraw)
                    {
                        // Force selection of all, which fires an equivalent result
                        uiIncr = -1;// default to "All" (first)
                        int idxAll = comboSelectSingle.Items.IndexOf("All");
                        if (idxAll >= 0) comboSelectSingle.SelectedItem = comboSelectSingle.Items[idxAll];
                    }
                    else
                    {
                        uiIncr = incr;
                    }
                    L.l(location, "Loading prime (" + prime + ") with increment (" + uiIncr + ") num series (" + numSeriesToDraw + ").");
                    if (!loadPrime(prime, true))
                    {
                        L.err(location, "Failed to load prime (" + prime + ").");
                    }
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private void btnSelectSingleIncr_Click(object sender, RoutedEventArgs e)
        {
            const string location = CLASSNAME + ".btnSelectSingleIncr_Click";
            try
            {
                // Get the current prime
                int prime = -1;
                if (this.regularPrimesIdx >= 0 && this.regularPrimesIdx < this.regularPrimes.Count)
                {
                    prime = this.regularPrimes[this.regularPrimesIdx];
                }
                if (prime < 0)
                {
                    L.err(location, "Current prime not set.");
                    return;
                }

                // Get the requested increment
                int incr = -1;
                try
                {
                    string selectIncr = "";
                    if (comboSelectSingle.SelectedItem != null)
                        selectIncr = comboSelectSingle.SelectedItem.ToString();

                    if (!selectIncr.Equals("All") && selectIncr.Length > 0)
                    {
                        incr = Convert.ToInt32(selectIncr);
                    }
                }
                catch (Exception ex) { }
                
                // Validate increment against prime
                int numSeriesToDraw = ((prime / 2) | 0) + 1;
                if (incr < 1 || incr > numSeriesToDraw)
                {
                    // This is not really an error scenario, its a quiet autocorrect
                    //L.err(location, "Increment (" + incr + ") out of bounds (1-" + numSeriesToDraw + ").");

                    // Force selection of all
                    int idxAll = comboSelectSingle.Items.IndexOf("All");
                    if (idxAll >= 0) comboSelectSingle.SelectedItem = comboSelectSingle.Items[idxAll];
                    this.uiIncr = -1;// default to "All"
                }
                else
                {
                    this.uiIncr = incr;
                }
                L.l(location, "Loading prime (" + prime + ") with increment (" + 
                    (this.uiIncr > 0 ? Convert.ToString(this.uiIncr) : "All") + ") out of supported (" + numSeriesToDraw + ").");

                // Draw
                if (!loadPrime(prime, true))
                {
                    L.err(location, "Failed to load prime (" + prime + ").");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }
    }


}

