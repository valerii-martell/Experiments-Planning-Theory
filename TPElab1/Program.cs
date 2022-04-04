using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPElab1
{
    static class ExtensionsMethods
    {
        public static void Print(this double value)
        {
            if (value > 0)
            {
                string s = string.Format("{0:F2}", value);
                Console.Write(s + "|");
            }
            else if (value < 0)
            {
                string s = string.Format("{0:F1}", value);
                Console.Write(s + "|");
            }
            else
            {
                string s = string.Format("{0:F1}", value);
                Console.Write(s + " |");
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Input left limit:");
                double a = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Input right limit:");
                double b = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Input a0:");
                double a0 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Input a1:");
                double a1 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Input a2:");
                double a2 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Input a3:");
                double a3 = Convert.ToDouble(Console.ReadLine());

                Model model = new Model(a, b, a0, a1, a2, a3);
                double[,] matrix = model.MatrixPlan;

                Console.WriteLine("-----------------Matrix---------------");
                Console.WriteLine("|N| X1 | X2 | X3 | Y  |Xn1 |Xn2 |Xn3 |");
                for (int i = 0; i < 8; i++)
                {
                    Console.Write("|" + i + "|");
                    for (int j = 0; j < 7; j++)
                    {
                        matrix[i, j].Print();
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("-------Standard------");
                Console.WriteLine("|X01 |X02 |X03 | Y0 |");
                Console.Write("|");
                for (int j = 0; j < 4; j++)
                {
                    model.Standard[j].Print();
                }

                Console.WriteLine("----Interval----");
                Console.WriteLine("|dx1 |dx2 |dx3 |");
                Console.Write("|");
                for (int j = 0; j < 3; j++)
                {
                    model.Interval[j].Print();
                }

                Console.WriteLine();
                Console.WriteLine("-------Optimum-------");
                Console.WriteLine("|X01 |X02 |X03 | Y0 |");
                Console.Write("|");
                for (int j = 0; j < 4; j++)
                {
                    matrix[model.Optimum, j].Print();
                }

                Console.ReadKey();
            }

        }
    }
}
