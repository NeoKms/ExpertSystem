//Кластеризация 1
using System;
namespace Clasterisacia
{
    class Program
    {
        public static int N = 0;
        public static int M = 0;
        class Matrix
        {
            
            public int L;
            public int[,] source = new int[N, M];
            public int[,] comp = new int[N, N];
            static int[] res = new int[N];
            static int[] weight = new int[N-1];

            public int[,] Compare(int[,] C, int[,] S)
            {
                
                for (int k = 0; k < N; k++)
                    for (int i = 0; i < N; i++)
                    {
                        int sch = 0;         
                        for (int j = 0; j < M; j++)
                        {
                            if (k == i) continue;
                            if (S[k, j] != S[i, j]) sch++;
                        }
                        C[k, i] = sch;
                        C[i, k] = sch;
                    }   
                return C;
            } 

            public void Clasteric(int[,] C, int L)
            {
                int min = 999;
                int min_i = 0, min_j = 0;

                /*Search Minimum Matrix*/
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                        if ((C[i, j] < min) && (i != j))
                        {
                            min_i = i;
                            min_j = j;
                            min = C[i, j];
                        }
                }
     
                res[0] = min_i + 1;
                res[1] = min_j + 1;
                weight[0] = min;
                C = CNULL(C, min_i);
               
                /*Searh Minimum Str*/
                for (int p = 2; p < N; p++)
                {         
                    int tmp = min_j;
                    min_j = Search(C, min_j, p);
                    C = CNULL(C, tmp);
                }

                Console.Write("Граф : ");
                PrintRes(res);
                Console.Write("Вес  :   ");
                PrintRes(weight);
                Console.WriteLine("\nРезультат выполнения программы :  ");
                Result(res, weight, L);
            }

            static int Search(int[,] C, int i, int p)
            {
                int min = 999;
                int min_j = 0;

                for (int j = 0; j < N; j++)
                    if ((C[i, j] < min) && (C[i, j] != -1) && (j != i))
                    {
                        min = C[i, j];
                        min_j = j;
                    }
                
                res[p] = min_j + 1;
                weight[p - 1] = min;
                return min_j;
            }

            static int[,] CNULL(int[,] C, int k)
            {
                for (int j = 0; j < N; j++)
                {
                    C[k, j] = -1;
                    C[j, k] = -1;
                }
                return C;
            }

            static void Result(int[] R, int[] W, int L)
            {      
                int[] index = new int[L];
                int ind_i = 0;

                for (int k = L-2; k >= 0; k--)
                {
                    int max = -1;
                    for (int i = 0; i < N-1; i++)
                    {
                        if ((W[i] > max)&&(W[i] != -1))
                        { 
                            max = W[i];                     
                            ind_i = i;
                        }
                    }
                    index[k] = ind_i;
                    W[ind_i] = -1;
                }
                int cl = 1;
                Console.Write($"Кластер {cl} : ");
                for (int i = 0; i < N; i++)
                {
                    Console.Write(R[i] + " ");
                    for (int k = 0; k < L - 1; k++)
                        if (index[k] == i)
                        {
                            cl++;
                            Console.Write($"\nКластер {cl} : ");                      
                        }
                }              
            }

            static void PrintRes(int[] RW)
            {
                for (int i = 0; i < RW.GetLength(0); i++)
                {
                    Console.Write(RW[i]);
                    if (i + 1 != RW.GetLength(0)) Console.Write(" - ");
                }
                Console.WriteLine();
            }     
        }
        static public void Print(int[,] R)
            {
                for (int i = 0; i < R.GetLength(0); i++)
                {
                    for (int j = 0; j < R.GetLength(1); j++)                 
                        Console.Write(R[i,j] + " ");                 
                    Console.WriteLine();
                }
            }
        static void Main(string[] args)
        {
                      
            /*enter data*/
            Console.Write("Количество строк ( > 1) : ");
            N = Convert.ToInt32(Console.ReadLine());
            Console.Write("Количество столбцов : ");
            M = Convert.ToInt32(Console.ReadLine());
            Matrix matrix = new Matrix();

            Console.Write($"Введите значения исходной матрицы ({N}x{M}) : \n");
            for (int i = 0; i < N; i++)
            {
                string[] str = Console.ReadLine().Split(' ');
                for (int j = 0; j < M; j++)
                    matrix.source[i, j] = int.Parse(str[j]);
            }
            Console.Write("Введите параметр L : ");
            matrix.L = int.Parse(Console.ReadLine());

            /*algoritm*/        
            matrix.Compare(matrix.comp, matrix.source);
            Console.WriteLine($"\nМатрица, полученная при сравнении строк исходной : ");
            Print(matrix.comp);
            Console.WriteLine();
            matrix.Clasteric(matrix.comp, matrix.L);
            Console.ReadLine();
        }
    }
}
