using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kNN
{
    public class Point 
    {
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(double x, double y, byte c)
        {
            this.x = x;
            this.y = y;
            this.c = c;
        }

        public Point(int iX, int iY, byte c)
        {
            this.iX = iX;
            this.iY = iY;
            this.c = c;
        }

        public int CompareTo(Point first, Point second)
        {
            if (first != null && second != null)
            {
                // We can compare both properties.
                if (first.x == second.x
                    && first.y == second.y)
                {
                    return 0;
                }

                if (first.x < second.x
                    && first.y < second.y)
                {
                    return -1;
                }

                if (first.x > second.x
                    && first.y > second.y)
                {
                    return 1;
                }
            }

            if (first == null && second == null)
            {
                // We can't compare any properties, so they are essentially equal.
                return 0;
            }

            if (first != null)
            {
                // Only the first instance is not null, so prefer that.
                return -1;
            }

            // Only the second instance is not null, so prefer that.
            return 1;
        }

        public double d;
        public double x;
        public double y;
        public byte c;
        public int iX;
        public int iY;
        public Color color;
    }
}
