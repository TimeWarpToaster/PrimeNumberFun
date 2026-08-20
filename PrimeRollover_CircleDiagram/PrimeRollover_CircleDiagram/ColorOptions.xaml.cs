//Prime Rollover - Circle Diagram
//(c) 2026 - TimeWarpToaster

//https://www.gnu.org/licenses/gpl-3.0.html

using System;
using System.Windows;
using System.Windows.Media;

namespace PrimeRollover_CircleDiagram
{
    /// <summary>
    /// Interaction logic for ColorOptions.xaml
    /// </summary>
    public partial class ColorOptions : Window
    {
        public const string CLASSNAME = "ColorOptions";
        public ColorOptions()
        {
            const string location = CLASSNAME + ".Constructor";
            try
            {

                InitializeComponent();

                if (!this.init())
                {
                    L.err(location, "Failed to initialize Color Options window.");
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
            bool returnVal = false;
            try
            {


                swatchAqua.Fill = Brushes.Aqua;
                swatchBisque.Fill = Brushes.Bisque;
                swatchBurlyWood.Fill = Brushes.BurlyWood;
                swatchCadetBlue.Fill = Brushes.CadetBlue;
                swatchChartreuse.Fill = Brushes.Chartreuse;
                swatchChocolate.Fill = Brushes.Chocolate;
                swatchCornflowerBlue.Fill = Brushes.CornflowerBlue;
                swatchCornsilk.Fill = Brushes.Cornsilk;
                swatchDarkGoldenrod.Fill = Brushes.DarkGoldenrod;
                swatchDarkOrange.Fill = Brushes.DarkOrange;
                swatchDarkOrchid.Fill = Brushes.DarkOrchid;
                swatchDeepSkyBlue.Fill = Brushes.DeepSkyBlue;
                swatchDodgerBlue.Fill = Brushes.DodgerBlue;
                swatchForestGreen.Fill = Brushes.ForestGreen;
                swatchGainsboro.Fill = Brushes.Gainsboro;
                swatchGold.Fill = Brushes.Gold;
                swatchKhaki.Fill = Brushes.Khaki;
                swatchMagenta.Fill = Brushes.Magenta;
                swatchMediumVioletRed.Fill = Brushes.MediumVioletRed;
                swatchMoccasin.Fill = Brushes.Moccasin;
                swatchOrangeRed.Fill = Brushes.OrangeRed;
                swatchRosyBrown.Fill = Brushes.RosyBrown;
                swatchSlateBlue.Fill = Brushes.SlateBlue;
                swatchSteelBlue.Fill = Brushes.SteelBlue;
                swatchTan.Fill = Brushes.Tan;
                swatchTomato.Fill = Brushes.Tomato;



                cbAqua.IsChecked = ColorOpts.isAqua;
                cbBisque.IsChecked = ColorOpts.isBisque;
                cbBurlyWood.IsChecked = ColorOpts.isBurlyWood;
                cbCadetBlue.IsChecked = ColorOpts.isCadetBlue;
                cbChartreuse.IsChecked = ColorOpts.isChartreuse;
                cbChocolate.IsChecked = ColorOpts.isChocolate;
                cbCornflowerBlue.IsChecked = ColorOpts.isCornflowerBlue;
                cbCornsilk.IsChecked = ColorOpts.isCornsilk;
                cbDarkGoldenrod.IsChecked = ColorOpts.isDarkGoldenrod;
                cbDarkOrange.IsChecked = ColorOpts.isDarkOrange;
                cbDarkOrchid.IsChecked = ColorOpts.isDarkOrchid;
                cbDeepSkyBlue.IsChecked = ColorOpts.isDeepSkyBlue;
                cbDodgerBlue.IsChecked = ColorOpts.isDodgerBlue;
                cbForestGreen.IsChecked = ColorOpts.isForestGreen;
                cbGainsboro.IsChecked = ColorOpts.isGainsboro;
                cbGold.IsChecked = ColorOpts.isGold;
                cbKhaki.IsChecked = ColorOpts.isKhaki;
                cbMagenta.IsChecked = ColorOpts.isMagenta;
                cbMediumVioletRed.IsChecked = ColorOpts.isMediumVioletRed;
                cbMoccasin.IsChecked = ColorOpts.isMoccasin;
                cbOrangeRed.IsChecked = ColorOpts.isOrangeRed;
                cbRosyBrown.IsChecked = ColorOpts.isRosyBrown;
                cbSlateBlue.IsChecked = ColorOpts.isSlateBlue;
                cbSteelBlue.IsChecked = ColorOpts.isSteelBlue;
                cbTan.IsChecked = ColorOpts.isTan;
                cbTomato.IsChecked = ColorOpts.isTomato;

                returnVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return returnVal;
        }

        private void btnColorOptsAccept_Click(object sender, RoutedEventArgs e)
        {
            const string location = CLASSNAME + ".btnColorOptsAccept_Click";
            try
            {

                ColorOpts.isAqua = cbAqua.IsChecked == true;
                ColorOpts.isBisque = cbBisque.IsChecked == true;
                ColorOpts.isBurlyWood = cbBurlyWood.IsChecked == true;
                ColorOpts.isCadetBlue = cbCadetBlue.IsChecked == true;
                ColorOpts.isChartreuse = cbChartreuse.IsChecked == true;
                ColorOpts.isChocolate = cbChocolate.IsChecked == true;
                ColorOpts.isCornflowerBlue = cbCornflowerBlue.IsChecked == true;
                ColorOpts.isCornsilk = cbCornsilk.IsChecked == true;
                ColorOpts.isDarkGoldenrod = cbDarkGoldenrod.IsChecked == true;
                ColorOpts.isDarkOrange = cbDarkOrange.IsChecked == true;
                ColorOpts.isDarkOrchid = cbDarkOrchid.IsChecked == true;
                ColorOpts.isDeepSkyBlue = cbDeepSkyBlue.IsChecked == true;
                ColorOpts.isDodgerBlue = cbDodgerBlue.IsChecked == true;
                ColorOpts.isForestGreen = cbForestGreen.IsChecked == true;
                ColorOpts.isGainsboro = cbGainsboro.IsChecked == true;
                ColorOpts.isGold = cbGold.IsChecked == true;
                ColorOpts.isKhaki = cbKhaki.IsChecked == true;
                ColorOpts.isMagenta = cbMagenta.IsChecked == true;
                ColorOpts.isMediumVioletRed = cbMediumVioletRed.IsChecked == true;
                ColorOpts.isMoccasin = cbMoccasin.IsChecked == true;
                ColorOpts.isOrangeRed = cbOrangeRed.IsChecked == true;
                ColorOpts.isRosyBrown = cbRosyBrown.IsChecked == true;
                ColorOpts.isSlateBlue = cbSlateBlue.IsChecked == true;
                ColorOpts.isSteelBlue = cbSteelBlue.IsChecked == true;
                ColorOpts.isTan = cbTan.IsChecked == true;
                ColorOpts.isTomato = cbTomato.IsChecked == true;

                if (!ColorOpts.init())
                {
                    L.err(location, "Failed to reinitialize colors on accept of options.");
                }
                // try to close window?
                this.Close();
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }
    }
}
