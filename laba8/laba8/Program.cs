using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace laba8
{
    public static class ProcessOfStr
    {
        public static string RemovePunc(string input)
        {
            return Regex.Replace(input, @"[^\w\s]", "");
        }

        public static string AddAsterisks(string input)
        {
            return $"*{input}*";
        }

        public static string ToUpperCase(string input)
        {
            return input.ToUpper();
        }

        public static string RemoveExtraSpaces(string input)
        {
            return Regex.Replace(input, @"\s+", " ").Trim();
        }

        public static string AddPrefix(string input)
        {
            return $"[Prefix] {input}";
        }
    }

    public class ProgrammerEventArgs : EventArgs
    {
        public string Info { get; set; }
        public ProgrammerEventArgs(string info) => Info = info;
    }

    public class Programmer
    {
        public string Name { get; set; }

        public event EventHandler<ProgrammerEventArgs> Rename;
        public event EventHandler<ProgrammerEventArgs> NewProperty;

        public Programmer(string name) => Name = name;

        public void OnRename(string newName)
        {
            Name = newName;
            Rename?.Invoke(this, new ProgrammerEventArgs($"программист переименован в {newName}"));
        }

        public void OnNewProperty(string property)
        {
            NewProperty?.Invoke(this, new ProgrammerEventArgs($"добавлено новое свойство: {property}"));
        }
    }

    public class LangOfProg
    {
        public string Name { get; private set; }
        public string Version { get; private set; }
        public List<string> Features { get; private set; }

        public LangOfProg(string name, string version)
        {
            Name = name;
            Version = version;
            Features = new List<string>();
        }

        public void UpdateName(object sender, ProgrammerEventArgs e)
        {
            Console.WriteLine($"язык {Name} реагирует на событие: {e.Info}");
            Name = $"Updated_{Name}";
        }

        public void UpdateVersion(object sender, ProgrammerEventArgs e)
        {
            Console.WriteLine($"язык {Name} реагирует на событие: {e.Info}");

            string versionNumber = Version.StartsWith("v") ? Version.Substring(1) : Version;

            var versionParts = versionNumber.Split('.');
            if (int.TryParse(versionParts[0], out int majorVersion))
            {
                majorVersion++;

                Version = versionParts.Length > 1
                    ? $"v{majorVersion}.{versionParts[1]}"
                    : $"v{majorVersion}";
            }
            else
            {
                Console.WriteLine("ошибка: невозможно обновить версию из-за неверного формата.");
            }
        }


        public void AddFeature(object sender, ProgrammerEventArgs e)
        {
            Console.WriteLine($"Язык {Name} реагирует на событие: {e.Info}");
            Features.Add($"Функция_{Features.Count + 1}");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"язык: {Name}, версия: {Version}");
            Console.WriteLine("функции:");
            if (Features.Count == 0)
            {
                Console.WriteLine(" - нет добавленных функций");
            }
            else
            {
                foreach (var feature in Features)
                    Console.WriteLine($" - {feature}");
            }
            Console.WriteLine();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string text = "           пример обработки    ... строки!!!";

            List<Func<string, string>> processors = new List<Func<string, string>>()
            {
                input => Regex.Replace(input, @"[^\w\s]", ""),     
                input => Regex.Replace(input, @"\s+", " ").Trim(), 
                input => input.ToUpper(),                        
                input => $"*{input}*",                          
                input => $"[Prefix] {input}"            
            };


            foreach (var processor in processors)
            {
                text = processor(text);
            }

            Console.WriteLine("результат обработки строки:");
            Console.WriteLine(text);

            var programmer = new Programmer("Artem");

            var language1 = new LangOfProg("C#", "v1");
            var language2 = new LangOfProg("Ruby", "v2");
            var language3 = new LangOfProg("Swift", "v3");

            programmer.Rename += language1.UpdateName;
            programmer.NewProperty += language1.AddFeature;

            programmer.Rename += language2.UpdateName;
            programmer.NewProperty += language2.UpdateVersion;

            programmer.Rename += language3.UpdateVersion;
            programmer.NewProperty += language3.AddFeature;

            Console.WriteLine("\nинициализация языков программирования:");
            language1.ShowInfo();
            language2.ShowInfo();
            language3.ShowInfo();

            Console.WriteLine("событие: Переименование программиста");
            programmer.OnRename("Vika");

            Console.WriteLine("\nсобытие: Новое свойство программиста");
            programmer.OnNewProperty("allala");

            Console.WriteLine("\nрезультаты после событий:");
            language1.ShowInfo();
            language2.ShowInfo();
            language3.ShowInfo();
        }
    }

}
