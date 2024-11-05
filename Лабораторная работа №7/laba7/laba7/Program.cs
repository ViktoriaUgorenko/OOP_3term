using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace laba7
{
    public interface IOformlenie
    {
        string GetDetails();
        void PrintDetails();
    }

    public abstract class Oformlenie : IOformlenie
    {
        public string Color { get; private set; }

        protected Oformlenie(string color)
        {
            Color = color;
        }

        public abstract string GetDetails();
        public abstract void PrintDetails();
    }

  
    public class Window : Oformlenie
    {
        public string NameOfWind { get; private set; }
        public int CountOfWind { get; private set; }

        public Window(string color, string nameOfWind, int countOfWind) : base(color)
        {
            NameOfWind = nameOfWind;
            CountOfWind = countOfWind;
        }

        public override string GetDetails()
        {
            return $"Window {{ Color='{Color}', NameOfWind='{NameOfWind}', CountOfWind={CountOfWind} }}";
        }

        public override void PrintDetails()
        {
            Console.WriteLine("Oformlenie (Window): " + GetDetails());
        }

        public override string ToString()
        {
            return $"Window (Type: {this.GetType().Name}) {{ Color='{Color}', NameOfWind='{NameOfWind}', CountOfWind={CountOfWind} }}";
        }
    }

    public interface ICollectionOperations<T>
    {
        void Add(T item);
        void Remove(T item);
        void View();
        T Find(Predicate<T> predicate);
        void SaveToFile(string filePath);
        void LoadFromFile(string filePath);
    }

    public class CollectionType<T> : ICollectionOperations<T> where T : IOformlenie
    {
        private List<T> _items;

        public CollectionType()
        {
            _items = new List<T>();
        }

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "элемент не может быть null");

            _items.Add(item);
            Console.WriteLine($"элемент добавлен: {item}");
        }

        public void Remove(T item)
        {
            if (_items.Remove(item))
                Console.WriteLine($"элемент удален: {item}");
            else
                Console.WriteLine("элемент не найден");
        }

        public void View()
        {
            if (_items.Count == 0)
            {
                Console.WriteLine("коллекция пуста");
                return;
            }

            Console.WriteLine("элементы коллекции:");
            foreach (var item in _items)
                Console.WriteLine(item);
        }

        public T Find(Predicate<T> predicate)
        {
            var foundItem = _items.Find(predicate);
            if (foundItem != null)
            {
                Console.WriteLine($"найден элемент: {foundItem}");
                return foundItem;
            }
            else
            {
                Console.WriteLine("элемент не найден");
                return default;
            }
        }

        public void SaveToFile(string filePath)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, _items);
                }
                Console.WriteLine("коллекция сохранена в файл");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"произошла ошибка при сохранении коллекции: {ex.Message}");
            }
        }

        public void LoadFromFile(string filePath)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    _items = (List<T>)formatter.Deserialize(stream);
                }
                Console.WriteLine("коллекция загружена из файла");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"произошла ошибка при загрузке коллекции: {ex.Message}");
            }
        }
    }

    public class CollectionTypeWithoutInterface<T> : ICollectionOperations<T>
    {
        private List<T> _items;

        public CollectionTypeWithoutInterface()
        {
            _items = new List<T>();
        }

        public void Add(T item)
        {
            _items.Add(item);
            Console.WriteLine($"элемент добавлен: {item}");
        }

        public void Remove(T item)
        {
            if (_items.Remove(item))
                Console.WriteLine($"элемент удален: {item}");
            else
                Console.WriteLine("элемент не найден");
        }

        public void View()
        {
            if (_items.Count == 0)
            {
                Console.WriteLine("коллекция пуста");
                return;
            }

            Console.WriteLine("элементы коллекции:");
            foreach (var item in _items)
                Console.WriteLine(item);
        }

        public T Find(Predicate<T> predicate)
        {
            var foundItem = _items.Find(predicate);
            if (foundItem != null)
            {
                Console.WriteLine($"найден элемент: {foundItem}");
                return foundItem;
            }
            else
            {
                Console.WriteLine("элемент не найден");
                return default;
            }
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var item in _items)
                    writer.WriteLine(item);
            }
            Console.WriteLine("коллекция сохранена в файл");
        }

        public void LoadFromFile(string filePath)
        {
            _items.Clear();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    _items.Add((T)Convert.ChangeType(line, typeof(T)));
            }
            Console.WriteLine("коллекция загружена из файла");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----- Коллекция Window -----");
            CollectionType<Window> windowCollection = new CollectionType<Window>();
            Window window1 = new Window("Red", "window1", 3);
            Window window2 = new Window("Blue", "window2", 2);

            windowCollection.Add(window1);
            windowCollection.Add(window2);
            windowCollection.View();

            windowCollection.SaveToFile("D:\\БГТУ\\ООП\\Лабораторная работа №7\\laba7\\laba7\\obj\\Debug\\windows.txt");
            windowCollection.LoadFromFile("D:\\БГТУ\\ООП\\Лабораторная работа №7\\laba7\\laba7\\obj\\Debug\\windows.txt");
            windowCollection.View();

            windowCollection.Remove(window1);
            windowCollection.View();

            windowCollection.Find(w => w.NameOfWind == "window2");

            Console.WriteLine("\n----- Коллекция string -----");
            CollectionTypeWithoutInterface<string> stringCollection = new CollectionTypeWithoutInterface<string>();
            stringCollection.Add("Vika Ugorenko");
            stringCollection.Add("Anton Ugorenko");
            stringCollection.View();

            stringCollection.SaveToFile("D:\\БГТУ\\ООП\\Лабораторная работа №7\\laba7\\laba7\\obj\\Debug\\strings.txt");
            stringCollection.LoadFromFile("D:\\БГТУ\\ООП\\Лабораторная работа №7\\laba7\\laba7\\obj\\Debug\\strings.txt");
            stringCollection.View();

            stringCollection.Remove("Anton Ugorenko");
            stringCollection.View();

            stringCollection.Find(s => s.Contains("Vika"));

            Console.WriteLine("\n----- Коллекция int -----");
            CollectionTypeWithoutInterface<int> intCollection = new CollectionTypeWithoutInterface<int>();
            intCollection.Add(5);
            intCollection.Add(10);
            intCollection.View();

            intCollection.SaveToFile("D:\\БГТУ\\ООП\\Лабораторная работа №7\\laba7\\laba7\\obj\\Debug\\ints.txt");
            intCollection.LoadFromFile("D:\\БГТУ\\ООП\\Лабораторная работа №7\\laba7\\laba7\\obj\\Debug\\ints.txt");
            intCollection.View();

            intCollection.Remove(10);
            intCollection.View();

            intCollection.Find(i => i == 5);
            Console.WriteLine("\n----- Коллекция double -----");
            CollectionTypeWithoutInterface<double> doubleCollection = new CollectionTypeWithoutInterface<double>();
            doubleCollection.Add(5.5);
            doubleCollection.Add(10.1);
            doubleCollection.View();

            doubleCollection.SaveToFile("D:\\БГТУ\\ООП\\Лабораторная работа №7\\laba7\\laba7\\obj\\Debug\\doubles.txt");
            doubleCollection.LoadFromFile("D:\\БГТУ\\ООП\\Лабораторная работа №7\\laba7\\laba7\\obj\\Debug\\doubles.txt");
            doubleCollection.View();

            doubleCollection.Remove(10.1);
            doubleCollection.View();

            doubleCollection.Find(d => d > 5.0);
        }
    }
}
