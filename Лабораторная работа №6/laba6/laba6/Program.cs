using laba6;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace laba6
{
    public class InvalidDimensionsException : Exception
    {
        public InvalidDimensionsException(string message) : base(message) { }
    }

    public class InvalidNestingLevelException : Exception
    {
        public InvalidNestingLevelException(string message) : base(message) { }
    }

    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(string message) : base(message) { }
    }

    enum ColorType
    {
        Yellow,
        Purple,
        Orange,
        Brown
    }

    struct Dimensions
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Dimensions(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new InvalidDimensionsException("Ширина и высота должны быть положительными числами.");

            Width = width;
            Height = height;
        }

        public int CalculateArea() => Width * Height;

        public override string ToString()
        {
            return $"Размер (Width={Width}, Height={Height})";
        }
    }

    interface IOformlenie
    {
        string Color { get; }
        string GetDetails();
        void PrintDetails();
    }

    abstract class Oformlenie
    {
        public ColorType Color { get; set; }
        public Dimensions Size { get; set; }

        public Oformlenie(ColorType color, Dimensions size)
        {
            Color = color;
            Size = size;
        }

        public abstract string GetDetails();

        public virtual void PrintDetails()
        {
            Console.WriteLine("Оформление: " + GetDetails());
        }
    }

    class Menu : Oformlenie, IOformlenie
    {
        public string TypeOfMenu { get; private set; }
        public string NameOfMenu { get; private set; }
        public int NestingLevel { get; private set; }

        public Menu(ColorType color, Dimensions size, string typeOfMenu, string nameOfMenu, int nestingLevel)
            : base(color, size)
        {
            if (nestingLevel <= 0)
                throw new InvalidNestingLevelException("Уровень вложенности должен быть положительным.");

            TypeOfMenu = typeOfMenu;
            NameOfMenu = nameOfMenu;
            NestingLevel = nestingLevel;
        }

        string IOformlenie.Color => Color.ToString();

        public override string GetDetails()
        {
            return $"Menu{{ Color='{Color}', TypeOfMenu='{TypeOfMenu}', NameOfMenu='{NameOfMenu}', NestingLevel={NestingLevel}, Size={Size} }}";
        }

        public override void PrintDetails()
        {
            Console.WriteLine("Menu: " + GetDetails());
        }
    }

    class Button : Oformlenie, IOformlenie
    {
        public string NameOfButton { get; private set; }
        public int NestingLevel { get; private set; }

        public Button(ColorType color, Dimensions size, string nameOfButton, int nestingLevel)
            : base(color, size)
        {
            if (nestingLevel <= 0)
                throw new InvalidNestingLevelException("Уровень вложенности должен быть положительным.");

            NameOfButton = nameOfButton;
            NestingLevel = nestingLevel;
        }

        string IOformlenie.Color => Color.ToString();

        public override string GetDetails()
        {
            return $"Button{{ Color='{Color}', NameOfButton='{NameOfButton}', NestingLevel={NestingLevel}, Size={Size} }}";
        }

        public override void PrintDetails()
        {
            Console.WriteLine("Button: " + GetDetails());
        }
    }

  

    class OformlenieContainer
    {
        private List<IOformlenie> items = new List<IOformlenie>();

        public void Add(IOformlenie item)
        {
            items.Add(item);
        }

        public void PrintItems()
        {
            foreach (var item in items)
            {
                item.PrintDetails();
            }
        }

        public List<IOformlenie> GetItems()
        {
            return items;
        }
    }

    class OformlenieController
    {
        private OformlenieContainer container;

        public OformlenieController(OformlenieContainer container)
        {
            this.container = container;
        }

        public List<Menu> FindMenusByNestingLevel(int level)
        {
            var result = new List<Menu>();
            foreach (var item in container.GetItems())
            {
                if (item is Menu menu && menu.NestingLevel == level)
                {
                    result.Add(menu);
                }
            }
            return result;
        }

        public List<Button> FindButtonsByNestingLevel(int level)
        {
            var result = new List<Button>();
            foreach (var item in container.GetItems())
            {
                if (item is Button button && button.NestingLevel == level)
                {
                    result.Add(button);
                }
            }
            return result;
        }

        public void CalculateAllWindowsFreeSpace()
        {
            foreach (var item in container.GetItems())
            {
                if (item is Window window)
                {
                    int area = window.Size.CalculateArea();
                    Console.WriteLine($"Window '{window.NameOfWindow}' имеет свободное пространство: {area} кв.ед.");
                }
            }
        }
    }


class Program
{
    static void Main(string[] args)
    {
        try
        {
            OformlenieContainer container = new OformlenieContainer();

            try
            {
                container.Add(new Window(ColorType.Yellow, new Dimensions(-120, 200), "окно1", 5));
            }
            catch (InvalidDimensionsException ex)
            {
                Console.WriteLine($"Ошибка при добавлении окна: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Блок finally для некорректных размеров выполнен.");
            }

            try
            {
                container.Add(new Menu(ColorType.Purple, new Dimensions(100, 150), "вертикальное", "меню1", -2));
            }
            catch (InvalidNestingLevelException ex)
            {
                Console.WriteLine($"Ошибка при добавлении меню: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Блок finally для некорректного уровня вложенности выполнен.");
            }
            try
            {
                int result = DivideNumbers(10, 0); 
                Console.WriteLine($"Результат деления: {result}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Ошибка на уровне Main: {ex.Message}");
                Console.WriteLine($"Стек вызовов: {ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine("Блок finally для деления на ноль выполнен.");
            }

            try
            {
                Console.WriteLine("Элемент с индексом 10: ");
                container.GetItems()[10].PrintDetails(); 
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка: неверный индекс. {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Блок finally для неверного индекса выполнен.");
            }

            try
            {
                ReadFile("oop.txt");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Ошибка: файл не найден. {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Блок finally для работы с файлами выполнен.");
            }

            container.Add(new Window(ColorType.Orange, new Dimensions(130, 210), "окно2", 3));
            container.Add(new Menu(ColorType.Brown, new Dimensions(110, 160), "горизонтальное", "меню2", 1));
            container.Add(new Button(ColorType.Yellow, new Dimensions(50, 30), "Кнопка1", 2));
            container.Add(new Button(ColorType.Purple, new Dimensions(50, 30), "Кнопка2", 1));

            Console.WriteLine("--- Содержимое контейнера ---");
            container.PrintItems();
            Console.WriteLine("-----------------------------");

            OformlenieController controller = new OformlenieController(container);

            Console.WriteLine("--- Меню с уровнем вложенности 1 ---");
            var foundMenus = controller.FindMenusByNestingLevel(1);
            if (foundMenus.Count == 0)
                throw new ElementNotFoundException("Меню с уровнем вложенности 1 не найдено.");

            foreach (var menu in foundMenus)
            {
                menu.PrintDetails();
            }
            Console.WriteLine("------------------------------------");

            Console.WriteLine("--- Кнопки с уровнем вложенности 2 ---");
            var foundButtons = controller.FindButtonsByNestingLevel(2);
            if (foundButtons.Count == 0)
                throw new ElementNotFoundException("Кнопки с уровнем вложенности 2 не найдены.");

            foreach (var button in foundButtons)
            {
                button.PrintDetails();
            }
            Console.WriteLine("--------------------------------------");

            Console.WriteLine("--- Расчет свободного места в окнах ---");
            controller.CalculateAllWindowsFreeSpace();
        }
        catch (InvalidDimensionsException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (InvalidNestingLevelException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (ElementNotFoundException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Произошла неожиданная ошибка: {ex.Message}");
            Console.WriteLine($"Стек вызовов: {ex.StackTrace}");
        }
        finally
        {
            Console.WriteLine("Блок finally для основного кода выполнен.");
        }
        Debugger.Launch();
    }
        static int DivideNumbers(int a, int b)
        {
            Debug.Assert(b != 0, "Делитель не должен быть равен нулю"); 
            return a / b;
        }

        static void ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл {filePath} не найден.");
            }
        }
    }
}
