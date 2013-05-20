using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp
{
    internal class MathHelper
    {
        internal static double DegToRad(double degrees)
        {
            return (degrees / 180.0) * Math.PI;
        }

        internal static double mm_converter(double length)
        {
            return length / 3.543307;
        }
        internal static double calculate_distance(double x1, double y1, double z1, double x2, double y2, double z2)
        {

            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2) + Math.Pow(z2 - z1, 2));
        }

    }
}
