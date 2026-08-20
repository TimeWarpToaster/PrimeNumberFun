//Prime Rollover - Circle Diagram
//(c) 2026 - TimeWarpToaster

//https://www.gnu.org/licenses/gpl-3.0.html

using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PrimeRollover_CircleDiagram
{
    public static class Primer
    {
        public const string CLASSNAME = "Primer";

        private static bool addHeader(RichTextBox rtb, string header, double fontSize)
        {
            const string location = CLASSNAME + ".addHeader";
            bool retVal = false;
            try
            {
                // Validate input
                if (rtb == null)
                {
                    L.err(location, "View was null.");
                    return retVal;
                }
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

        private static bool addResourceImage(RichTextBox rtb, string path, int width, int height)
        {
            const string location = CLASSNAME + ".addResourceImage";
            bool retVal = false;
            try
            {
                // Validate input
                if (rtb == null)
                {
                    L.err(location, "View was null.");
                    return retVal;
                }
                if (path == null || path.Length == 0)
                {
                    L.err(location, "Input path was null or empty.");
                    return retVal;
                }

                Uri resource = new Uri(@path, UriKind.Relative);
                if (resource == null)
                {
                    return retVal;
                }

                BitmapImage bmp = new BitmapImage(resource);
                if (bmp == null || bmp.Width == 0 || bmp.Height == 0)
                {
                    return retVal;
                }
                Image img = new Image
                {
                    Source = bmp
                };
                if (width > 0) img.Width = width;
                if (height > 0) img.Height = height;

                rtb.CaretPosition = rtb.Document.ContentEnd;
                TextPointer position = rtb.CaretPosition.GetInsertionPosition(LogicalDirection.Forward);
                InlineUIContainer container = new InlineUIContainer(img, position);
                rtb.CaretPosition = container.ElementEnd;

                // Flag success for completing
                retVal = true;
            }
            catch (Exception ex)
            {
                // Quietly ignore missing resources
                //L.ex(location, ex);
            }
            return retVal;
        }

        public static bool setPrimer(RichTextBox rtb)
        {
            const string location = CLASSNAME + ".setPrimer";
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

                Paragraph title = new Paragraph();

                if (!addHeader(rtb, "Prime Rollover", 18))
                {
                    L.err(location, "Failed to add title.");
                }
                if (!addHeader(rtb, "Primer", 18))
                {
                    L.err(location, "Failed to add subtitle.");
                }

                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "Leaving out notions of a revolving computer stack, Prime Rollover is the " + 
                    "process of attempting to divide a prime number, and allowing it to rollover, " + 
                    "until the division is successful."
                );
                rtb.Document.Blocks.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "In math terms, it is like saying it is okay to divide a prime and get a " + 
                    "remainder, and instead of failing, you add prime to the remainder and try " + 
                    "again as many times as it takes until succeeding."
                );
                rtb.Document.Blocks.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "The process that makes Prime Rollover possible, is a combination of the " + 
                    "indivisibility of primes, and the fact that the product of multiplying " + 
                    "any number by N, is also evenly divisible by N."
                );
                rtb.Document.Blocks.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "This is almost self-evident:"
                );
                rtb.Document.Blocks.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "\t(P x N) / N = P"
                );
                rtb.Document.Blocks.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "This seemingly useless, albeit obvious, piece of information, is the " + 
                    "same piece of information that makes Prime Rollover not only possible, " + 
                    "but absolute. There is no way to increment by N over a span of P, rolling " + 
                    "over and continuing until reaching the start, without requiring N loops " + 
                    "across the span of P, and touching P points along the way, which also " + 
                    "happen to be unique points (or all of them)."
                );
                rtb.Document.Blocks.Add(paragraph);

                rtb.AppendText("\n");

                // Add two-images of 5
                string path1 = "./Images/PR5d1Lbl400w.bmp";
                if (!addResourceImage(rtb, path1, 0, 300))// measure off for even height, scale width appropriately
                {
                    L.err(location, "Failed to add image (" + path1 + ").");
                }

                rtb.AppendText("  ");

                string path2 = "./Images/PR5d2Lbl400w.bmp";
                if (!addResourceImage(rtb, path2, 0, 300))
                {
                    L.err(location, "Failed to add image (" + path2 + ").");
                }


                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "Take, for example, the images of five. There are two-possible increments, " + 
                    "that result in optically unique patterns, 1 and 2. If you increment by 1, " + 
                    "it takes 1 trip around P points, to touch all points and return. Because, " + 
                    "(P(5) x 1) / 1 = P(5). If you increment by 2, it takes 2 trips around P " + 
                    "points, to touch each point associated with P."
                );
                rtb.Document.Blocks.Add(paragraph);


                rtb.AppendText("\n\n");

                // Move cursor to top
                rtb.CaretPosition = rtb.Document.ContentStart.GetInsertionPosition(LogicalDirection.Forward);

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

