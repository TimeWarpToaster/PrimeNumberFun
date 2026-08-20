//Prime Rollover - Circle Diagram
//(c) 2026 - TimeWarpToaster

//https://www.gnu.org/licenses/gpl-3.0.html

using System;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace PrimeRollover_CircleDiagram
{

    public class VisualHost : FrameworkElement
    {
        public const string CLASSNAME = "VisualHost";

        private readonly VisualCollection _children = null;

        private readonly ConcurrentQueue<DrawingVisual> _queuedVisuals = new ConcurrentQueue<DrawingVisual>();

        private readonly DispatcherTimer _timer;

        public int Count => _children.Count;
        public int CountQueue => _queuedVisuals.Count;


        public VisualHost()
        {
            const string location = CLASSNAME + ".Constructor";
            try
            {
                _children = new VisualCollection(this);
                _timer = new DispatcherTimer(DispatcherPriority.Render);
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        protected override int VisualChildrenCount => _children.Count;

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _children[index];
        }


        public bool Add(DrawingVisual visual)
        {
            const string location = CLASSNAME + ".Add";
            bool retVal = false;
            try
            {
                if (visual == null)
                {
                    return retVal;
                }

                _queuedVisuals.Enqueue(visual);
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public int clear()
        {
            const string location = CLASSNAME + ".clear";
            int retVal = 0;
            try
            {
                if (_timer.IsEnabled) _timer.Stop();

                while (_queuedVisuals.TryDequeue(out _)) { }

                if (_children == null) return retVal;
                int cntOldItems = _children.Count;
                _children.Clear();

                retVal = cntOldItems;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool DrawLine(Point pt1, Point pt2, Pen pen)
        {
            const string location = CLASSNAME + ".Add";
            bool retVal = false;
            try
            {
                if (pt1 == null || pt2 == null || pen == null)
                {
                    // Too frequent to log if error
                    return retVal;
                }

                DrawingVisual visual = new DrawingVisual();
                using (DrawingContext drawing = visual.RenderOpen())
                {
                    try
                    {
                        drawing.DrawLine(pen, pt1, pt2);
                    }
                    catch (Exception exAdd)
                    {
                        L.err(location, "Failed to draw object: " + exAdd.Message);
                    }
                }
                _queuedVisuals.Enqueue(visual);
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public int getTimerDelay()
        {
            return (int)_timer.Interval.TotalMilliseconds;
        }

        public bool setTimerDelay(int delayMs)
        {
            const string location = CLASSNAME + ".setTimerDelay";
            bool retVal = false;
            try
            {
                if (delayMs < 0) return retVal;
                _timer.Stop();
                _timer.Interval = TimeSpan.FromMilliseconds(delayMs);
                _timer.Start();
                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        private void loadNextVisual(object sender, EventArgs e)
        {
            const string location = CLASSNAME + ".loadNextVisual";
            try
            {
                if (_queuedVisuals.IsEmpty)
                {
                    _timer.Stop();
                    return;
                }

                DrawingVisual visual;
                _queuedVisuals.TryDequeue(out visual);
                if (visual == null)
                {
                    L.err(location, "Stopping timer for null visual.");
                    _timer.Stop();
                    return;
                }
                _children.Add(visual);
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
        }

        public bool StartQueueAnimation()
        {
            const string location = CLASSNAME + ".StartQueueAnimation";
            bool retVal = false;
            try
            {
                loadNextVisual(new object(), new EventArgs());
                _timer.Tick += loadNextVisual;
                _timer.Start();

                retVal = true;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public bool StopQueueAnimation()
        {
            const string location = CLASSNAME + ".StopQueueAnimation";
            bool retVal = false;
            try
            {
                _timer.Stop();
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }


    }
}
