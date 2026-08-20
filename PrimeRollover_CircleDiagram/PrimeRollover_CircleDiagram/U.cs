//Prime Rollover - Circle Diagram
//(c) 2026 - TimeWarpToaster

//https://www.gnu.org/licenses/gpl-3.0.html

using System.Windows.Media;
using System.Threading;

namespace PrimeRollover_CircleDiagram
{
    // Utility Static
    public static class U
    {
        public const string CLASSNAME = "U";


        public static PointCollection pts { get; set; }

        public static Thread graphThread = null;

    }
}
