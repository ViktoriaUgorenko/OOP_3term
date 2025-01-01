using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace laba14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("выберите задачу (1-5):");
            Console.WriteLine("1. вывести список процессов и записать их в файл");
            Console.WriteLine("2. исследовать домен приложения");
            Console.WriteLine("3. вычислить простые числа до заданного числа и записать их в файл");
            Console.WriteLine("4. одновременный вывод чётных и нечётных чисел потоками");
            Console.WriteLine("5. выполнение повторяющейся задачи с таймером");

            int taskNumber;
            if (!int.TryParse(Console.ReadLine(), out taskNumber))
            {
                Console.WriteLine("введите корректный номер задачи");
                return;
            }

            switch (taskNumber)
            {
                case 1:
                    ListProcesses();
                    break;
                case 2:
                    ExploreAppDomain();
                    break;
                case 3:
                    CalculatePrimes();
                    break;
                case 4:
                    EvenOddThreads();
                    break;
                case 5:
                    TimerTask();
                    break;
                default:
                    Console.WriteLine("неверный номер задачи:(");
                    break;
            }
        }

        static void ListProcesses()
        {
            StreamWriter writer = new StreamWriter("Processes.txt");
            try
            {
                foreach (var process in Process.GetProcesses())
                {
                    try
                    {
                        writer.WriteLine($"ID: {process.Id}, имя: {process.ProcessName}, приоритет: {process.BasePriority}, состояние: {process.Responding}, запуск: {process.StartTime}, время CPU: {process.TotalProcessorTime}");
                    }
                    catch (Exception ex)
                    {
                        writer.WriteLine($"ошибка получения данных о процессе {process.ProcessName}: {ex.Message}");
                    }
                }

                Console.WriteLine("список процессов записан в файл Processes.txt");
            }
            finally
            {
                writer.Close();
            }
        }

        static void ExploreAppDomain()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Console.WriteLine($"имя домена: {currentDomain.FriendlyName}");

            foreach (Assembly assembly in currentDomain.GetAssemblies())
            {
                Console.WriteLine($"загруженная сборка: {assembly.FullName}");
            }

            AppDomain newDomain = AppDomain.CreateDomain("NewDomain");
            try
            {
                string assemblyPath = Assembly.GetExecutingAssembly().Location;
                newDomain.Load(AssemblyName.GetAssemblyName(assemblyPath));
                Console.WriteLine($"сборка {Path.GetFileName(assemblyPath)} загружена в новый домен");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ошибка: {ex.Message}");
            }
            finally
            {
                AppDomain.Unload(newDomain);
                Console.WriteLine("новый домен выгружен");
            }
        }

        static void CalculatePrimes()
        {
            Console.Write("введите n для вычисления простых чисел: ");
            int n = int.Parse(Console.ReadLine());

            Thread primeThread = new Thread(() => {
                StreamWriter writer = new StreamWriter("Primes.txt");
                try
                {
                    for (int i = 2; i <= n; i++)
                    {
                        if (IsPrime(i))
                        {
                            Console.WriteLine(i);
                            writer.WriteLine(i);
                            Thread.Sleep(50);
                        }
                    }
                }
                finally
                {
                    writer.Close();
                }
            });

            primeThread.Start();
            while (primeThread.IsAlive)
            {
                Console.WriteLine($"статус потока: {primeThread.ThreadState}");
                Thread.Sleep(100);
            }

            Console.WriteLine("простые числа записаны в файл Primes.txt");
        }

        static bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        static void EvenOddThreads()
        {
            Console.Write("введите n для вывода четных и нечетных чисел: ");
            int n = int.Parse(Console.ReadLine());
            StreamWriter writer = new StreamWriter("EvenOdd.txt");

            object locker = new object();
            bool evenTurn = true;

            Thread evenThread = new Thread(() =>
            {
                for (int i = 2; i <= n; i += 2)
                {
                    lock (locker)
                    {
                        while (!evenTurn) Monitor.Wait(locker);
                        Console.WriteLine($"четное: {i}");
                        writer.WriteLine($"четное: {i}");
                        evenTurn = false;
                        Monitor.Pulse(locker);
                    }
                }
            });

            Thread oddThread = new Thread(() =>
            {
                for (int i = 1; i <= n; i += 2)
                {
                    lock (locker)
                    {
                        while (evenTurn) Monitor.Wait(locker);
                        Console.WriteLine($"нечетное: {i}");
                        writer.WriteLine($"нечетное: {i}");
                        evenTurn = true;
                        Monitor.Pulse(locker);
                    }
                }
            });

            evenThread.Start();
            oddThread.Start();
            evenThread.Join();
            oddThread.Join();

            writer.Close();
            Console.WriteLine("четные и нечетные числа записаны в файл EvenOdd.txt");
        }

        static void TimerTask()
        {
            Timer timer = new Timer(_ =>
            {
                Console.WriteLine($"повторяющаяся задача выполнена в {DateTime.Now}");
            }, null, 0, 2000);

            Console.WriteLine("пока");
            Console.ReadKey();
            timer.Dispose();
        }
    }
}
