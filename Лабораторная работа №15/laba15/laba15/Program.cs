using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 345; 
            var matrixA = GenerateMatrix(size, size);
            var matrixB = GenerateMatrix(size, size);

            Stopwatch stopwatch = new Stopwatch();

            for (int run = 1; run <= 3; run++) 
            {
                Console.WriteLine($"\nзапуск {run}:");
                stopwatch.Restart();

                Task<double[,]> multiplicationTask = Task.Run(() => MultiplyMatrices(matrixA, matrixB));

                Console.WriteLine($"ID задачи: {multiplicationTask.Id}");

                while (!multiplicationTask.IsCompleted)
                {
                    Console.WriteLine("задача ещё не завершена...");
                    Task.Delay(500).Wait(); 
                }

                var resultMatrix = multiplicationTask.Result; 
                stopwatch.Stop();

                Console.WriteLine($"задача завершена: {multiplicationTask.IsCompleted}");
                Console.WriteLine($"затраченное время: {stopwatch.ElapsedMilliseconds} мс");
            }
        }

        static double[,] GenerateMatrix(int rows, int cols)
        {
            var random = new Random();
            var matrix = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = random.NextDouble() * 100;
            return matrix;
        }

        static double[,] MultiplyMatrices(double[,] matrixA, double[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int colsB = matrixB.GetLength(1);

            var result = new double[rowsA, colsB];

            Parallel.For(0, rowsA, i =>
            {
                for (int j = 0; j < colsB; j++)
                {
                    for (int k = 0; k < colsA; k++)
                    {
                        result[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            });

            return result;
        }
    }
}
