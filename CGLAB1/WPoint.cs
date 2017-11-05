using System.Windows;
using System.Windows.Shapes;

namespace CGLAB1
{
    public class WPoint
    {
        public Point Ghost { get; set; }
        public Rectangle Shell { get; set; }

        public WPoint(Point point)
        {
            Ghost = point;
        }

        public Point ToPoint()
        {
            Point p = new Point();
            p.X = Ghost.X;
            p.Y = Ghost.Y;
            return p;
        }

        public override string ToString()
        {
            return Ghost.X.ToString() + ", " + Ghost.Y.ToString();
        }
    }
}