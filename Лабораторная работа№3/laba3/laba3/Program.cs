using System;
using System.Text.RegularExpressions;

namespace laba3
{
    public class Stroka
    {
        public string Value { get; private set; }

        public Stroka(string value)
        {
            Value = value;
        }

        public class Production
        {
            public int Id { get; private set; }
            public string nameOfOrg { get; private set; }

            public Production(int id, string organizationName)
            {
                Id = id;
                nameOfOrg = organizationName;
            }

            public override string ToString()
            {
                return $"Production ID: {Id}, Organization: {nameOfOrg}";
            }
        }

        public class Developer
        {
            public string FullName { get; private set; }
            public int Id { get; private set; }
            public string Department { get; private set; }

            public Developer(string fullName, int id, string department)
            {
                FullName = fullName;
                Id = id;
                Department = department;
            }

            public override string ToString()
            {
                return $"Developer: {FullName}, ID: {Id}, Department: {Department}";
            }
        }

        public Production ProductionInfo { get; private set; }
        public Developer DeveloperInfo { get; private set; }

        public Stroka(string value, int productionId, string organizationName, string developerName, int developerId, string department)
        {
            Value = value;
            ProductionInfo = new Production(productionId, organizationName);
            DeveloperInfo = new Developer(developerName, developerId, department);
        }

        public static bool operator <(Stroka str1, Stroka str2)
        {
            return str1.Value.Length < str2.Value.Length;
        }

        public static bool operator >(Stroka str1, Stroka str2)
        {
            return str1.Value.Length > str2.Value.Length;
        }

        public static Stroka operator +(Stroka str, int number)
        {
            return new Stroka(str.Value + number.ToString());
        }

        public static Stroka operator -(Stroka str)
        {
            if (str.Value.Length > 0)
                return new Stroka(str.Value.Substring(0, str.Value.Length - 1));
            else
                return str;
        }

        public static Stroka operator *(Stroka str, char replaceChar)
        {
            return new Stroka(new string(replaceChar, str.Value.Length));
        }

        public bool specialSymbols()
        {
            return Regex.IsMatch(Value, @"[\W_]+");
        }

        public Stroka removePunct()
        {
            return new Stroka(Regex.Replace(Value, @"[^\w\s]", ""));
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public static class StatisticOperation
    {
        public static int sumOfLength(Stroka str1, Stroka str2)
        {
            return str1.Value.Length + str2.Value.Length;
        }

        public static int difference(Stroka str1, Stroka str2)
        {
            return Math.Abs(str1.Value.Length - str2.Value.Length);
        }

        public static int countOfChar(Stroka str)
        {
            return str.Value.Length;
        }

        public static int WordCount(this string str)
        {
            return str.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static int countOfDig(this Stroka str)
        {
            return Regex.Matches(str.Value, @"\d").Count;
        }

        public static Stroka replaceSymbols(this Stroka str)
        {
            return new Stroka(str.Value.Replace(' ', '_'));
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Stroka str1 = new Stroka("hello99, my name 1s V1ka!", 5, "BSTU", "Vika Ugorenko", 222, "IT");
            Stroka str2 = new Stroka("Nice to meet you!", 889, "PSU", "Anton Ugorenko", 465, "NFT");

            int sumOfLength = StatisticOperation.sumOfLength(str1, str2);
            Console.WriteLine($"сумма длин строк: {sumOfLength}"); 

            int lengthDifference = StatisticOperation.difference(str1, str2);
            Console.WriteLine($"разница между длинами строк: {lengthDifference}"); 

            int countChar = StatisticOperation.countOfChar(str1);
            Console.WriteLine($"кол-во символов в строке str1: {countChar}"); 

            string testString = "I like oop";
            Console.WriteLine($"кол-во слов: {testString.WordCount()}"); 

            Console.WriteLine($"кол0во цифр в строке str1: {str1.countOfDig()}"); 
            Stroka replacedStr = str1.replaceSymbols();
            Console.WriteLine($"строка после замены пробелов: {replacedStr}"); 
        }
    }
}
