//Prime Rollover - Circle Diagram
//(c) 2026 - TimeWarpToaster

//https://www.gnu.org/licenses/gpl-3.0.html

using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace PrimeRollover_CircleDiagram
{
    public static class ColorOpts
    {
        public const string CLASSNAME = "ColorOpts";

        public static List<Brush> brushes { get; set; }

        public static List<Pen> pens { get; set; }

        public static bool isAqua = true;
        public static bool isBisque = true;
        public static bool isBurlyWood = true;
        public static bool isCadetBlue = true;
        public static bool isChartreuse = true;
        public static bool isChocolate = true;
        public static bool isCornflowerBlue = true;
        public static bool isCornsilk = true;
        public static bool isDarkGoldenrod = true;
        public static bool isDarkOrchid = true;
        public static bool isDarkOrange = true;
        public static bool isDeepSkyBlue = true;
        public static bool isDodgerBlue = true;
        public static bool isForestGreen = true;
        public static bool isGainsboro = true;
        public static bool isGold = true;
        public static bool isKhaki = true;
        public static bool isMagenta = true;
        public static bool isMediumVioletRed = true;
        public static bool isMoccasin = true;
        public static bool isOrangeRed = true;
        public static bool isRosyBrown = true;
        public static bool isSteelBlue = true;
        public static bool isSlateBlue = true;
        public static bool isTan = true;
        public static bool isTomato = true;


        public static bool init()
        {
            const string location = CLASSNAME + ".init";
            bool retVal = false;
            try
            {

                ColorOpts.brushes = new List<Brush>();
                if (isAqua) ColorOpts.brushes.Add(Brushes.Aqua);
                if (isBisque) ColorOpts.brushes.Add(Brushes.Bisque);
                if (isBurlyWood) ColorOpts.brushes.Add(Brushes.BurlyWood);
                if (isCadetBlue) ColorOpts.brushes.Add(Brushes.CadetBlue);
                if (isChartreuse) ColorOpts.brushes.Add(Brushes.Chartreuse);
                if (isChocolate) ColorOpts.brushes.Add(Brushes.Chocolate);
                if (isCornflowerBlue) ColorOpts.brushes.Add(Brushes.CornflowerBlue);
                if (isCornsilk) ColorOpts.brushes.Add(Brushes.Cornsilk);
                if (isDarkGoldenrod) ColorOpts.brushes.Add(Brushes.DarkGoldenrod);
                if (isDarkOrange) ColorOpts.brushes.Add(Brushes.DarkOrange);
                if (isDeepSkyBlue) ColorOpts.brushes.Add(Brushes.DeepSkyBlue);
                if (isDodgerBlue) ColorOpts.brushes.Add(Brushes.DodgerBlue);
                if (isForestGreen) ColorOpts.brushes.Add(Brushes.ForestGreen);
                if (isGainsboro) ColorOpts.brushes.Add(Brushes.Gainsboro);
                if (isGold) ColorOpts.brushes.Add(Brushes.Gold);
                if (isKhaki) ColorOpts.brushes.Add(Brushes.Khaki);
                if (isMagenta) ColorOpts.brushes.Add(Brushes.Magenta);
                if (isMediumVioletRed) ColorOpts.brushes.Add(Brushes.MediumVioletRed);
                if (isMoccasin) ColorOpts.brushes.Add(Brushes.Moccasin);
                if (isOrangeRed) ColorOpts.brushes.Add(Brushes.OrangeRed);
                if (isRosyBrown) ColorOpts.brushes.Add(Brushes.RosyBrown);
                if (isSteelBlue) ColorOpts.brushes.Add(Brushes.SteelBlue);
                if (isDarkOrchid) ColorOpts.brushes.Add(Brushes.DarkOrchid);
                if (isSlateBlue) ColorOpts.brushes.Add(Brushes.SlateBlue);
                if (isTan) ColorOpts.brushes.Add(Brushes.Tan);
                if (isTomato) ColorOpts.brushes.Add(Brushes.Tomato);
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }
    }
}
