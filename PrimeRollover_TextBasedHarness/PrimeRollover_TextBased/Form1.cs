//Prime Rollover - Text Based Harness
//(c) 2026 - TimeWarpToaster

//https://www.gnu.org/licenses/gpl-3.0.html

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PrimeRollover_Example
{
    public partial class Form1 : Form
    {
        public const string CLASSNAME = "Form1";

        // These are prime numbers to choose from in the drop-down
        public int[] optionsForSelect = new int[]
        {
            1,3,7,11,13,
            17,19,23,29,31,
            37,41,43,47,53,
            59,61,67,71,73,
            79,83,89,97,101,
            103,107,109,113,127,
            131,137,139,149,151,
            157,163,167,173,179,
            181,191,193,197,199
        };


        public Form1()
        {
            const string location = CLASSNAME + ".Form1";
            try
            {
                InitializeComponent();

                if (!L.logInit(null, rtbLogsOut, false))
                {
                    // In this case, logging will quietly do nothing
                }

                if (!initApp())
                {
                    L.err(location, "Failed to initialize application.");
                }
                else 
                {
                    L.l(location, "Application initialized.");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        public bool initApp()
        {
            const string location = CLASSNAME + ".initApp";
            bool retVal = false;
            try
            {
                int cntErr = 0;

                this.loadSelectPrime();
                L.l(location, "Loaded (" + selectPrime.Items.Count + ") primes to select from.");
                if (selectPrime.Items.Count == 0)
                {
                    cntErr++;
                }
                else
                {
                    // Select a prime near the top of list
                    int idxSelect = 0;
                    if (selectPrime.Items.Count > 4) idxSelect = selectPrime.Items.Count / 4 | 0;
                    selectPrime.SelectedItem = selectPrime.Items[idxSelect];
                }

                this.loadInstructions();
                this.loadDocumentation();
                this.loadAbout();

                retVal = cntErr == 0;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool go()
        {
            const string location = CLASSNAME + ".go";
            bool retVal = false;
            try
            {
                // Get text on ui thread
                string sP = selectPrime.SelectedItem.ToString();
                int p = -1;
                try
                {
                    p = Convert.ToInt32(sP);
                }
                catch (Exception exConv) { }
                if (p < 0)
                {
                    L.err(location, "Failed to select item from list.");
                    return retVal;
                }
                L.l(location, "Setting up rollover for (" + p + ").");

                // Clear old UI data
                if (!setRtb(rtbMain, ""))
                {
                    L.err(location, "Failed to clear UI before series for (" + p + ").");
                }

                Thread thread = new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        // Create a prime rollover object
                        PrimeRollover pr = new PrimeRollover();
                        pr.setPrime(p);
                        pr.offset = 0;
                        //pr.increment = 1;

                        StringBuilder sb = new StringBuilder();

                        /*// Next lines log the current series based upon P and increment
                        List<int> series = pr.getCurrentSeries();
                        for (int i = 0; i < series.Count; i++)
                        {
                            sb.Append(i).Append(i == series.Count - 1 ? "." : ", ");
                        }
                        L.l(location, "P (" + pr.p + "), Series (" + pr.increment + "), Values: " + sb.ToString());*/

                        // Next lines pushes all series for P to Main tab in UI
                        Dictionary<int, List<int>> allSeries = pr.getAllSeries();
                        if (allSeries == null || allSeries.Count == 0)
                        {
                            L.err(location, "Result for all increments was null or empty.");
                            return;
                        }

                        sb = new StringBuilder();
                        sb.Append("\nIncrements For (" + pr.p + "):\n\n\n");
                        foreach (KeyValuePair<int, List<int>> kv in allSeries)
                        {
                            sb.Append("Prime (" + pr.p + "), Increment (" + kv.Key + "):\n");
                            string temp = pr.formatSeriesForUi(kv.Value);

                            sb.Append(temp);

                            sb.Append("\n\n\n");
                        }

                        if (!setRtb(rtbMain, sb.ToString()))
                        {
                            L.err(location, "Failed to load UI with all series result.");
                        }
                    }
                    catch (Exception ex)
                    {
                        L.ex(location, ex);
                    }
                }));
                thread.Start();


                // Flag success for starting thread
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public void loadAbout()
        {
            const string location = CLASSNAME + ".loadAbout";
            try
            {
                string license = License.license;

                if (!setRtb(rtbAbout, license))
                {
                    L.err(location, "Failed to load About tab.");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        public void loadDocumentation()
        {
            const string location = CLASSNAME + ".loadDocumentation";
            try
            {
                string documentation =
                    "\n" +
                    "Prime Rollover is a generic process for iterating a list of items, knowing you have used " +
                    "every item once-and-only-once, and either quitting, going again in the same pattern, " +
                    "or selecting a new increment, when returning to the starting-point. In-short, Prime " +
                    "Rollover is a pseudo-randomizer, that does not rely on re-draws or eliminating duplicates, " +
                    "while enforcing even-usage of elements, and is extremely performance efficient.\n" +
                    "\n" +
                    "For verbal illustration, let’s use the old fence-post analogy. You have seven fence posts in a " +
                    "row, and need to connect them. For this, you use six sections of fence. To understand what is " +
                    "happening in Prime Rollover, you need to first arrange these fence posts into a circle. How many " +
                    "sections of fence do you need? You now need seven sections of fence to complete the circle. When " +
                    "we use Prime Rollover, we treat the values as though our list is a circle.\n" +
                    "\n" +
                    "This gets better. We are well acquainted with the indivisibility of primes. In a normal circumstance, " +
                    "we either say that it cannot be done, or results in a fraction. Prime Rollover is an exception. Every " +
                    "prime number is evenly divisible everytime, from a Prime Rollover perspective. If anything, this " +
                    "makes primes just as unique in Prime Rollover, as they are in the ordinary perspective of numbers " +
                    "(non-prime numbers quit before using every element, they fail). Everything I just said, is something " + 
                    "of a misnomer.\n" +
                    "\n" +
                    "Moving back to our example of a fence, arranged in a circle, and getting back to the nuts and bolts " +
                    "of what Prime Rollover is, think of connecting every fence-post, to two-different fence sections, " +
                    "without duplicating lines. The shape of a circle stops mattering. What you get, is either a circle-" +
                    "perimeter (increment = 1, as in our first case), or star shapes if incrementing by any number " +
                    "greater-than-one but less than P-1 (P-1 is also a circle, travelled backwards). What you see, is " + 
                    "the visual attempt to divide a prime, and collecting the remainder until full.\n" +
                    "\n" +
                    "To make this more clear, when walking the perimeter, we have effectively divided the prime number by " +
                    "one, touched every fence-post in order, and on the P-section of fence, returned to where we were. " +
                    "We divided P by 1, and got P sections of fence, two for every post. Just as important, we did this " +
                    "in one-revolution of the circle.\n" +
                    "\n" +
                    "Now, clear everything but the posts, and increment by 2, touching every-other post in order, until " +
                    "you reach the start. You have effectively divided P by 2, taking 2 revolutions around the circle to " +
                    "perform, for a total of P * 2 posts, or 14-posts.\n" +
                    "\n" +
                    "You can see now, that we are not actually dividing a prime. We are using the optical illustration of " +
                    "Prime Rollover, to see that out of 7-posts, you can only touch 3.5 of them per revolution, when " +
                    "dividing by two – but inherently, you touch every post, without repeat, before returning to where you " +
                    "began. This is the quality that makes Prime Rollover an ideal pseudo-randomizer. The remainder " +
                    "collects, until whole, on the N iteration. We simultaneously try to divide prime by some smaller " +
                    "number N, and multiply prime by that same number N, to arrive at everything being used after N iterations " +
                    "through list.\n" +
                    "\n" + 
                    "Put plainly, (P * N) / N always divides evenly and usefully, and always equals P.\n" + 
                    "\n" + 
                    "---\n" +
                    "\n" + 
                    "Uses for Prime Rollover vary. The most obvious uses, are making a large number of assignments, from a " + 
                    "limited pool, where even-usage is important. That said, I have used it in very-small projects, for " +
                    "something as simple as randomizing answers on a multiple-choice form.\n" + 
                    "\n" + 
                    "The obvious draw back, is the list must contain a prime number of elements. In the example of multiple " + 
                    "choice, my lists needed to be 5 or 7 elements long, even if there were fewer answers. The simple " + 
                    "workaround, is to have null elements at the end, and discard null when passing by.\n" +
                    "\n";

                // Push to UI
                if (!setRtb(rtbDocumentation, documentation))
                {
                    L.err(location, "Failed to set documentation tab.");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        public void loadInstructions()
        {
            const string location = CLASSNAME + ".loadInstructions";
            try
            {
                string instructions =
                    //"Prime Rollover\n" + 
                    "\n" +
                    "On app load, select a prime number to perform rollover on from the left-pane, " +
                    "and click \"GO\".\n" +
                    "\n" +
                    "In the Main tab, a series of numbers will be displayed, corresponding to the value " +
                    "stored at each location.\n" +
                    "\n" +
                    "For simplicity of illustration, this app automatically loads a List, with the number " +
                    "directly corresponding to the current position (e.g. the value stored at index-1 is 1). " +
                    "Each series shown, displays every stored value, in the order established by increment.\n" +
                    "\n" +
                    "For a more in-depth tutorial on what Prime Rollover is, what these numbers or their " +
                    "order means, and what Prime Rollover is capable of, check out the Documentation tab.";

                // Push to UI
                if (!setRtb(rtbInstructions, instructions))
                {
                    L.err(location, "Failed to set instructions tab.");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        public void loadSelectPrime()
        {
            const string location = CLASSNAME + ".loadSelectPrime";
            try
            {
                if (selectPrime.InvokeRequired)
                {
                    selectPrime.Invoke(new Action(() => loadSelectPrime()));
                }
                else
                {
                    string[] forUi = new string[this.optionsForSelect.Length];
                    for (int i = 0; i < this.optionsForSelect.Length; i++)
                    {
                        forUi[i] = Convert.ToString(this.optionsForSelect[i]);
                    }
                    selectPrime.Items.AddRange(forUi);
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        public void rtbSetBold(RichTextBox rtb, string lastStringAdded)
        {
            const string location = CLASSNAME + ".boldText";
            try
            {
                if (rtb.InvokeRequired)
                {
                    rtb.Invoke(new Action(() => rtbSetBold(rtb, lastStringAdded)));
                }
                else
                {
                    int start = rtb.TextLength - lastStringAdded.Length - 1;// TODO - figure out if -1 required
                    rtb.Select(start, lastStringAdded.Length);
                    rtb.SelectionFont = new Font(rtb.Font, FontStyle.Bold);

                    rtb.SelectionStart = rtb.TextLength;
                    rtb.SelectionLength = 0;
                    rtb.SelectionFont = new Font(rtb.Font, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private bool setRtb(RichTextBox rtb, string value)
        {
            const string location = CLASSNAME + ".setRtb";
            bool retVal = false;
            try
            {
                if (rtb == null)
                {
                    L.err(location, "Input rtb was null.");
                    return retVal;
                }
                if (value == null)
                {
                    L.err(location, "Input value was null.");
                    return retVal;
                }

                if (rtb.InvokeRequired)
                {
                    rtb.Invoke(new Action(() => setRtb(rtb, value)));
                }
                else
                {
                    rtb.Text = value;
                }

                // Flag success for completing, invoke disjoins true result
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string location = CLASSNAME + ".exitToolStripMenuItem_Click";
            try
            {
                L.l(location, "Application exiting from menu item.");
                Environment.Exit(0);
                L.l(location, "Application failed to exit from menu item.");
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            const string location = CLASSNAME + ".btnGo_Click";
            try
            {
                if (!go())
                {
                    L.err(location, "Failed to complete processing on go attempt.");
                }
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }
    }
}
