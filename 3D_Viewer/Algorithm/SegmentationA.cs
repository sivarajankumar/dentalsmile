using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace smileUp.Algorithm
{
    class SegmentationA
    {
        //ref: 060-V1-E165.pdf
        public void doIt()
        {
            createOcclusalPlane();
            //drawArch(); //using cubicSpline
            //detectTeeth(); //convert to 2D and detect 
        }

        void createOcclusalPlane(){
            //find 3 highest point from 3parts
            Point3D p0 = new Point3D();
            Point3D p1 = new Point3D();
            Point3D p2 = new Point3D();

            Vector3D n = CalculateNormal(p0, p1, p2);
            double d = Vector3D.DotProduct(new Vector3D(p2.X, p2.Y, p2.Z), n);
            Vector3D pPlane = Vector3D.Multiply(d, n);
            //to Point3D
            
            //d1 = n * p2;
            //Point3D p = d1 / n; //d1 = n* p ;
            //float dist = Vector3D.dotProduct(p.normal, (Vector3D.Subtract(point, p.point)));
        }
        
        private Vector3D CalculateNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            //(p1 -p2 * p0- p2) / (p1-p2 * p0-p2)
            Vector3D v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            Vector3D v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }

        public void CubicSpline()
        {
            Dictionary<int, double>  x = new Dictionary<int, double> ();
            Dictionary<int, double>  y = new Dictionary<int, double> ();
            Dictionary<int, double>  h = new Dictionary<int, double> ();  //h(i) = x(i + 1) - x(i);
            for (int i = 0; i < (x.Count-1); i++)
            {
                h.Add(i, (x.ElementAt(i+1).Value - x.ElementAt(i).Value));
            }

            Dictionary<int, double>  A = new Dictionary<int, double> ();
            Dictionary<int, double> C = new Dictionary<int, double>();
            Dictionary<int, double> B = new Dictionary<int, double>();
            Dictionary<int, double> D = new Dictionary<int, double>();
            for (int i = 1; i < (x.Count - 1); i++)
            {
                D.Add(i, 2 * (h.ElementAt(i-1).Value + h.ElementAt(i).Value));
                A.Add(i, h.ElementAt(i).Value);
                B.Add(i,h.ElementAt(i - 1).Value);
                //j = i - 1 'Shift to TDMA coordinate system
                //D(j) = 2 * (h(i - 1) + h(i))
                //A(j) = h(i) 'Ignore A(norder)
                //B(j) = h(i - 1) 'Ignore B(0) 
            }

            for (int i = 1; i < (x.Count - 1); i++)
            {
                C.Add(i,6 * ((y.ElementAt(i + 1).Value - y.ElementAt(i).Value) / h.ElementAt(i).Value) - ((y.ElementAt(i).Value - y.ElementAt(i-1).Value) / h.ElementAt(i-1).Value));
                //C(j) = 6 * ((y(i + 1) - y(i)) / h(i) - (y(i) - y(i - 1)) / h(i - 1)) 
            }
            
            int ntdma = x.Count - 2;
            for (int i = 1; i < (ntdma); i++)
            {
                double R = B.ElementAt(i).Value / D.ElementAt(i - 1).Value;
                D.Add(i, D.ElementAt(i).Value - R * A.ElementAt(i-1).Value);
                C.Add(i, C.ElementAt(i).Value - R * C.ElementAt(i - 1).Value);
                // R = B(i) / D(i - 1)
                //D(i) = D(i) - R * A(i - 1)
                //C(i) = C(i) - R * C(i - 1) 
            }
            C.Add(ntdma, C.ElementAt(ntdma).Value / D.ElementAt(ntdma).Value );
            //C(ntdma) = C(ntdma) / D(ntdma)
            int nstep = 0;
            for (int i = ntdma - 1; i == 1; i--)
            {
                C.Add(i, C.ElementAt(i).Value - A.ElementAt(i).Value * C.ElementAt(i + 1).Value / D.ElementAt(i).Value);
                //C(i) = (C(i) - A(i) * C(i + 1)) / D(i) 
                nstep ++;
            }

            Dictionary<int, double> S = new Dictionary<int,double>();
            for (int i = 1; i < (x.Count - 1); i++)
            {
                S.Add(i, C.ElementAt(i).Value);
            }
            S.Add(1, 0);

            for (int i = 1; i < (x.Count - 1); i++)
            {
                A.Add(i, (S.ElementAt(i + 1).Value - S.ElementAt(i).Value) / (6 * h.ElementAt(i).Value));
                B.Add(i, S.ElementAt(i).Value / 2);
                C.Add(i, (y.ElementAt(i + 1).Value - y.ElementAt(i).Value) / h.ElementAt(i).Value - (2 * h.ElementAt(i).Value * S.ElementAt(i).Value + h.ElementAt(i).Value * S.ElementAt(i + 1).Value) / 6);
                D.Add(i, y.ElementAt(i).Value);
                //A(i) = (S(i + 1) - S(i)) / (6 * h(i))
                //B(i) = S(i) / 2
                //C(i) = (y(i + 1) - y(i)) / h(i) - (2 * h(i) * S(i) + h(i) * S(i + 1)) / 6
                //D(i) = y(i) 
            }

            //plot
            for (int i = 1; i < (x.Count - 1); i++)
            {
                for (int j = 1; j < nstep; j++)
                {
                    //xs = x(i) + (h(i) / nstep) * (j - 1)
                    //ys = A(i) * (xs - x(i)) ^ 3 + B(i) * (xs - x(i)) ^ 2 + C(i) * (xs - x(i)) + D(i)
                }
            }
        }
    }
}
