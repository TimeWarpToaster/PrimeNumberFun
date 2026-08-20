namespace PrimeRollover_CircleDiagram
{
    class Artifacts
    {

        /*// First drawing, original app
        public bool drawPolygon()
        {
            const string location = CLASSNAME + ".drawPolygon";
            bool retVal = false;
            try
            {// Draws polygon on grid from a PointCollection (artifact)
                App.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        Polygon myPolygon = new Polygon();
                        myPolygon.Stroke = System.Windows.Media.Brushes.Black;
                        myPolygon.Fill = System.Windows.Media.Brushes.Transparent;
                        myPolygon.StrokeThickness = this.strokeWidth;
                        myPolygon.HorizontalAlignment = HorizontalAlignment.Left;
                        myPolygon.VerticalAlignment = VerticalAlignment.Top;
                        myPolygon.Points = U.pts;

                        myGrid.Children.Add(myPolygon);
                    }
                    catch (Exception ex)
                    {
                        L.ex(location, ex);
                    }
                });

                // Flag success for dispatching
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }*/
    }
}
