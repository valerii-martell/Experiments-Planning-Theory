using System;
using System.Linq;

namespace EPTlab2
{
    class Model
    {

        private double x1Min;
        private double x1Max;
        private double x2Min;
        private double x2Max;
        private double yMin;
        private double yMax;

        private double[,] ymatr = new double[3,5];
        private double[,] xmatr = new double[3,2]{
                                   { -1.0, -1.0 },
                                   { 1.0, -1.0 },
                                   {-1.0, 1.0 }};
        private double[] alpha = new double[3];
        private double[] b = new double[3];
        private double[] ym = new double[3];
        private bool dispersion;

        public Model(double x1Min, double x1Max, double x2Min, double x2Max, double yMin, double yMax)
        {
            this.x1Min = x1Min;
            this.x1Max = x1Max;
            this.x2Min = x2Min;
            this.x2Max = x2Max;
            this.yMin = yMin;
            this.yMax = yMax;
        }
        public void DoExperiment()
        {
            for (int i = 0; i < ymatr.GetLength(0); i++)
            {
                for (int j = 0; j < ymatr.GetLength(1); j++)
                {
                    Random rnd = new Random();
                    ymatr[i,j] = rnd.NextDouble() * (yMax - yMin) - yMin;
                }
            }

            ym[0] = 0;
            ym[1] = 0;
            ym[2] = 0;

            for (int i = 0; i < 5; i++)
            {
                ym[0] = ym[0] + ymatr[0,i];
                ym[1] = ym[1] + ymatr[1,i];
                ym[2] = ym[2] + ymatr[2,i];
            }

            ym[0] = ym[0] / 5;
            ym[1] = ym[1] / 5;
            ym[2] = ym[2] / 5;

            double disp1 = 0, disp2 = 0, disp3 = 0;

            for (int i = 0; i < 5; i++)
            {
                disp1 = disp1 + (ymatr[0,i] - ym[0]) * (ymatr[0,i] - ym[0]);
                disp2 = disp2 + (ymatr[1,i] - ym[1]) * (ymatr[1,i] - ym[1]);
                disp3 = disp3 + (ymatr[2,i] - ym[2]) * (ymatr[2,i] - ym[2]);
            }
            disp1 = disp1 / 5; disp2 = disp2 / 5; disp3 = disp3 / 5;

            double sigma = 1.79;
            double fuv1 = disp1 / disp2;
            double fuv2 = disp3 / disp1;
            double fuv3 = disp3 / disp2;

            double tuv1 = fuv1 * (3 / 4);
            double tuv2 = fuv2 * (3 / 4);
            double tuv3 = fuv3 * (3 / 4);

            double ruv1 = Math.Abs(tuv1 - 1) / sigma;
            double ruv2 = Math.Abs(tuv2 - 1) / sigma;
            double ruv3 = Math.Abs(tuv3 - 1) / sigma;

            if (ruv1 < 2 && ruv2 < 2 && ruv3 < 2)
                dispersion = true;
            else
                dispersion = false;

            // ruv < 2

            double mx1 = (xmatr[0,0] + xmatr[1,0] + xmatr[2,0]) / 3;
            double mx2 = (xmatr[0,1] + xmatr[1,1] + xmatr[2,1]) / 3;

            double my = (ym.Sum()) / 3;

            double a1 = (xmatr[0,0] * xmatr[0,0] + xmatr[1,0] * xmatr[1,0] + xmatr[2,0] * xmatr[2,0]) / 3;
            double a2 = (xmatr[0,0] * xmatr[0,1] + xmatr[1,0] * xmatr[1,1] + xmatr[2,0] * xmatr[2,1]) / 3;
            double a3 = (xmatr[0,1] * xmatr[0,1] + xmatr[1,1] * xmatr[1,1] + xmatr[2,1] * xmatr[2,1]) / 3;

            double a11 = (xmatr[0,0] * ym[0] + xmatr[1,0] * ym[1] + xmatr[2,0] * ym[2]) / 3;
            double a22 = (xmatr[0,1] * ym[0] + xmatr[1,1] * ym[1] + xmatr[2,1] * ym[2]) / 3;

            b[0] = (my * a1 * a3 + mx1 * a2 * a22 + mx2 * a11 * a2 - mx2 * a1 * a22 - mx1 * a11 * a3 - my * a2 * a2) /
                    (1 * a1 * a3 + mx1 * a2 * mx2 + mx2 * mx1 * a2 - mx2 * a1 * mx2 - mx1 * mx1 * a3 - 1 * a2 * a2);
            b[1] = (1 * a11 * a3 + my * a2 * mx2 + mx2 * mx1 * a22 - mx2 * a11 * mx2 - my * mx1 * a3 - 1 * a2 * a22) /
                    (1 * a1 * a3 + mx1 * a2 * mx2 + mx2 * mx1 * a2 - mx2 * a1 * mx2 - mx1 * mx1 * a3 - 1 * a2 * a2);
            b[2] = (1 * a1 * a22 + mx1 * a11 * mx2 + my * mx1 * a2 - my * a1 * mx2 - mx1 * mx1 * a22 - 1 * a11 * a2) /
                    (1 * a1 * a3 + mx1 * a2 * mx2 + mx2 * mx1 * a2 - mx2 * a1 * mx2 - mx1 * mx1 * a3 - 1 * a2 * a2);

            double dx1 = Math.Abs(x1Max - x1Min) / 2;
            double dx2 = Math.Abs(x2Max - x2Min) / 2;
            double x10 = (x1Max + x1Min) / 2;
            double x20 = (x2Max + x2Min) / 2;

            alpha[0] = b[0] - b[1] * (x10 / dx1) - b[2] * (x20 / dx2);
            alpha[1] = b[1] / dx1;
            alpha[2] = b[2] / dx2;
        }

        public double[,] Xmatr { get { return xmatr; } }

        public double[,] Ymatr { get { return ymatr; } }

        public double[] Alpha { get { return alpha; } }

        public double[] B { get { return b; } }

        public double[] Ym { get { return ym; } }

        public string Dispersion
        {
            get { if (dispersion == false)
                    return "Not uniform dispersion";
                  else
                    return "Uniform dispersion";
            }
        }
        }
    }