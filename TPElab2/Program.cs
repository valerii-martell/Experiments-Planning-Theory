using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPElab2
{
    /*
    class Program
    {
        static void Main(string[] args)
        {
            Model model = new Model(10, 60, -70,-10, 90,190);
            model.DoExperiment();
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

            Console.ReadKey();
        }
    }*/
}