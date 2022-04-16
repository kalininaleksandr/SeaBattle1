using System;
using System.Collections.Generic;

namespace game_2
{
    class Program
    {
        static void SayHello()
        {
            Console.WriteLine("Hello");
        }
        static string[,] init(int m, int n, string sym)
        {
            string[,] tmp = new string[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    tmp[i, j] = sym;
                }
            }
            return tmp;
        }
        static void show(string[,] field, int m, int n)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(field[i, j]);
                }
                Console.WriteLine();
            }
        }
        static bool end_game(string[,] field1, string[,] field2, int m, int n, int k)
        {
            bool endgame = false;
            int cnt_1 = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (field1[i, j] == "1")
                    {
                        cnt_1 += 1;
                    }
                }
            }
            if (cnt_1 == k)
            {
                Console.WriteLine("Игрок first победил");
                endgame = true;
            }


            int cnt_2 = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (field2[i, j] == "1")
                    {
                        cnt_2 += 1;
                    }
                }
            }
            if (cnt_2 == k)
            {
                Console.WriteLine("Игрок second победил");
                endgame = true;
            }
            return endgame;

        }

        static bool checkNear(string[,] field, int m, int n, int orig_m, int orig_n)
        {
            int m1 = m - 1 <= 0 ? 0 : m - 1;
            int n1 = n - 1 <= 0 ? 0 : n - 1;
            if (field[m1, n1] == "#") { return false; }

            int m2 = m;
            int n2 = n - 1 <= 0 ? 0 : n - 1;
            if (field[m2, n2] == "#") { return false; }

            int m3 = m + 1 > orig_m - 1 ? m : m + 1;
            int n3 = n - 1 <= 0 ? 0 : n - 1;
            if (field[m3, n3] == "#") { return false; }

            int m4 = m - 1 <= 0 ? 0 : m - 1;
            int n4 = n;
            if (field[m4, n4] == "#") { return false; }

            int m5 = m;
            int n5 = n;
            if (field[m5, n5] == "#") { return false; }

            int m6 = m + 1 > orig_m - 1 ? m : m + 1;
            int n6 = n;
            if (field[m6, n6] == "#") { return false; }

            int m7 = m - 1 <= 0 ? 0 : m - 1;
            int n7 = n + 1 > orig_n - 1 ? n : n + 1;
            if (field[m7, n7] == "#") { return false; }

            int m8 = m;
            int n8 = n + 1 > orig_n - 1 ? n : n + 1;
            if (field[m8, n8] == "#") { return false; }

            int m9 = m + 1 > orig_m - 1 ? m : m + 1;
            int n9 = n + 1 > orig_n - 1 ? n : n + 1;
            if (field[m9, n9] == "#") { return false; }

            return true;
        }
        static int count_status_ship(string[,] field, int m, int n)
        {
            int cnt_k = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (field[i, j] == "#")
                    {
                        cnt_k += 1;
                    }
                }
            }

            return cnt_k;
        }
        class RndCell
        {
            public RndCell(int _x, int _y)
            {
                x = _x;
                y = _y;
            }
            public int x { get; }
            public int y { get; }
        }
        static void Main(string[] args)
        {
            Console.Write("Enter m:");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter n:");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter k:");
            int k = Convert.ToInt32(Console.ReadLine());

            /*int m = 5;
            int n = 4;
            int k = 3;*/

            string status_miss = "0";
            string status_goal = "1";
            string status_empty = "?";
            string status_ship = "#";

            string[,] field1 = init(m, n, status_empty);
            string[,] field2 = init(m, n, status_empty);

            while (count_status_ship(field1, m, n) < k)
            {
                Random rnd = new Random();
                int m_rnd = rnd.Next(0, m);
                int n_rnd = rnd.Next(0, n);
                if (checkNear(field1, m_rnd, n_rnd, m, n) == true)
                {
                    field1[m_rnd, n_rnd] = status_ship;
                }
            }

            while (count_status_ship(field2, m, n) < k)
            {
                Random rnd = new Random();
                int m_rnd = rnd.Next(0, m);
                int n_rnd = rnd.Next(0, n);
                if (checkNear(field2, m_rnd, n_rnd, m, n) == true)
                {
                    field2[m_rnd, n_rnd] = status_ship;
                }
            }

            string[,] field_1 = init(m, n, status_empty);
            string[,] field_2 = init(m, n, status_empty);

            //show(field_1,m,n);

            while (end_game(field_1, field_2, m, n, k) != true)
            {
                show(field_1, m, n);
                Console.Write("First Enter x:");
                int x = Convert.ToInt32(Console.ReadLine());
                Console.Write("First Enter y:");
                int y = Convert.ToInt32(Console.ReadLine());
                if ((x < 0 || x >= m) || (y < 0 || y >= n))
                {
                    Console.WriteLine("Соблюдайте границы массива!");
                    continue;
                }

                if (field2[x, y] == "#")
                {
                    field_1[x, y] = "1";
                    Console.ForegroundColor = ConsoleColor.Green;
                    show(field_1, m, n);
                    Console.Write("Ты попал!\r\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    field_1[x, y] = "0";
                    show(field_1, m, n);
                    Console.Write("Ты промазал!\r\n");
                    Console.ResetColor();
                }
                //////////////////////////
                show(field_2, m, n);
                Console.Write("Second Enter x:");
                x = Convert.ToInt32(Console.ReadLine());
                Console.Write("Second Enter y:");
                y = Convert.ToInt32(Console.ReadLine());
                if ((x < 0 || x >= m) || (y < 0 || y >= n))
                {
                    Console.WriteLine("Соблюдайте границы массива!");
                    continue;
                }


                if (field1[x, y] == "#")
                {
                    field_2[x, y] = "1";
                    show(field_2, m, n);
                    Console.Write("Ты попал!\r\n");
                }
                else
                {
                    field_2[x, y] = "0";
                    show(field_2, m, n);
                    Console.Write("Ты промазал!\r\n");
                }
            }

        }
    }
}