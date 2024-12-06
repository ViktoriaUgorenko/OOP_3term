using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace laba11
{
    public static class Reflector
    {
        public static string GetAssemblyName(string className)
        {
            var type = Type.GetType(className);
            if (type == null) throw new ArgumentException($"Класс {className} не найден.");
            return type.Assembly.FullName;
        }

        public static bool HasPublicConstructors(string className)
        {
            var type = Type.GetType(className);
            if (type == null) throw new ArgumentException($"Класс {className} не найден.");
            return type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Any();
        }

        public static IEnumerable<string> GetPublicMethods(string className)
        {
            var type = Type.GetType(className);
            if (type == null) throw new ArgumentException($"Класс {className} не найден.");
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                       .Select(method => method.Name);
        }

        public static IEnumerable<string> GetFieldsAndProperties(string className)
        {
            var type = Type.GetType(className);
            if (type == null) throw new ArgumentException($"Класс {className} не найден.");
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                             .Select(field => $"Field: {field.Name}");
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                                 .Select(property => $"Property: {property.Name}");
            return fields.Concat(properties);
        }

        public static IEnumerable<string> GetImplementedInterfaces(string className)
        {
            var type = Type.GetType(className);
            if (type == null) throw new ArgumentException($"Класс {className} не найден.");
            return type.GetInterfaces().Select(i => i.Name);
        }

        public static IEnumerable<string> GetMethodsByParameterType(string className, Type parameterType)
        {
            var type = Type.GetType(className);
            if (type == null) throw new ArgumentException($"Класс {className} не найден.");
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                       .Where(method => method.GetParameters().Any(param => param.ParameterType == parameterType))
                       .Select(method => method.Name);
        }

        public static void Invoke(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл {filePath} не найден.");

            var lines = File.ReadAllLines(filePath);
            if (lines.Length < 2)
                throw new ArgumentException("Файл должен содержать минимум 2 строки: имя класса и имя метода.");

            string className = lines[0]; 
            string methodName = lines[1];

            var type = Type.GetType(className);
            if (type == null)
                throw new ArgumentException($"Класс {className} не найден.");

            object instance;
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
                throw new InvalidOperationException($"У класса {className} нет публичных конструкторов.");

            var constructor = constructors.First();
            var constructorParams = constructor.GetParameters()
                                               .Select(p => GenerateValueForType(p.ParameterType))
                                               .ToArray();
            instance = constructor.Invoke(constructorParams);

            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            if (method == null)
                throw new ArgumentException($"Метод {methodName} не найден в классе {className}.");

            var parameters = method.GetParameters();
            object[] parameterValues = parameters.Select(p => GenerateValueForType(p.ParameterType)).ToArray();

            var result = method.Invoke(instance, parameterValues);

            if (result == null)
            {
                Console.WriteLine($"Метод {methodName} выполнен успешно.");
                if (type.GetMethod("ToString") != null)
                {
                    Console.WriteLine($"Состояние объекта: {instance}");
                }
            }
            else
            {
                Console.WriteLine($"Результат выполнения метода {methodName}: {result}");
            }
        }

        public static T Create<T>(params object[] args)
        {
            Type type = typeof(T);

            var constructor = type.GetConstructors()
                                  .FirstOrDefault(c => c.GetParameters().Length == args.Length);

            if (constructor == null)
            {
                throw new InvalidOperationException($"Для типа {type.Name} не найден подходящий публичный конструктор с {args.Length} параметрами.");
            }

            try
            {
                return (T)constructor.Invoke(args);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Не удалось создать объект типа {type.Name}: {ex.Message}", ex);
            }
        }

        private static object GenerateValueForType(Type type)
        {
            if (type == typeof(int)) return new Random().Next(1, 100); 
            if (type == typeof(double)) return new Random().NextDouble() * 100;
            if (type == typeof(string)) return "GeneratedString";
            if (type == typeof(bool)) return true; 
            if (type.IsEnum) return Enum.GetValues(type).GetValue(0); 
            if (type.IsValueType) return Activator.CreateInstance(type);
            return null;
        }
    }

    public class Programmer
    {
        public string Name { get; set; }

        public Programmer(string name) => Name = name;

        public void Rename(string newName) => Name = newName;

        public override string ToString() => $"Programmer: {Name}";
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var programmer = Reflector.Create<Programmer>("Vika");
                Console.WriteLine(programmer); 

                programmer.Rename("Viktoria");
                Console.WriteLine(programmer); 

                Reflector.Invoke("a.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
