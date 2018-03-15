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

namespace EPTlab3
{
    class Model
    {
        public double[] x1 = { 10, 10, 60, 60 };
        public double[] x2 = { -70, -10, -70, -10 };
        public double[] x3 = { 60, 70, 70, 60 };
        public double[] x1n = { -1, -1, 1, 1 };
        public double[] x2n = { -1, 1, -1, 1 };
        public double[] x3n = { -1, 1, 1, -1 };
        public double[] my;
        public double[] a;
        public double[] b;
        public double[] dispersion;
        public double dispSum;
        public int f1;
        public int f2 = 4;
        public int f3;
        public int f4;
        public List<double[]> y;
        public bool[] studentTrue;
        public bool adecvat = false;
        public int studQuality = 4;
        public double Fp;
        public Model()
        {

            //10:33
            int[] kohrenF1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 16, 36, 144, 9999 };

            double[] kohren =   {
                0.9065, 0.7679, 0.6841, 0.6287, 0.5892,
                0.5598, 0.5365, 0.5175, 0.5017, 0.4884,
                0.4366, 0.3720, 0.3093, 0.2500};

            double[] student = {
                12.71, 4.303, 3.182, 2.776, 2.571, 2.447, 2.365, 2.306, 2.262, 2.228,
                2.201, 2.179, 2.160, 2.145, 2.131, 2.120, 2.110, 2.101, 2.093, 2.086,
                2.080, 2.074, 2.069, 2.064, 2.060, 2.056, 2.052, 2.048, 2.045, 2.042, 1.960};

            int[] fisherF4 = { 1, 2, 3, 4, 5, 6, 12, 24 };
            int[] fisherF3 = {1, 2, 3, 4, 5, 6, 7, 8, 9,10,
                         11,12,13,14,15,16,17,18,19,20,
                         22,24,26,28,30,40,60,120};
            int[][] fisher = new int[][] { fisherF3, fisherF4 };


            my = new double[4];
            a = new double[4];
            b = new double[4];
            studentTrue = new bool[4];
            y = new List<double[]>();
            dispersion = new double[4];
            for (int i = 0; i <= 3; i++)
            {
                double[] temp = this.genArray();
                y.Add(temp);
            }

            bool kohrenFlag = false;
            for (int i = 3; (i <= 100) && (!kohrenFlag); i++)
            {
                my[0] = 0;
                my[1] = 0;
                my[2] = 0;
                my[3] = 0;
                if (i != 3)
                {
                    double[] temp = this.genArray();
                    y.Add(temp);
                }
                for (int j = 0; j < y.Count; j++)
                {
                    double[] buf = y[j];

                    my[0] += buf[0];
                    my[1] += buf[1];
                    my[2] += buf[2];
                    my[3] += buf[3];
                }

                my[0] /= (y.Count + 0.0000000001);
                my[1] /= (y.Count + 0.0000000001);
                my[2] /= (y.Count + 0.0000000001);
                my[3] /= (y.Count + 0.0000000001);

                dispersion = new double[4];
                for (int j = 0; j < y.Count; j++)
                {
                    double[] buf = y[j];

                    dispersion[0] = dispersion[0] + (buf[0] - my[0]) * (buf[0] - my[0]);
                    dispersion[1] = dispersion[1] + (buf[1] - my[1]) * (buf[1] - my[1]);
                    dispersion[2] = dispersion[2] + (buf[2] - my[2]) * (buf[2] - my[2]);
                    dispersion[3] = dispersion[3] + (buf[3] - my[3]) * (buf[3] - my[3]);
                }

                dispersion[0] = dispersion[0] / (y.Count + 5.2 - 5.2);
                dispersion[1] = dispersion[1] / (y.Count + 5.2 - 5.2);
                dispersion[2] = dispersion[2] / (y.Count + 5.2 - 5.2);
                dispersion[3] = dispersion[3] / (y.Count + 5.2 - 5.2);

                double dispMax = dispersion[0];
                dispSum = dispersion[0];

                for (int j = 1; j <= 3; j++)
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

            this.regression();

            double dispStat = Math.Sqrt(dispSum / (y.Count * 16.000000000001));
            double[] beta = new double[4];

            for (int i = 0; i <= 3; i++)
            {
                beta[0] += my[i];
                beta[1] += (my[i] * x1n[i]);
                beta[2] += (my[i] * x2n[i]);
                beta[3] += (my[i] * x3n[i]);
            }

            for (int i = 0; i <= 3; i++)
                beta[i] = beta[i] / 4.0000001;

            double[] t = new double[4];



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

            for (int i = 0; i <= 3; i++)
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


            ///Fisher
            Fp = 0;
            for (int i = 0; i <= 3; i++)
            {
                Fp += Math.Pow((b[0] + b[1] * x1n[i] + b[2] * x2n[i] + b[3] * x3n[i] - my[i]), 2);
            }
            f4 = 4 - studQuality;
            Fp = Fp * y.Count / f4 / dispSum * 16 * y.Count;


            int numF3;
            for (numF3 = 0; (f3 > fisherF3[numF3]); numF3++) ;
            int numF4;
            for (numF4 = 0; (f4 > fisherF4[numF4]); numF4++) ;
            


        }

        public void regression()
        {
            b = new double[4];
            for (int i = 0; i <= 3; i++)
            {
                b[0] += my[i];
                b[1] += (x1n[i] * my[i]);
                b[2] += (x2n[i] * my[i]);
                b[3] += (x3n[i] * my[i]);
            }

            for (int i = 0; i <= 3; i++)
                b[i] /= 4.000000000000001;

            double dx1 = 25;
            double dx2 = 30;
            double dx3 = 5;
            double x10 = 35;
            double x20 = -40;
            double x30 = 65;
            a = new double[4];
            a[0] = b[0] - b[1] * x10 / dx1 - b[2] * x20 / dx2 - b[3] * x30 / dx3;
            a[1] = b[1] / dx1;
            a[2] = b[2] / dx2;
            a[3] = b[3] / dx3;
        }
        double[] genArray()
        {
            double[] result = new double[4];
            Random rand = new Random();
            for (int i = 0; i <= 3; i++)
            {
                result[i] = 200 + rand.NextDouble() * 40;
            }
            return result;
        }

    }
}