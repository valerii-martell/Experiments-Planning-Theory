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

namespace EPTlab6
{
    class Model
    {/*
        double[][] x;
        double[][] xn = {
            {-1, -1, -1, -1, 1, 1, 1, 1, -1.73, 1.73, 0, 0, 0, 0},
            {-1, -1, 1, 1, -1, -1, 1, 1, 0, 0, -1.73, 1.73, 0, 0},
            {-1, 1, -1, 1, -1, 1, -1, 1, 0, 0, 0, 0, -1.73, 1.73}

    };


        double[][] xn1;

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
        LinkedList<double[]> y;
        bool[] studentTrue;
        bool adecvat = false;
        int studQuality;
        double Fp;
        double Gp;
        double Gt;
        double Tt;
        double Ft;
        String equBefore;
        String equAfter;
        double[][] matrix;
        double[] vector;
        double[] resultBef;
        double[] resultAft;


        Model()
        {
            double[] xmin = { 10, -70, 60 };
            double[] xmax = { 60, -10, 70 };


            double[] dx = new double[3];
            double[] x0 = new double[3];
            for (int i = 0; i < 3; i++)
            {
                dx[i] = (xmax[i] - xmin[i]) / 2;
                x0[i] = dx[i] + xmin[i];
            }

            n = 4;

            x = new double[3, 4];
            xn1 = new double[3, 4];

            for (int i = 0; i < 3; i++)
            {
                double[] temp = new double[x[0].length];
                for (int j = 0; j < x[0].length; j++)
                    temp[j] = genNumber(x[0][j], x[1][j], x[2][j]);

                y.add(temp);
            }
            kohren();
            regression();
            equBefore = this.getNaturalFactor();

            studentTrue = new boolean[n];
            for (int i = 0; i < n; i++)
            {
                studentTrue[i] = true;
            }
            resultBef = new double[x[0].length];
            for (int i = 0; i < x[0].length; i++)
            {
                resultBef[i] = b[0];
                for (int j = 0; j < (n - 1); j++)
                {
                    resultBef[i] += (b[j + 1] * x[j][i]);
                }

            }
            student();
            resultAft = new double[x[0].length];
            for (int i = 0; i < x[0].length; i++)
            {
                resultAft[i] = b[0];
                for (int j = 0; j < (n - 1); j++)
                {
                    resultAft[i] += (b[j + 1] * x[j][i]);
                }

            }
            equAfter = this.getNaturalFactor();
            fisher();


            if (adecvat == false)
            {
                n = 8;


                double[][] xn = {
                    {-1, -1, -1, -1, 1, 1, 1, 1, -1.73, 1.73, 0, 0, 0, 0},
                    {-1, -1, 1, 1, -1, -1, 1, 1, 0, 0, -1.73, 1.73, 0, 0},
                    {-1, 1, -1, 1, -1, 1, -1, 1, 0, 0, 0, 0, -1.73, 1.73}};


                x = new double[7, 8];
                xn1 = new double[7, 8];

                LinkedList y1 = new LinkedList();


                for (int i = 0; i < y.size(); i++)
                {
                    double[] temp = new double[x[0].length];
                    for (int j = 0; j < x[0].length; j++)
                        temp[j] = genNumber(x[0][j], x[1][j], x[2][j]);
                    for (int j = 0; j < y.get(i).length; j++)
                    {
                        temp[j] = y.get(i)[j];
                    }
                    y1.add(i, temp);
                }
                y = new LinkedList();
                y.addAll(y1);
                kohren();
                regression();
                studentTrue = new boolean[n];
                for (int i = 0; i < n; i++)
                {
                    studentTrue[i] = true;
                }
                ;
                equBefore = this.getNaturalFactor();

                resultBef = new double[x[0].length];
                for (int i = 0; i < x[0].length; i++)
                {
                    resultBef[i] = b[0];
                    for (int j = 0; j < (n - 1); j++)
                    {
                        resultBef[i] += (b[j + 1] * x[j][i]);
                    }

                }
                student();
                resultAft = new double[x[0].length];
                for (int i = 0; i < x[0].length; i++)
                {
                    resultAft[i] = b[0];
                    for (int j = 0; j < (n - 1); j++)
                    {
                        resultAft[i] += (b[j + 1] * x[j][i]);
                    }

                }
                fisher();
                equAfter = this.getNaturalFactor();
                if (adecvat == false)
                {
                    n = 11;



                    x = new double[10, 14];
                    xn1 = new double[10, 14];
                    for (int i = 0; i < x[0].length; i++)
                    {



                    }
                    LinkedList y2 = new LinkedList();
                    for (int i = 0; i < y.size(); i++)
                    {

                        double[] temp = new double[x[0].length];
                        for (int j = 0; j < x[0].length; j++)
                            temp[j] = genNumber(x[0][j], x[1][j], x[2][j]);


                        for (int j = 0; j < y.get(i).length; j++)
                        {
                            temp[j] = y.get(i)[j];
                        }
                        y2.add(i, temp);
                    }
                    y = new LinkedList();
                    y.addAll(y2);
                    kohren();
                    regression();

                    studentTrue = new boolean[n];
                    for (int i = 0; i < n; i++)
                    {
                        studentTrue[i] = true;
                    }
                    equBefore = this.getNaturalFactor();

                    student();
                    equAfter = this.getNaturalFactor();

                    fisher();

                }
            }

        }


        void regression()
        {
            b = new double[n];


            double[] mx = new double[n - 1];
            double[] a0 = new double[n - 1];
            double[][] aa = new double[n - 1, n - 1];
            double my0 = 0;
            for (int i = 0; i < x[0].length; i++)
            {
                my0 += my[i];

            }
            my0 /= (double)x[0].length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < x[0].length; j++)
                {
                    mx[i] += x[i][j];
                    a0[i] += (x[i][j] * my[j]);

                }
                mx[i] /= (double)x[0].length;
                a0[i] /= (double)x[0].length;
            }
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        aa[i][j] += (x[i][k] * x[j][k]);
                    }
                    aa[i][j] /= (double)x[0].length;
                }
            }


            matrix = new double[n, n];
            matrix[0][0] = 1;
            vector = new double[n];
            vector[0] = my0;
            for (int i = 1; i < n; i++)
            {
                matrix[i][0] = mx[i - 1];
                matrix[0][i] = mx[i - 1];
                vector[i] = a0[i - 1];
                for (int j = 1; j < n; j++)
                {
                    matrix[i][j] = aa[i - 1][j - 1];
                }

            }

            Matrix matrixTemp0 = new Matrix(matrix);
            double det = matrixTemp0.det();

            for (int i = 0; i < n; i++)
            {
                double[][] temp = new double[n, n];

                for (int k = 0; k < n; k++)
                {
                    for (int l = 0; l < n; l++)
                    {
                        temp[k][l] = matrix[k][l];
                    }
                    temp[k][i] = vector[k];
                }


                Matrix matrixTemp = new Matrix(temp);
                b[i] = matrixTemp.det() / det;
            }

        }

        void kohren()
        {
            int[] kohrenF1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 16, 36, 144, 9999 };

            double[] kohren4 = {
                9.065, 7.679, 6.841, 6.287, 5.892,
                5.598, 5.365, 5.175, 5.017, 4.884,
                4.366, 3.720, 3.093, 2.500};
            double[] kohren8 = {
                6.798, 5.157, 4.377, 3.910, 3.595,
                3.362, 3.185, 3.043, 2.926, 2.829,
                2.462, 2.022, 1.616, 1.250};

            double[] kohren11 = {
                5.410, 3.924, 3.264, 2.880, 2.624,
                2.439, 2.299, 2.187, 2.098, 2.020,
                1.737, 1.403, 1.000, 0.833};


            double[] kohren = new double[14];

            if (n == 4)
            {
                for (int i = 0; i < 14; i++)
                    kohren[i] = kohren4[i];
                f2 = 4;
            }

            if (n == 8)
            {
                for (int i = 0; i < 14; i++)
                    kohren[i] = kohren8[i];
                f2 = 8;
            }

            if (n == 11)
            {
                for (int i = 0; i < 14; i++)
                    kohren[i] = kohren11[i];
                f2 = 11;
            }

            my = new double[x[0].length];

            studentTrue = new boolean[n];


            boolean kohrenFlag = false;


            int ii = y.size();

            for (int i = ii; (i <= 100) && (!kohrenFlag); i++)
            {
                for (int k = 0; k < x[0].length; k++)
                    my[k] = 0;


                for (int j = 0; j < y.size(); j++)
                {
                    double[] buf = y.get(j);
                    for (int k = 0; k < x[0].length; k++)
                        my[k] += buf[k];
                }

                for (int k = 0; k < x[0].length; k++)
                    my[k] /= y.size();


                dispersion = new double[x[0].length];
                for (int j = 0; j < y.size(); j++)
                {
                    double[] buf = y.get(j);
                    for (int k = 0; k < x[0].length; k++)
                        dispersion[k] += (buf[k] - my[k]) * (buf[k] - my[k]);
                }
                for (int k = 0; k < x[0].length; k++)
                    dispersion[k] /= y.size();
                double dispMax = dispersion[0];
                dispSum = dispersion[0];

                for (int j = 1; j < x[0].length; j++)
                {
                    dispSum += dispersion[j];
                    if (dispersion[j] > dispMax)
                        dispMax = dispersion[j];
                }
                Gp = dispMax / dispSum;
                f1 = y.size() - 1;

                int num;
                for (num = 0; (y.size() > kohrenF1[num]); num++) ;
                if (num > 0)
                    num--;
                Gt = kohren[num] / 10;
                if (Gt > Gp)
                    kohrenFlag = true;
                else if (i != ii)
                {

                    double[] temp = new double[x[0].length];
                    for (int j = 0; j < x[0].length; j++)
                        temp[j] = genNumber(x[0][j], x[1][j], x[2][j]);

                    y.add(temp);
                }


            }
        }

        void student()
        {
            dispersion = new double[x[0].length];
            for (int j = 0; j < y.size(); j++)
            {
                double[] buf = y.get(j);
                for (int k = 0; k < x[0].length; k++)
                    dispersion[k] += (buf[k] - my[k]) * (buf[k] - my[k]);
            }
            for (int k = 0; k < x[0].length; k++)
                dispersion[k] /= y.size();

            dispSum = dispersion[0];

            for (int j = 1; j < x[0].length; j++)
            {
                dispSum += dispersion[j];

            }

            double[] student = {
                12.71, 4.303, 3.182, 2.776, 2.571, 2.447, 2.365, 2.306, 2.262, 2.228,
                2.201, 2.179, 2.160, 2.145, 2.131, 2.120, 2.110, 2.101, 2.093, 2.086,
                2.080, 2.074, 2.069, 2.064, 2.060, 2.056, 2.052, 2.048, 2.045, 2.042, 1.960};
            double dispStat = Math.sqrt(dispSum / (y.size() * n * n));
            double[] beta = new double[n];
            studentTrue = new boolean[n];
            for (int i = 0; i < n; i++)
            {
                studentTrue[i] = true;
            }

            for (int i = 0; i < x[0].length; i++)
            {
                beta[0] += my[i];
                for (int k = 1; k < n; k++)
                    beta[k] += (my[i] * xn1[k - 1][i]);
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
            Tt = t0;
            studQuality = n;
            for (int i = 0; i < n; i++)
            {

                t[i] = Math.abs(beta[i]) / dispStat;
                studentTrue[i] = true;
                if (t0 > t[i])
                {
                    studentTrue[i] = false;
                    studQuality = studQuality - 1;
                    //a[i] = 0;
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

            double[][] fisher = {
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
                {}
        };
            Fp = 0;
            for (int i = 0; i < x[0].length; i++)
            {
                double buf = b[0];
                for (int k = 0; k < (n - 1); k++)
                    buf += (b[k + 1] * x[k][i]);
                buf -= my[i];
                Fp += Math.pow(buf, 2);
            }

            f4 = n - studQuality;

            Fp = Fp * y.size() / f4 / dispSum * n * n * y.size();


            int numF3;
            for (numF3 = 0; (f3 > fisherF3[numF3]) && (f3 < fisherF3.length - 1); numF3++) ;

            int numF4;
            for (numF4 = 0; (f4 > fisherF4[numF4]) && (f4 < fisherF4.length - 1); numF4++) ;
            if (Fp < fisher[numF3][numF4])
            {
                adecvat = true;
            }
            Ft = fisher[numF3][numF4];
        }


        double[] genArray()
        {
            double[] result = new double[x[0].length];
            Random rand = new Random();
            for (int i = 0; i < x[0].length; i++)
            {
                result[i] = 195 + 2.0 / 3.0 + rand.nextDouble() * 7;

            }
            return result;
        }

        double genNumber(double x1, double x2, double x3)
        {
            Random rand = new Random();
            double result = 6.7 + 7.1 * x1 + 7.8 * x2 + 7.4 * x3 +
                            0.1 * x1 * x1 + 0.7 * x2 * x2 + 7.1 * x3 * x3 +
                            8.2 * x1 * x2 + 0.2 * x1 * x3 + 9.5 * x2 * x3 +
                            6.6 * x1 * x2 * x3;

            result = result - 5 + rand.nextDouble() * 10;

            return result;
        }

        String getF()
        {
            String f = "f1=" + f1 + "  f2=" + f2 + "  f3=" + f3 + "  f4=" + f4
                    + "  Gt=" + String.format("%.4f", Gt) + "  Gp=" + String.format("%.4f", Gp)
                    + "  Fp=" + String.format("%.4f", Fp) + "  Ft=" + String.format("%.4f", Ft) + "  Tt=" + String.format("%.4f", Tt);
            return f;
        }

        String getResult()
        {
            String f = "";
            for (int i = 0; i < x[0].length; i++)
            {
                double temp = b[0];
                for (int j = 0; j < (n - 1); j++)
                {
                    temp += (b[j + 1] * x[j][i]);
                }
                f = f + " " + String.format("%.3f", temp);
            }
            return f;
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

        }*/
    }
}