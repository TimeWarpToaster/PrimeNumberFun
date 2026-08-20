//Prime Rollover - Circle Diagram
//(c) 2026 - TimeWarpToaster

//https://www.gnu.org/licenses/gpl-3.0.html

using System;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PrimeRollover_CircleDiagram
{
    public static class About
    {
        public const string CLASSNAME = "About";

        public static bool setAbout(RichTextBox rtb)
        {
            const string location = CLASSNAME + ".setAbout";
            bool retVal = false;
            try
            {
                if (rtb == null) 
                {
                    L.err(location, "About view was null.");
                    return retVal;
                }


                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "Prime Rollover - Circle Diagram"
                );
                rtb.Document.Blocks.Add(paragraph);

                paragraph = new Paragraph();
                paragraph.Inlines.Add(
                    "(c) 2026 TimeWarpToaster"
                );
                rtb.Document.Blocks.Add(paragraph);

                rtb.AppendText("\n\n");

                rtb.AppendText(License.license);
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
