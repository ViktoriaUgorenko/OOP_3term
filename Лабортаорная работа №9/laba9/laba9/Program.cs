using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace laba10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            services.Add("1", new Service("1", "Сервис электронной почты", "Обрабатывает отправку электронной почты."));
            services.Add("2", new Service("2", "Платежный сервис", "Обрабатывает платежные транзакции."));
            services.Add("3", new Service("3", "Сервис уведомлений", "Обрабатывает отправку push-уведомлений."));

            Console.WriteLine("Все услуги:");
            foreach (DictionaryEntry entry in services)
            {
                Console.WriteLine(entry.Value);
            }

            Queue<int> queue = new Queue<int>();

            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);
            queue.Enqueue(40);
            queue.Enqueue(50);

            Console.WriteLine("\nСодержимое очереди:");
            foreach (var item in queue)
            {
                Console.WriteLine(item);
            }

            int n = 2;
            Console.WriteLine($"\nУдаление {n} элементов из очереди:");
            for (int i = 0; i < n && queue.Count > 0; i++)
            {
                Console.WriteLine($"Удалено: {queue.Dequeue()}");
            }

            Console.WriteLine("\nДобавление новых элементов в очередь:");
            queue.Enqueue(60);
            queue.Enqueue(70);
            queue.Enqueue(80);

            foreach (var item in queue)
            {
                Console.WriteLine(item);
            }

            Stack<int> stack = new Stack<int>();
            while (queue.Count > 0)
            {
                stack.Push(queue.Dequeue());
            }

            Console.WriteLine("\nСодержимое стека:");
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }

            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            int key = 1;

            Console.WriteLine("\nЗаполнение словаря с использованием сгенерированных ключей:");
            foreach (var service in services.Values)
            {
                dictionary[key++] = service.ToString();
            }

            foreach (var pair in dictionary)
            {
                Console.WriteLine($"Ключ: {pair.Key}, Значение: {pair.Value}");
            }

            Console.WriteLine("\nПоиск значения 60 в стеке:");
            int searchValue = 60;
            if (stack.Contains(searchValue))
            {
                Console.WriteLine($"Значение {searchValue} найдено в стеке.");
            }
            else
            {
                Console.WriteLine($"Значение {searchValue} не найдено в стеке.");
            }

            ObservableCollection<Service> observableServices = new ObservableCollection<Service>();

            observableServices.CollectionChanged += OnCollectionChanged;

            Console.WriteLine("\nРабота с ObservableCollection:");

            Console.WriteLine("\nДобавление улуг в ObservableCollection:");
            observableServices.Add(new Service("4", "Сервис пользователей", "Обрабатывает управление пользователями."));
            observableServices.Add(new Service("5", "Сервис логирования", "Обрабатывает логирование приложений."));

            Console.WriteLine("\nУдаление услуги из ObservableCollection:");
            observableServices.Remove(observableServices[0]);

            Console.ReadLine(); 
        }

        private static void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Console.WriteLine("Элемент(ы) добавлены в коллекцию:");
                    foreach (Service item in e.NewItems)
                    {
                        Console.WriteLine($"  {item}");
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    Console.WriteLine("Элемент(ы) удалены из коллекции:");
                    foreach (Service item in e.OldItems)
                    {
                        Console.WriteLine($"  {item}");
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    Console.WriteLine("Элемент(ы) заменены в коллекции.");
                    break;

                case NotifyCollectionChangedAction.Move:
                    Console.WriteLine("Элемент(ы) перемещены в коллекции.");
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Console.WriteLine("Коллекция была сброшена.");
                    break;
            }
        }
    }

    public class Service
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Service(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return $"Услуга [Id:{Id}, Название:{Name}, Описание:{Description}]";
        }
    }

    public class ServiceCollection : IOrderedDictionary, ICollection
    {
        private readonly OrderedDictionary _services = new OrderedDictionary();

        public object this[int index]
        {
            get => _services[index];
            set => _services[index] = value;
        }

        public object this[object key]
        {
            get => _services[key];
            set => _services[key] = value;
        }

        public void Add(object key, object value)
        {
            if (!(value is Service))
                throw new ArgumentException("Значение должно быть типа Service");
            _services.Add(key, value);
        }

        public void Insert(int index, object key, object value)
        {
            if (!(value is Service))
                throw new ArgumentException("Значение должно быть типа Service");
            _services.Insert(index, key, value);
        }

        public void RemoveAt(int index)
        {
            _services.RemoveAt(index);
        }

        public void Remove(object key)
        {
            _services.Remove(key);
        }

        public bool Contains(object key)
        {
            return _services.Contains(key);
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return _services.GetEnumerator();
        }

        public void Clear()
        {
            _services.Clear();
        }

        public int Count => _services.Count;

        public ICollection Keys => _services.Keys;

        public ICollection Values => _services.Values;

        public bool IsSynchronized => false;

        public object SyncRoot => new object();

        public bool IsReadOnly => _services.IsReadOnly;

        public bool IsFixedSize => false; 

        public void CopyTo(Array array, int index)
        {
            _services.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _services.GetEnumerator();
        }
    }
}
