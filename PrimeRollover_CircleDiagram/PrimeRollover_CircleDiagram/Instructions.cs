//Prime Rollover - Circle Diagram
//(c) 2026 - TimeWarpToaster

//https://www.gnu.org/licenses/gpl-3.0.html

using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PrimeRollover_CircleDiagram
{
    public static class Instructions
    {
        const string CLASSNAME = "Instructions";
        

        private static bool addHeader(RichTextBox rtb, string header, double fontSize)
        {
            const string location = CLASSNAME + ".addHeader";
            bool retVal = false;
            try
            {
                // Validate input
                if (header == null)
                {
                    L.err(location, "Input text was null.");
                    return retVal;
                }
                if (header.Length == 0)
                {
                    retVal = true;
                    return retVal;// nothing to do
                }
                if (fontSize < 6 || fontSize > 200)
                {
                    L.err(location, "Requested font-size (" + fontSize + ") out of range (6-200).");
                    return retVal;
                }

                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Bold(new Run(header)));
                TextPointer selectionStart = rtb.Document.ContentEnd.GetInsertionPosition(LogicalDirection.Forward);
                rtb.Document.Blocks.Add(paragraph);
                TextPointer selectionEnd = rtb.Document.ContentEnd.GetInsertionPosition(LogicalDirection.Backward);
                rtb.Selection.Select(selectionStart, selectionEnd);
                rtb.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, 14.0);
                rtb.Selection.Select(rtb.Document.ContentEnd, rtb.Document.ContentEnd);

                // Flag success for completing
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public static bool setInstructions(RichTextBox rtb)
        {
            const string location = CLASSNAME + ".setInstructions";
            bool retVal = false;
            try
            {
                if (rtb == null) 
                {
                    L.err(location, "Failed to set instructions with null view.");
                    return retVal;
                }

                rtb.Document.Blocks.Clear();
                rtb.FontSize = 16;
                Color foreColor = Color.FromRgb(20, 20, 20);
                rtb.Foreground = new SolidColorBrush(foreColor);
                Color backColor = Color.FromRgb(252, 252, 245);
                rtb.Background = new SolidColorBrush(backColor);



                if (!addHeader(rtb, "Prime Rollover - Circle Diagram", 18))
                {
                    L.err(location, "Failed to add title.");
                }
                if (!addHeader(rtb, "Instructions", 18))
                {
                    L.err(location, "Failed to add instructions.");
                }

                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "The purpose of this app, is to visually illustrate Prime Rollover as a " + 
                    "product of the indivisibility of primes. In default configuration, each " + 
                    "color represents attempting to divide the prime by a different value."
                );
                rtb.Document.Blocks.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "Upon app load, a pre-detemined prime number is loaded to view, its " + 
                    "points are calculated along a circle, and it begins attempting to divide " + 
                    "the prime number, rolling-over, until returning to the place where it " + 
                    "began. Each complete increment series (attempt to divide while rolling over) " + 
                    "has a color, and each line can be thought of as representing the amount of " + 
                    "change for subtracting the increment from prime, one-time."
                );
                rtb.Document.Blocks.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "To begin, use the dropdown at the top-right, below \"Current Prime\", to " + 
                    "select a prime of your own. This will automatically start the prime being " + 
                    "drawn. Now, you have some options for how the prime is drawn."
                );
                rtb.Document.Blocks.Add(paragraph);


                // Setting Options
                rtb.AppendText("\n");
                paragraph = new Paragraph();
                paragraph.Inlines.Add(new Bold(new Run("Setting Options")));
                rtb.Document.Blocks.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "Navigation controls are self-explanatory for forward, previous, and redraw."
                );
                rtb.Document.Blocks.Add(paragraph);


                if (!addHeader(rtb, "Select Single Increment", 14))
                    L.err(location, "Failed to add heading.");

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "The more interesting toggle. You can draw a specific increment, for a " +
                    "specific prime. A single divide attempt, with rollover, until success. " +
                    "When navigating from prime-to-prime, the increment setting is maintained " +
                    "so you can do things like compare 97 / 24 with 73 / 24 or 137 / 24 readily."
                );
                rtb.Document.Blocks.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Inlines.Add("To restore normal function, set to \"All\". When navigating between primes " + 
                    "it is entirely possible to have a chosen increment, that is not possible with " + 
                    "the newly selected prime. When this happens, the app will automatically default " + 
                    "to all, and proceed with loading the new prime."
                );
                rtb.Document.Blocks.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Inlines.Add("For clarity, the number of increments possible for a prime, is described as " + 
                    "(P / 2) | 0, which is half-round-down. For reasons described elsewhere, the " + 
                    "minimum supported increment is 1, and the maximum *possible* increment is P-1. " + 
                    "Where the first half, (P / 2) | 0, draws patterns one direction around the circle, " + 
                    "and the other half, draws the same patterns in the opposite clock-direction. To " + 
                    "prevent redundant \"patterns\", only one direction is drawn. The increments are such."
                );
                rtb.Document.Blocks.Add(paragraph);

                if (!addHeader(rtb, "Use Same Color", 14))
                    L.err(location, "Failed to add heading.");

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "Toggles whether or not to alternate colors on a per-increment (or series) basis. " + 
                    "Using the same color is useful, when looking at the overall pattern of small primes, " + 
                    "or in keeping a common color when checking an increment across primes. When you " + 
                    "get to numbers larger than about 50, using a single color is less effective " + 
                    "for drawing all increments."
                );
                rtb.Document.Blocks.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "Change takes effect upon next prime or increment selection."
                );
                rtb.Document.Blocks.Add(paragraph);

                if (!addHeader(rtb, "Line Delay - Beta", 14))
                    L.err(location, "Failed to add heading.");

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "This is a beta-feature, new and less-tested. It is supposed to allow slowing down " + 
                    "or speeding up the draw process. Low numbers draw faster. This feature is beta for " +
                    "still being intermittent. The interval is dispatched to a timer responsible for " + 
                    "adding one line at-a-time to the UI. However, if you slow the time to a couple-thousand " + 
                    "(alleged) milliseconds, you can clearly see about ten-frames being batched. I appologize " +
                    "for this, I never finished chasing down quirks in the UI throttle. If you slow-down, and " + 
                    "it starts running fast after reloads, setting the button again should work."
                );
                rtb.Document.Blocks.Add(paragraph);

                if (!addHeader(rtb, "Stroke Width", 14))
                    L.err(location, "Failed to add heading.");

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "Sets the brushstroke width, used to draw lines. This only takes effect upon the next prime " + 
                    "or increment selection. For nerd reasons, loading thousands of lines to the UI is too " + 
                    "burdensome to do without queueing frames. As such, all of the lines are pre-calculated " +
                    "and pre-drawn, then handed off as a stack. By the time you see the image begin, it is like " + 
                    "a video of everything just calculated. It is generally too late to see the new stroke until redraw."
                );
                rtb.Document.Blocks.Add(paragraph);

                if (!addHeader(rtb, "Color Options", 14))
                    L.err(location, "Failed to add heading.");

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "A panel to select which colors to include when drawing. Colors are used in the order shown. " + 
                    "If there are too-few colors selected, they will be recycled."
                );


                // Disclaimer
                rtb.AppendText("\n");
                if (!addHeader(rtb, "Disclaimer", 14))
                    L.err(location, "Failed to add heading.");

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "I make no pretense that everything in this app is accurate 100%. It was a solo-project, that " + 
                    "has been rehashed a few times. It is accurate to the best of my knowledge."
                );
                rtb.Document.Blocks.Add(paragraph);

                rtb.AppendText("\n\n");


                rtb.Selection.Select(rtb.Document.ContentStart, rtb.Document.ContentStart);

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
