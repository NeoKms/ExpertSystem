//Кластеризация 2
using System;
namespace Clasterisacia2
{
    class Program
    {
        public static void ElementsAdd(int[,] S, int N, int M)
        {
            for (int i = 0; i < N; i++)
            {
                Console.Write($" A{i + 1}:  ");
                string[] str = Console.ReadLine().Split(' ');
                for (int j = 0; j < M; j++)
                    S[i, j] = int.Parse(str[j]);
            }
        }
        public static void Print(int[,] S)
        {
            for (int i = 0; i < S.GetLength(0); i++)
            {
                Console.Write($" A{i + 1}:  ");
                for (int j = 0; j < S.GetLength(1); j++)
                    Console.Write(S[i, j] + " ");
                Console.WriteLine();
            }
        }
        public static int DefinL(int M)
        {
            if (M == 1) return 1;
            else
                return Convert.ToInt32(Math.Floor((float)M/2 + (float)M/4));
        }
        /*Алгоритм решения*/     
        class ClasterCompare
        {
            int QM = 1;
            int L = 0;
            int I = 0, J = 0;
            public void Initialization(int Defl, int N, int M)
            {
                L = Defl; // порог различий
                I = N; J = M;
                Console.WriteLine($"\n Порог различимости (схожести) : {L} ");
            }
            /*Инициализация матрицы масок*/
            public int CreateMask(int[,] S, int[,] M, int[] C)
            {
                for (int j = 0; j < J; j++)  //1 маска = 1 строка
                    M[0, j] = S[0, j];

                for (int i = 1; i < I; i++)
                {
                    for (int j = 0; j < J; j++)
                        C[j] = S[i, j];
                    compCreateMask(C, M, i);
                }
                return QM;
            }
            /*Определение и подсчет масок*/
            private void compCreateMask(int[] C, int[,] M, int index)
            {
                int Rz = 0; //различие
                for (int j = 0; j < J; j++)
                    if (C[j] != M[index - 1, j]) Rz++; //кол.во различий

                if (Rz >= L)
                {
                    for (int i = 0; i < index - 1; i++)
                    {
                        Rz = 0;
                        for (int j = 0; j < J; j++)
                            if (C[j] == M[i, j]) Rz++;
                        if (Rz >= L) break;
                    }

                    for (int j = 0; j < J; j++)
                        if (Rz <= L) M[QM, j] = C[j];
                    if (Rz <= L) QM++;
                }             
            }
            /*Инициализация конечной матрицы кодирования масок*/
            public void CreateResult(int[,] S, int[,] M, int[,] R, int[] C)
            {
                for (int i = 0; i < I; i++)
                {
                    for (int j = 0; j < J; j++)
                        C[j] = S[i, j];

                    for (int k = 0; k < QM; k++)
                        compResult(C, M, R, k, i);
                }
            }
            /*Кодирование масок*/
            private void compResult(int[] C, int[,] M, int[,] R, int kndex, int index)
            {
                int Sh = 0; //сходимость
                for (int j = 0; j < J; j++)
                    if (C[j] == M[kndex, j]) Sh++;

                if (Sh >= L)
                    R[index, kndex] = 1;
                else
                    R[index, kndex] = 0;
            }

        }    
        static void Main(string[] args)
        {
            //Входные данные
            Console.Write("I. Укажите параметры задачи \n");
            Console.Write("\n Количество строк ( > 1) : ");
            int N = Convert.ToInt32(Console.ReadLine());
            Console.Write(" Количество столбцов : ");
            int M = Convert.ToInt32(Console.ReadLine());

            int[,] Source = new int[N,M]; // исходная матрица
            int[,] Mask = new int[N, M];  // матрица подсчета масок
            int[] Compare = new int[M];   // матрица сравнения i-строки с маской
            int[] Index = new int[N];     // массив индексов

            Console.Write($"\n Исходная матрицы ({N}x{M}) : \n");
            ElementsAdd(Source, N, M);          
          
            Console.Write("\nII. Результат решения задачи \n");
            ClasterCompare Classter = new ClasterCompare();
            Classter.Initialization(DefinL(M), N, M);

            int QM = Classter.CreateMask(Source, Mask, Compare);  //  количество масок                              
            int[,] Result = new int[N, QM]; // конечная матрица масок кодирования

            Console.WriteLine($" Количество масок : {QM} \n");
            //Console.WriteLine(" Матрица масок : "); Print(Mask);
            Console.WriteLine(" Матрица кодирования строк: ");
            Classter.CreateResult(Source, Mask, Result, Compare);
            Print(Result);

            //Преобразование двумерного массива в одномерный для сортировки
            string[] Sort = new string[N]; 
            for (int i = 0; i < N; i++) 
            {
                Index[i] = i;
                for (int j = 0; j < QM; j++)
                    Sort[i] += Result[i, j].ToString();
            }
            //Сортировка
            for (int j = 0; j < N - 1; j++)
            {
                for (int i = 0; i < N - j - 1; i++)
                    if (Sort[i].CompareTo(Sort[i + 1]) < 0)
                    {
                        string tempchar = Sort[i];
                        Sort[i] = Sort[i + 1];
                        Sort[i + 1] = tempchar;

                        int temp = Index[i];
                        Index[i] = Index[i + 1];
                        Index[i + 1] = temp;
                    }
            }
            
            //Вывод результата
            int QC = 1;       
            Console.WriteLine("\n Кластеры : ");
            for (int i = 0; i < N; i++) 
            {
                Console.Write(" A" + (Index[i] + 1));
                if (i + 1 != N)
                {
                    if (Sort[i] != Sort[i + 1])
                    {
                        Console.Write(" | ");
                        QC++;
                    }
                    else Console.Write(", ");
                }
            }
            Console.WriteLine("\n\n Количество кластеров : " + QC);
            Console.WriteLine("\nДля выхода нажмите клавишу ввода (ENTER)...");
            Console.ReadLine();
        }
    }
}
