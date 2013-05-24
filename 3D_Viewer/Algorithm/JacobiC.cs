using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smileUp.Algorithm
{
    //http://www.c-sharpcorner.com/Forums/Thread/39095/
    public static class JacobiC
    {

        private const int MAX_ROTATIONS = 60;

        public static bool __jacobi(double[,] a, double[] w, double[,] v)
        {
            int N = w.Length;
            int i, j, k, iq, ip;
            double tresh, theta, tau, t, sm, s, h, g, c;
            double tmp;
            double[] b = new double[N];
            double[] z = new double[N];

            // initialize
            for (ip = 0; ip < N; ip++)
            {
                for (iq = 0; iq < N; iq++) v[ip, iq] = 0.0;
                v[ip, ip] = 1.0;
            }
            for (ip = 0; ip < N; ip++)
            {
                b[ip] = w[ip] = a[ip, ip];
                z[ip] = 0.0;
            }

            // begin rotation sequence
            for (i = 0; i < MAX_ROTATIONS; i++)
            {
                sm = 0.0;
                for (ip = 0; ip < 2; ip++)
                {
                    for (iq = ip + 1; iq < N; iq++) sm += Math.Abs(a[ip, iq]);
                }
                if (sm == 0.0) break;

                if (i < 4) tresh = 0.2 * sm / (9);
                else tresh = 0.0;

                for (ip = 0; ip < 2; ip++)
                {
                    for (iq = ip + 1; iq < N; iq++)
                    {
                        g = 100.0 * Math.Abs(a[ip, iq]);
                        if (i > 4 && (Math.Abs(w[ip]) + g) == Math.Abs(w[ip])
                            && (Math.Abs(w[iq]) + g) == Math.Abs(w[iq]))
                        {
                            a[ip, iq] = 0.0;
                        }
                        else if (Math.Abs(a[ip, iq]) > tresh)
                        {
                            h = w[iq] - w[ip];
                            if ((Math.Abs(h) + g) == Math.Abs(h)) t = (a[ip, iq]) / h;
                            else
                            {
                                theta = 0.5 * h / (a[ip, iq]);
                                t = 1.0 / (Math.Abs(theta) + Math.Sqrt(1.0 + theta * theta));
                                if (theta < 0.0) t = -t;
                            }
                            c = 1.0 / Math.Sqrt(1 + t * t);
                            s = t * c;
                            tau = s / (1.0 + c);
                            h = t * a[ip, iq];
                            z[ip] -= h;
                            z[iq] += h;
                            w[ip] -= h;
                            w[iq] += h;
                            a[ip, iq] = 0.0;
                            for (j = 0; j < ip - 1; j++)
                            {
                                g = a[j, ip];
                                h = a[j, iq];
                                a[j, ip] = g - s * (h + g * tau);
                                a[j, iq] = h + s * (g - h * tau);
                            }
                            for (j = ip + 1; j < iq - 1; j++)
                            {
                                g = a[ip, j];
                                h = a[j, iq];
                                a[ip, j] = g - s * (h + g * tau);
                                a[j, iq] = h + s * (g - h * tau);
                            }
                            for (j = iq + 1; j < N; j++)
                            {
                                g = a[ip, j];
                                h = a[iq, j];
                                a[ip, j] = g - s * (h + g * tau);
                                a[iq, j] = h + s * (g - h * tau);
                            }
                            for (j = 0; j < N; j++)
                            {
                                g = v[j, ip];
                                h = v[j, iq];
                                v[j, ip] = g - s * (h + g * tau);
                                v[j, iq] = h + s * (g - h * tau);
                            }
                        }
                    }
                }

                for (ip = 0; ip < N; ip++)
                {
                    b[ip] += z[ip];
                    w[ip] = b[ip];
                    z[ip] = 0.0;
                }
            }

            if (i >= MAX_ROTATIONS)
                return false;

            // sort eigenfunctions
            for (j = 0; j < N; j++)
            {
                k = j;
                tmp = w[k];
                for (i = j; i < N; i++)
                {
                    if (w[i] >= tmp)
                    {
                        k = i;
                        tmp = w[k];
                    }
                }
                if (k != j)
                {
                    w[k] = w[j];
                    w[j] = tmp;
                    for (i = 0; i < N; i++)
                    {
                        tmp = v[i, j];
                        v[i, j] = v[i, k];
                        v[i, k] = tmp;
                    }
                }
            }

            // VTK addition to original Numerical Recipes code:
            //    insure eigenvector consistency (i.e., Jacobi can compute
            //    vectors that are negative of one another (.707,.707,0) and
            //    (-.707,-.707,0). This can reek havoc in
            //    hyperstreamline/other stuff. We will select the most
            //    positive eigenvector.
            int numPos;
            for (j = 0; j < N; j++)
            {
                for (numPos = 0, i = 0; i < N; i++) if (v[i, j] >= 0.0) numPos++;
                if (numPos < 2) for (i = 0; i < N; i++) v[i, j] *= -1.0;
            }

            return true;
        }


        public static bool eigen3(double[,] m, double[] eig_vals, double[][] eig_vecs)
        {
            double[,] a = new double[3, 3];
            double[,] v = new double[3, 3];
            double[] w = new double[3];
            int i, j;

            for (i = 0; i < 3; i++) for (j = 0; j < 3; j++) a[i, j] = m[i, j];

            bool result = __jacobi(a, w, v);
            if (result)
            {
                for (i = 0; i < 3; i++) eig_vals[i] = w[i];

                for (i = 0; i < 3; i++) for (j = 0; j < 3; j++) eig_vecs[i][j] = v[j, i];
            }

            return result;
        }

        public static bool eigen4(double[,] m, double[] eig_vals, double[][] eig_vecs)
        {
            double[,] a = new double[4, 4];
            double[,] v = new double[4, 4];
            double[] w = new double[4];
            int i, j;

            for (i = 0; i < 4; i++) for (j = 0; j < 4; j++) a[i, j] = m[i, j];

            bool result = __jacobi(a, w, v);
            if (result)
            {
                for (i = 0; i < 4; i++) eig_vals[i] = w[i];

                for (i = 0; i < 4; i++) for (j = 0; j < 4; j++) eig_vecs[i][j] = v[j, i];
            }

            return result;
        }

    } // end Jacobi class
}
