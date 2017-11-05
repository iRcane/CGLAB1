using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static CGLAB1.Bezier;

namespace CGLAB1
{
    public class DrawModule
    {
        private Canvas canvas;

        public DrawModule(Canvas c)
        {
            canvas = c;
        }

        public void DrawPoint(WPoint wp, Brush b)
        {
            wp.Shell = new Rectangle();
            wp.Shell.Stroke = Brushes.Black;
            wp.Shell.Fill = b;

            Canvas.SetTop(wp.Shell, wp.Ghost.Y - 3);
            Canvas.SetLeft(wp.Shell, wp.Ghost.X - 3);
            wp.Shell.HorizontalAlignment = HorizontalAlignment.Left;
            wp.Shell.VerticalAlignment = VerticalAlignment.Top;
            wp.Shell.Height = 6;
            wp.Shell.Width = 6;

            canvas.Children.Add(wp.Shell);
        }

        public void DrawCubicBezier(WChain wc)
        {
            int SEGMENT_COUNT = 30;
            wc.CBShell.Points.Clear();
            wc.CBShell.Points.Add(wc.List[0].ToPoint());

            for (int i = 0; i < wc.List.Count; i+=3)
            {
                if ((wc.List.Count - i) < 4)
                {
                    //заглушка на остатки точек, не вошедших в путь Безье
                    Polyline pl = new Polyline();
                    for (int k = i; k < wc.List.Count; k++)
                    {
                        pl.Points.Add(wc.List[k].ToPoint());
                    }
                    wc.Dump.Add(pl);

                    pl.Stroke = Brushes.Black;
                    if (!canvas.Children.Contains(pl))
                        canvas.Children.Add(pl);

                    break;
                }

                for (int j = 1; j <= SEGMENT_COUNT; ++j)
                {
                    float t = j / (float)SEGMENT_COUNT;
                    wc.CBShell.Points.Add(CalculateCubicBezierPoint(t, wc.List[i],
                        wc.List[i + 1], wc.List[i + 2], wc.List[i + 3]).ToPoint());
                }
            }

            wc.CBShell.Stroke = Brushes.Black;
            if (!canvas.Children.Contains(wc.CBShell))
                canvas.Children.Add(wc.CBShell);
        }

        public void DrawConnections(WChain wc)
        {
            wc.Shell.Points.Clear();
            foreach(WPoint wp in wc.List)
            {
                wc.Shell.Points.Add(wp.ToPoint());
            }

            wc.Shell.Stroke = Brushes.Black;
            if (!canvas.Children.Contains(wc.Shell))
                canvas.Children.Add(wc.Shell);
        }

        public void DrawAdditionalPoint(WChain wc, WPoint wp)
        {
            Polyline pl = new Polyline();

            pl.Points.Add(wc.List.Last().ToPoint());
            pl.Points.Add(wc.List[wc.List.Count - 2].ToPoint());

            wc.Dump.Add(pl); //не очень изящное решение, still it works

            pl.Stroke = Brushes.Black;
            if (!canvas.Children.Contains(pl))
                canvas.Children.Add(pl);
        }

        public void ClearConnections(WChain wc, Polyline shell)
        {
            canvas.Children.Remove(shell);
            foreach (Polyline pl in wc.Dump)
            {
                canvas.Children.Remove(pl);
            }
        } 

        public void ClearConnections(WChain wc)
        {
            ClearConnections(wc, wc.Shell);
            ClearConnections(wc, wc.CBShell);
            ClearConnections(wc, wc.QBShell);
        }

        public void ClearPoint(WPoint wp)
        {
            if (canvas.Children.Contains(wp.Shell))
                canvas.Children.Remove(wp.Shell);
        }
    }
}