using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EPTlab4
{
    class Model
    {
        double[,] x = {
            {-25, -25, 5, 5, -25, -25, 5, 5},
            {-30, 45, -30, 45, -30, 45, -30, 45},
            {-5, 5, 5, -5, 5, -5, -5, 5}};
        double[,] xn = {
            {-1, -1, 1, 1,-1,-1,1,1},
            {-1, 1, -1, 1,-1,1,-1,1},
            {-1, 1, 1, -1,1,-1,-1,1},
            {1,-1,-1,1,1,-1,-1,1},
            {1,1,-1,-1,-1,-1,1,1},
            {1,-1,1,-1,-1,1,-1,1},
            {-1,-1,-1,-1,1,1,1,1}
    };
        double[] my;
        double[] a;
        double[] b;
        double[] dispersion;
        double dispSum;
        int f1;
        int f2;
        int f3;
        int f4;
        int n;
        List<double[]> y;
        bool[] studentTrue;
        bool adecvat = false;
        int studQuality;
        double Fp;
        public Model()
        {
            n = 4;
            y = new List<double[]>();
            for (int i = 0; i <= 3; i++)
            {
                double[] temp = this.genArray();
                y.Add(temp);
            }
            kohren();
            regression();
            student();
            fisher();
            if (adecvat == false)
            {
                n = 8;
                y = new List<double[]>();
                for (int i = 0; i <= 3; i++)
                {
                    double[] temp = this.genArray();
                    y.Add(temp);
                }
                kohren();
                regression();
                student();
                fisher();
            }
        }
        void kohren()
        {
            int[] kohrenF1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 16, 36, 144, 9999 };
            double[] kohren4 = {
                9.065, 7.679,6.841, 6.287, 5.892,
                5.598, 5.365, 5.175, 5.017, 4.884,
                4.366, 3.720, 3.093, 2.500};
            double[] kohren8 = {
                6.798, 5.157, 4.377, 3.910, 3.595,
                3.362, 3.185, 3.043, 2.926, 2.829,
                2.462, 2.022, 1.616, 1.250};
            double[] kohren = new double[14];
            if (n == 4)
            {
                for (int i = 0; i < 14; i++)
                    kohren[i] = kohren4[i];
                f2 = 4;
            }
            else
            {
                for (int i = 0; i < 14; i++)
                    kohren[i] = kohren8[i];
                f2 = 8;
            }
            my = new double[n];
            a = new double[n];
            b = new double[n];
            studentTrue = new bool[n];
            dispersion = new double[n];
            bool kohrenFlag = false;
            int ii = y.Count;
            for (int i = ii; (i <= 100) && (!kohrenFlag); i++)
            {
                for (int k = 0; k < n; k++)
                    my[k] = 0;
                if (i != ii)
                {
                    double[] temp = this.genArray();
                    y.Add(temp);
                }
                for (int j = 0; j < y.Count; j++)
                {
                    double[] buf = y[j];
                    for (int k = 0; k < n; k++)
                        my[k] += buf[k];
                }
                for (int k = 0; k < n; k++)
                    my[k] /= y.Count;
                dispersion = new double[n];
                for (int j = 0; j < y.Count; j++)
                {
                    double[] buf = y[j];
                    for (int k = 0; k < n; k++)
                        dispersion[k] += (buf[k] - my[k]) * (buf[k] - my[k]);
                }
                for (int k = 0; k < n; k++)
                    dispersion[k] /= y.Count;
                double dispMax = dispersion[0];
                dispSum = dispersion[0];
                for (int j = 1; j < n; j++)
                {
                    dispSum += dispersion[j];
                    if (dispersion[j] > dispMax)
                        dispMax = dispersion[j];
                }
                double Gp = dispMax / dispSum;
                f1 = y.Count - 1;
                int num;
                for (num = 0; (i > kohrenF1[num]); num++) ;
                if (kohren[num] > Gp)
                    kohrenFlag = true;
            }
        }
        void student()
        {
            double[] student = {
                12.71, 4.303, 3.182, 2.776, 2.571, 2.447, 2.365, 2.306, 2.262, 2.228,
                2.201, 2.179, 2.160, 2.145, 2.131, 2.120, 2.110, 2.101, 2.093, 2.086,
                2.080, 2.074, 2.069, 2.064, 2.060, 2.056, 2.052, 2.048, 2.045, 2.042, 1.960};
            double dispStat = Math.Sqrt(dispSum / (y.Count * n * n));
            double[] beta = new double[n];
            for (int i = 0; i < n; i++)
            {
                beta[0] += my[i];
                for (int k = 0; k < (n - 1); k++)
                    beta[k + 1] += (my[i] * xn[k,i]);
            }
            for (int i = 0; i < n; i++)
                beta[i] = beta[i] / n;
            double[] t = new double[n];
            f3 = f1 * f2;
            double t0;
            if (f3 <= 30)
            {
                t0 = student[f3 - 1];
            }
            else
            {
                t0 = student[30];
            }
            studQuality = n;
            for (int i = 0; i < n; i++)
            {
                t[i] = Math.Abs(beta[i]) / dispStat;
                studentTrue[i] = true;
                if (t0 > t[i])
                {
                    studentTrue[i] = false;
                    studQuality--;
                    a[i] = 0;
                    b[i] = 0;
                }
                if (studentTrue[i] == false)
                {
                    a[i] = 0;
                    b[i] = 0;
                }
            }
        }
        void fisher()
        {
            int[] fisherF4 = { 1, 2, 3, 4, 5, 6, 12, 24 };
            int[] fisherF3 = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                22, 24, 26, 28, 30, 40, 60, 120};
            double[,] fisher = {
                {164.4, 199.5, 215.7, 224.6, 230.2, 234.0, 244.9, 249.0, 254.3},
                {18.5, 19.2, 19.2, 19.3, 19.3, 19.3, 19.4, 19.4, 19.5},
                {10.1, 9.6, 9.3, 9.1, 9.0, 8.9, 8.7, 8.6, 8.5},
                {7.7, 6.9, 6.6, 6.4, 6.3, 6.2, 5.9, 5.8, 5.6},
                {6.6, 5.8, 5.4, 5.2, 5.1, 5.0, 4.7, 4.5, 4.4},
                {6.0, 5.8, 5.4, 5.2, 5.1, 5.0, 4.7, 4.5, 4.4},
                {5.5, 4.7, 4.4, 4.1, 4.0, 3.9, 3.6, 3.4, 3.2},
                {5.3, 4.5, 4.1, 3.8, 3.7, 3.6, 3.3, 3.1, 2.9},
                {5.1, 4.3, 3.9, 3.6, 3.5, 3.4, 3.1, 2.9, 2.7},
                {5.0, 4.1, 3.7, 3.5, 3.3, 3.2, 2.9, 2.7, 2.5},
                {4.8, 4.0, 3.6, 3.4, 3.2, 3.1, 2.8, 2.6, 2.4},
                {4.8, 3.9, 3.5, 3.3, 3.1, 3.0, 2.7, 2.5, 2.3},
                {4.7, 3.8, 3.4, 3.2, 3.0, 2.9, 2.6, 2.4, 2.2},
                {4.6, 3.7, 3.3, 3.1, 3.0, 2.9, 2.5, 2.3, 2.1},
                {4.5, 3.7, 3.3, 3.1, 2.9, 2.8, 2.5, 2.3, 2.1},
                {4.5, 3.6, 3.2, 3.0, 2.9, 2.7, 2.4, 2.2, 2.0},
                {4.5, 3.6, 3.2, 3.0, 2.8, 2.7, 2.4, 2.2, 2.0},
                {4.4, 3.6, 3.2, 2.9, 2.8, 2.7, 2.3, 2.1, 1.9},
                {4.4, 3.5, 3.1, 2.9, 2.7, 2.6, 2.3, 2.1, 1.9},
                {4.4, 3.5, 3.1, 2.9, 2.7, 2.6, 2.3, 2.1, 1.9},
                {4.3, 3.4, 3.1, 2.8, 2.7, 2.6, 2.2, 2.0, 1.8},
                {4.3, 3.4, 3.0, 2.8, 2.6, 2.5, 2.2, 2.0, 1.7},
                {4.2, 3.4, 3.0, 2.7, 2.6, 2.5, 2.2, 2.0, 1.7},
                {4.2, 3.3, 3.0, 2.7, 2.6, 2.4, 2.1, 1.9, 1.7},
                {4.2, 3.3, 2.9, 2.7, 2.5, 2.4, 2.1, 1.9, 1.6},
                {4.1, 3.2, 2.9, 2.6, 2.5, 2.3, 2.0, 1.8, 1.5},
                {4.0, 3.2, 2.8, 2.5, 2.4, 2.3, 1.9, 1.7, 1.4},
                {3.9, 3.1, 2.7, 2.5, 2.3, 2.2, 1.8, 1.6, 1.3},
                {3.8, 3.0, 2.6, 2.4, 2.2, 2.1, 1.8, 1.5, 1.0},
                {0,0,0,0,0,0,0,0,0 }
        };
            Fp = 0;
            for (int i = 0; i < n; i++)
            {
                double buf = b[0];
                for (int k = 0; k < (n - 1); k++)
                    buf += (b[k + 1] * xn[k,i]);
                buf -= my[i];
                Fp += Math.Pow(buf, 2);
            }
            f4 = n - studQuality;
            double z;
            if (n == 4)
            {
                z = 16;
            }
            else
            {
                z = 7.25;
            }
            Fp = Fp * y.Count / f4 / dispSum * z * y.Count;
            int numF3;
            for (numF3 = 0; (f3 > fisherF3[numF3]); numF3++) ;
            int numF4;
            for (numF4 = 0; (f4 > fisherF4[numF4]); numF4++) ;
            if (Fp < fisher[numF3,numF4])
            {
                adecvat = true;
            }
        }
        public int getN()
        {
            return n;
        }
        void regression()
        {
            b = new double[n];
            for (int i = 0; i < n; i++)
            {
                b[0] += my[i];
                for (int k = 0; k < (n - 1); k++)
                {
                    b[k + 1] += (xn[k,i] * my[i]);
                }
            }
            for (int i = 0; i < n; i++)
                b[i] /= n;
            double[] dx = { 10, 37.5, 5, 937.5, 225, 125, 5625 };
            double[] x0 = { -10, 7.5, 0, -187.5, 0, 0, 0 }; // x1, x2, x3, x12, x23, x13, x123
            a = new double[n];
            a[0] = b[0];
            for (int k = 0; k < (n - 1); k++)
            {
                a[0] -= (b[k + 1] * x0[k] / dx[k]);
                a[k + 1] = b[k + 1] / dx[k];
            }
        }
        double[] genArray()
        {
            double[] result = new double[n];
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                result[i] = 180 + rand.NextDouble() * 35;
            }
            return result;
        }
        String getF()
        {
            String f = "f1=" + f1 + "  f2=" + f2 + "  f3=" + f3 + "  f4=" + f4 + "  Fp=" + Fp;
            return f;
        }
        String getNormalFactor()
        {
            String result = "";
            if (n == 8)
            {
                if (studentTrue[7] == true)
                {
                    String s = "";
                    if (result.Equals(""))
                    {
                        s = String.Format(" %.5f*x1x2x3", b[7]);
                    }
                    else
                    {
                        s = String.Format(" %.5f*x1x2x3 + ", b[7]);
                    }
                    result = s + result;
                }
                if (studentTrue[6] == true)
                {
                    String s = "";
                    if (result.Equals(""))
                    {
                        s = String.Format(" %.5f*x1x3", b[6]);
                    }
                    else
                    {
                        s = String.Format(" %.5f*x1x3 + ", b[6]);
                    }
                    result = s + result;
                }
                if (studentTrue[5] == true)
                {
                    String s = "";
                    if (result.Equals(""))
                    {
                        s = String.Format(" %.5f*x2x3", b[5]);
                    }
                    else
                    {
                        s = String.Format(" %.5f*x2x3 + ", b[5]);
                    }
                    result = s + result;
                }
                if (studentTrue[4] == true)
                {
                    String s = "";
                    if (result.Equals(""))
                    {
                        s = String.Format(" %.5f*x1x2", b[4]);
                    }
                    else
                    {
                        s = String.Format(" %.5f*x1x2 ", b[4]);
                    }
                    result = s + result;
                }
            }
            if (studentTrue[3] == true)
            {
                String s = "";
                if (result.Equals(""))
                {
                    s = String.Format(" %.3f*x3", b[3]);
                }
                else
                {
                    s = String.Format(" %.3f*x3 + ", b[3]);
                }
                result = s + result;
            }
            if (studentTrue[2] == true)
            {
                String s = "";
                if (result.Equals(""))
                {
                    s = String.Format(" %.3f*x2", b[2]);
                }
                else
                {
                    s = String.Format(" %.3f*x2 + ", b[2]);
                }
                result = s + result;
            }
            if (studentTrue[1] == true)
            {
                String s = "";
                if (result.Equals(""))
                {
                    s = String.Format(" %.3f*x1", b[1]);
                }
                else
                {
                    s = String.Format(" %.3f*x1 + ", b[1]);
                }
                result = s + result;
            }
            if (true)
            {
                String s = "";
                if (result.Equals(""))
                {
                    s = String.Format(" %.3f", b[0]);
                }
                else
                {
                    s = String.Format(" %.3f + ", b[0]);
                }
                result = s + result;
            }
            String s1 = "Normal equation of regression: y = ";
            result = s1 + result;
            return result;
        }
        public double[] getA()
        {
            return a;
        }
        public double[] getB()
        {
            return b;
        }
        String getNaturalFactor()
        {
            String result = "";
            if (n == 8)
            {
                if (studentTrue[7] == true)
                {
                    String s = "";
                    if (result.Equals(""))
                    {
                        s = String.Format(" %.5f*x1x2x3", a[7]);
                    }
                    else
                    {
                        s = String.Format(" %.5f*x1x2x3 + ", a[7]);
                    }
                    result = s + result;
                }
                if (studentTrue[6] == true)
                {
                    String s = "";
                    if (result.Equals(""))
                    {
                        s = String.Format(" %.5f*x1x3", a[6]);
                    }
                    else
                    {
                        s = String.Format(" %.5f*x1x3 + ", a[6]);
                    }
                    result = s + result;
                }
                if (studentTrue[5] == true)
                {
                    String s = "";
                    if (result.Equals(""))
                    {
                        s = String.Format(" %.5f*x2x3", a[5]);
                    }
                    else
                    {
                        s = String.Format(" %.5f*x2x3 + ", a[5]);
                    }
                    result = s + result;
                }
                if (studentTrue[4] == true)
                {
                    String s = "";
                    if (result.Equals(""))
                    {
                        s = String.Format(" %.5f*x1x2", a[4]);
                    }
                    else
                    {
                        s = String.Format(" %.5f*x1x2 ", a[4]);
                    }
                    result = s + result;
                }
            }
            if (studentTrue[3] == true)
            {
                String s = "";
                if (result.Equals(""))
                {
                    s = String.Format(" %.3f*x3", a[3]);
                }
                else
                {
                    s = String.Format(" %.3f*x3 + ", a[3]);
                }
                result = s + result;
            }
            if (studentTrue[2] == true)
            {
                String s = "";
                if (result.Equals(""))
                {
                    s = String.Format(" %.3f*x2", a[2]);
                }
                else
                {
                    s = String.Format(" %.3f*x2 + ", a[2]);
                }
                result = s + result;
            }
            if (studentTrue[1] == true)
            {
                String s = "";
                if (result.Equals(""))
                {
                    s = String.Format(" %.3f*x1", a[1]);
                }
                else
                {
                    s = String.Format(" %.3f*x1 + ", a[1]);
                }
                result = s + result;
            }
            if (true)
            {
                String s = "";
                if (result.Equals(""))
                {
                    s = String.Format(" %.3f", a[0]);
                }
                else
                {
                    s = String.Format(" %.3f + ", a[0]);
                }
                result = s + result;
            }
            String s1 = "Natural equation of regression: y = ";
            result = s1 + result;
            return result;
        }
        public String getAdequate()
        {
            String result = "";
            if (adecvat)
            {
                result += "Equation of regression is adequate";
            }
            else
            {
                result += "Equation of regression is not adequate";
            }
            return result;
        }
        public double[] getDispersion()
        {
            return dispersion;
        }
        public List<double[]> getY()
        {
            return y;
        }
        public double[,] getXn()
        {
            return xn;
        }
        public double[,] getX()
        {
            return x;
        }
        public double[] getMy()
        {
            return my;
        }

    }
}