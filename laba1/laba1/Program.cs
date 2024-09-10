using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace laba1
{
    class Program
    {
        static void Main(string[] args)
        {

            //ТИПЫ
            /*a. Определите переменные всех возможных примитивных типов С# и проинициализируйте их.
             * Осуществите ввод и вывод их значений используя консоль. 
             */
            bool var1 = false;
            byte var2 = 65;
            sbyte var3 = 4;
            char var4 = 'k';
            decimal var5 = 1189M;
            double var6 = 34.56D;
            float var7 = 1.65f;
            int var8 = -5678;
            uint var9 = 6233939;
            long var10 = -13984;
            ulong var11 = 2292982;
            short var12 = -4344;
            ushort var13 = 1221;
            string var14 = "HI";

            Console.WriteLine("вывод значений переменных:");
            Console.WriteLine($"bool    - {var1}\n" +
                              $"byte    - {var2}\n" +
                              $"sbyte   - {var3}\n" +
                              $"char    - {var4}\n" +
                              $"decimal - {var5}\n" +
                              $"double  - {var6}\n" +
                              $"float   - {var7}\n" +
                              $"int     - {var8}\n" +
                              $"uint    - {var9}\n" +
                              $"long    - {var10}\n" +
                              $"ulong   - {var11}\n" +
                              $"short   - {var12}\n" +
                              $"ushort  - {var13}\n" +
                              $"string  - {var14}\n"
                              );

            Console.WriteLine("Ввод значений переменных: ");
            Console.Write("bool: ");
            var1 = bool.Parse(Console.ReadLine());
            Console.Write("byte: ");
            var2 = byte.Parse(Console.ReadLine());
            Console.Write("sbyte: ");
            var3 = sbyte.Parse(Console.ReadLine());
            Console.Write("char: ");
            var4 = char.Parse(Console.ReadLine());
            Console.Write("decimal: ");
            var5 = decimal.Parse(Console.ReadLine());
            Console.Write("double: ");
            var6 = double.Parse(Console.ReadLine());
            Console.Write("float: ");
            var7 = float.Parse(Console.ReadLine());
            Console.Write("int: ");
            var8 = int.Parse(Console.ReadLine());
            Console.Write("uint: ");
            var9 = uint.Parse(Console.ReadLine());
            Console.Write("long: ");
            var10 = long.Parse(Console.ReadLine());
            Console.Write("ulong: ");
            var11 = ulong.Parse(Console.ReadLine());
            Console.Write("short: ");
            var12 = short.Parse(Console.ReadLine());
            Console.Write("ushort: ");
            var13 = ushort.Parse(Console.ReadLine());
            Console.Write("string: ");
            var14 = Console.ReadLine();

            Console.WriteLine($"bool    - {var1}\n" +
                             $"byte    - {var2}\n" +
                             $"sbyte   - {var3}\n" +
                             $"char    - {var4}\n" +
                             $"decimal - {var5}\n" +
                             $"double  - {var6}\n" +
                             $"float   - {var7}\n" +
                             $"int     - {var8}\n" +
                             $"uint    - {var9}\n" +
                             $"long    - {var10}\n" +
                             $"ulong   - {var11}\n" +
                             $"short   - {var12}\n" +
                             $"ushort  - {var13}\n" +
                             $"string  - {var14}\n"
                             );

            /*b. Выполните 5 операций явного и 5 неявного приведения. Изучите возможности класса Convert.*/

            //явное преобразование
            short var20 = 1111;
            int var21 = (int)var20;
            byte var22 = (byte)var21;
            char var23 = (char)var22;
            double var24 = (double)var23;

            Console.WriteLine("явное преобразование: ");
            Console.WriteLine($"short   - {var20}\n" +
                              $"int     - {var21}\n" +
                              $"byte    - {var22}\n" +
                              $"char    - {var23}\n" +
                              $"double  - {var24}\n"
                           );


            //неявное преобразование
            char var25 = 's';
            ushort var26 = var25;
            int var27 = var26;
            float var28 = var27;
            double var29 = var28;

            Console.WriteLine("неявное преобразование: ");
            Console.WriteLine($"char    - {var25}\n" +
                              $"ushort  - {var26}\n" +
                              $"int     - {var27}\n" +
                              $"float   - {var28}\n" +
                              $"double  - {var29}\n"
                           );

            //Класс Convert 
            Console.WriteLine("Convert: ");
            int var30 = 1893;
            double var31 = Convert.ToDouble(var30);
            Console.WriteLine($"int     - {var30}\n" +
                              $"double  - {var31}\n"
                          );

            //c. Выполните упаковку и распаковку значимых типов
            long var32 = 18829440050;//у
            object var33 = var32;
            var32 = (long)var33;//р

            //d. Продемонстрируйте работу с неявно типизированной переменной.
            var var34 = "Maxim";
            Console.WriteLine($"Hello,  {var34}");

            //e. Продемонстрируйте пример работы с Nullable переменной
            int? var35 = null;
            if (var35.HasValue)
                Console.WriteLine(var35.Value);
            else
                Console.WriteLine("var35 равен null");


            //СТРОКИ
            //а. Объявите строковые литералы. Сравните их.
            string str1 = "Hello";
            string str2 = "World";

            if (str1 == str2)
                Console.WriteLine($"Строка {str1} равна {str2}");
            else
                Console.WriteLine($"Строка {str1} не равна {str2}");

            //2b. Создайте 3 строки на осн. String
            String str5 = "я на гитаре";
            String str11 = "играю";
            String str3;
            String str4 = "я пришла и ушла";

            Console.WriteLine(str1 + " " + str2);                         ///сцепление
            str3 = str4;                                            ///копирование
            Console.WriteLine(str3);
            Console.WriteLine(str4.Substring(2, 6));                ///выделение подстроки
            string[] words = str4.Split(new char[] { ' ' });        ///разделение строки на слова
            foreach (string s in words) Console.WriteLine(s);
            str1 = str1.Substring(0, 2) + str2 + str1.Remove(0, 2); ///вставка подстроки в заданную позицию
            Console.WriteLine(str1);
            str4 = str4.Remove(2, 6);                               ///удаление заданной подстроки
            Console.WriteLine(str4 + "\n");


            /*c. Создайте пустую и null строку. Продемонстрируйте использование метода string.IsNullOrEmpty.
             * Продемонстрируйте что еще можно выполнить с такими строками*/
            string str7 = "";
            string str8 = null;
            string str9 = "   \t   ";

            if (String.IsNullOrEmpty(str7))
                Console.WriteLine("Str7 пустая или null-строка");
            else
                Console.WriteLine("Str7 не null-строка или не пустая");


            if (String.IsNullOrEmpty(str8))
                Console.WriteLine("Str8 пустая или null-строка");
            else
                Console.WriteLine("Str8 не null-строка или не пустая");

            if (String.IsNullOrEmpty(str9))
                Console.WriteLine("Str9 пустая или null-строка");
            else
                Console.WriteLine("Str9 не null-строка или не пустая");

            if (String.IsNullOrWhiteSpace(str7))
                Console.WriteLine("Str7 пустая или null-строка или строка из пробелов");
            else
                Console.WriteLine("Str7 не null-строка или не пустая");

            if (String.IsNullOrWhiteSpace(str8))
                Console.WriteLine("Str8 пустая или null-строка или строка из пробелов");
            else
                Console.WriteLine("Str8 не null-строка или не пустая");

            if (String.IsNullOrWhiteSpace(str9))
                Console.WriteLine("Str9 пустая или null-строка или строка из пробелов");
            else
                Console.WriteLine("Str9 не null-строка или не пустая");
            /*d. Создайте строку на основе StringBuilder. Удалите определенные позиции и добавьте 
             * новые символы в начало и конец строки. */

            StringBuilder str10 = new StringBuilder(" an old");
            str10.Remove(2, 5);
            str10.Insert(0, "This is");
            str10.Append(" new string");
            Console.WriteLine(str10);

            //
            //МАССИВЫ
            //a. Создайте целый двумерный массив и выведите его на консоль в отформатированном виде (матрица). 
            int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 10, 11, 12 } };

            Console.WriteLine("\n\nМатрица:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]} \t");
                }
                Console.WriteLine();
            }

            /*b. Создайте одномерный массив строк. Выведите на консоль его содержимое, длину массива. Поменяйте произвольный элемент
            (пользователь определяет позицию и значение).*/

            Console.WriteLine();

            string[] stringArray = { "First", "Second", "Third", "Fourth" };
            foreach (string str in stringArray)
                Console.Write($"{str}\t");
            Console.WriteLine($"\nДлина массива: {stringArray.Length}");

            Console.WriteLine($"Введите позицию замены (от 0 до {stringArray.Length - 1}):");
            int position = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите новую строку:");
            string newString = Console.ReadLine();

            stringArray[position] = newString;

            Console.WriteLine("Новый массив: ");
            foreach (string str in stringArray)
                Console.Write($"{str}\t");

            /*c. Создайте ступечатый (не выровненный) массив вещественных чисел с 3-мя строками, 
             * в каждой из которых 2, 3 и 4 столбцов соответственно. Значения массива введите с консоли.*/

            float[][] array1 = new float[3][];
            array1[0] = new float[2];
            array1[1] = new float[3];
            array1[2] = new float[4];

            Console.WriteLine("\nВведите элементы массива: ");
            for (var i = 0; i < array1.Length; i++)
            {
                for (var j = 0; j < array1[i].Length; j++)
                    array1[i][j] = float.Parse(Console.ReadLine());
                Console.WriteLine();
            }

            Console.WriteLine("Массив: ");
            for (int i = 0; i < array1.Length; i++)
            {
                for (int j = 0; j < array1[i].Length; j++)
                {
                    Console.Write($"{array1[i][j]} \t");
                }
                Console.WriteLine();
            }

            //d. Создайте неявно типизированные переменные для хранения массива и строки.
            var array2 = new[] { 1.5, 2.87, 3.23, 4.1 };
            var string1 = "abcdefgh";

            
            //КОРТЕЖИ
            //a.Задайте кортеж из 5 элементов с типами int, string, char, string, ulong.

            (int, string, char, string, ulong) myTuple = (-8, "Hi", 'a', "dear", 987654321);
            (int, string, char, string, ulong) myTuple1 = (823, "cool", 'j', "work", 1234567);

            //b. Выведите кортеж на консоль целиком и выборочно ( например 1, 3, 4 элементы)
            Console.WriteLine("Кортеж: ");
            Console.WriteLine(myTuple);
            Console.WriteLine("Элементы кортежа (1, 3, 4): ");
            Console.WriteLine(myTuple.Item1);
            Console.WriteLine(myTuple.Item3);
            Console.WriteLine(myTuple.Item4);

            /*c. Выполните распаковку кортежа в переменные. Продемонстрируйте различные способы распаковки кортежа.
            Продемонстрируйте использование переменной(_).*/
            var firstItem = myTuple.Item1;
            var secondItem = myTuple.Item2;
            var thirdItem = myTuple.Item3;
            var fourthItem = myTuple.Item4;
            var fifthItem = myTuple.Item5;

            var (item1, item2, tem3, item4, item5) = myTuple1;

            (int, float) newTuple = (-43, 7.8f);
            (int intVar, float floatVar) = newTuple;

            var (_, string11, _, string12, _) = (-4, "Hello", 'a', "World", 199388384848);

            //d. Сравните два кортежа.
            if (myTuple == myTuple1)
                Console.WriteLine("Кортежи равны");
            else
                Console.WriteLine("Кортежи не равны");

            //ЛОКАЛЬНАЯ ФУНКЦИЯ
 
            (int max, int min, int sum, char firstChar) AnalyzeArray(int[] nomers, string textik)
            {
                // Проверка на наличие элементов в массиве.
                if (nomers.Length == 0)
                    throw new ArgumentException("Массив не может быть пустым");

               
                int max = nomers[0];
                int min = nomers[0];
                int sum = 0;

                foreach (int num in nomers)
                {
                    if (num > max) max = num;
                    if (num < min) min = num;
                    sum += num;
                }

                // Проверка, содержит ли строка хотя бы один символ.
                // Если да, то берется первый символ строки, иначе устанавливается нулевой символ '\0'.
                char firstChar = textik.Length > 0 ? textik[0] : '\0';

                return (max, min, sum, firstChar);
            }
            int[] numbers = { 1, 2, 3, 4, 5 };
            string text = "I love this university:*";

     
            var result = AnalyzeArray(numbers, text);

            Console.WriteLine($"Строка массва: {text}");

            Console.WriteLine($"Максимальный элемент: {result.max}, Минимальный элемент: {result.min}, Сумма: {result.sum}, Первый символ: {result.firstChar}");

            // Работа с checked/unchecked

            void CheckedOperation()
            {
                checked
                {
                    try
                    {
                        
                        int maxValue = int.MaxValue;
                        int result0 = maxValue + 1; 
                    }
                    catch (OverflowException)
                    {
                        // Если переполнение происходит, выбрасывается исключение OverflowException
                        Console.WriteLine("Checked: Переполнение.");
                    }
                }
            }

          
            void UncheckedOperation()
            {
                unchecked
                {
                    int maxValue = int.MaxValue;
     
                    int result0 = maxValue + 1; 
                    Console.WriteLine($"Unchecked: Результат переполнения {result0}");
                }
            }

        
            CheckedOperation();
            UncheckedOperation();
        }
    }
}