using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class Program
    {
        //Последовательное умножение
        static int[,] Multiplication1(int[,] a, int[,] b)
        {
            DateTime start = DateTime.Now;
            int[,] r = new int[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            Console.WriteLine("\nПоследовательное умножение " + (DateTime.Now - start).TotalSeconds.ToString() + "c\n");
            return r;
        }

        //Параллельное умножение
        static int[,] Multiplication2(int[,] a, int[,] b)
        {
            DateTime start = DateTime.Now;
            Task[] tasks = new Task[a.GetLength(0)];
            int[,] r = new int[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                int index = i;
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < b.GetLength(1); j++)
                    {
                        for (int k = 0; k < b.GetLength(0); k++)
                        {
                            r[index, j] += a[index, k] * b[k, j];

                        }
                    }
                }
                );
            }
            Task.WaitAll(tasks);
            Console.WriteLine("\nПараллельное умножение " + (DateTime.Now - start).TotalSeconds.ToString() + "c\n");
            return r;
        }

        static void Main(string[] args)
        {
            int size = 0;
            Console.WriteLine("Введите N");
            size = Convert.ToInt32(Console.ReadLine());
            int[,] mas1 = new int[size, size];
            Random rnd = new Random();

            Console.WriteLine("Матрица А");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    mas1[i, j] = rnd.Next(0, 100);
                    if (size < 11) Console.Write(mas1[i, j] + "\t");
                }
                if (size < 11) Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("Матрица В");
            int[,] mas2 = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    mas2[i, j] = rnd.Next(0, 100);
                    if (size < 11) Console.Write(mas2[i, j] + "\t");
                }
                if (size < 11) Console.WriteLine();
            }

            int[,] res1 = Multiplication1(mas1, mas2);
            if (size < 11)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Console.Write(res1[i, j] + "\t");
                    }
                    if (size < 11) Console.WriteLine();
                }
            }
            int[,] res2 = Multiplication2(mas1, mas2);
            if (size < 11)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Console.Write(res2[i, j] + "\t");
                    }
                    if (size < 11) Console.WriteLine();
                }
            }
            Console.ReadKey();
        }
    }
}
