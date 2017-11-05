using System.Windows;

namespace CGLAB1
{
    public static class Bezier
    {
        public static WPoint CalculateCubicBezierPoint(float t, WPoint wp1, WPoint wp2, WPoint wp3, WPoint wp4)
        {
            Point p1 = wp1.ToPoint();
            Point p2 = wp2.ToPoint();
            Point p3 = wp3.ToPoint();
            Point p4 = wp4.ToPoint();
            Point p = p1;

            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            p.X *= uuu;
            p.Y *= uuu;
            
            p.X += 3 * uu * t * p2.X;
            p.Y += 3 * uu * t * p2.Y;
            
            p.X += 3 * u * tt * p3.X;
            p.Y += 3 * u * tt * p3.Y;

            p.X += ttt * p4.X;
            p.Y += ttt * p4.Y;

            return new WPoint(p);
        }
    }
}