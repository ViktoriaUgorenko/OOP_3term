using laba5;
using System;
using System.Collections.Generic;

namespace laba4
{
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
            Width = width;
            Height = height;
        }

        public int CalculateArea() => Width * Height;

        public override string ToString()
        {
            return $"Razmer (Width={Width}, Height={Height})";
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
            Console.WriteLine("Оформление" + GetDetails());
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

        public void Remove(IOformlenie item)
        {
            items.Remove(item);
        }

        public List<IOformlenie> GetItems()
        {
            return items;
        }

        public void SetItems(List<IOformlenie> newItems)
        {
            items = newItems;
        }

        public void PrintItems()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Контейнер пуст.");
                return;
            }

            foreach (var item in items)
            {
                item.PrintDetails();
            }
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
            List<Menu> foundMenus = new List<Menu>();
            foreach (var item in container.GetItems())
            {
                if (item is Menu menu && menu.NestingLevel == level)
                {
                    foundMenus.Add(menu);
                }
            }
            return foundMenus;
        }

        public List<Button> FindButtonsByNestingLevel(int level)
        {
            List<Button> foundButtons = new List<Button>();
            foreach (var item in container.GetItems())
            {
                if (item is Button button && button.NestingLevel == level)
                {
                    foundButtons.Add(button);
                }
            }
            return foundButtons;
        }

        public void CalculateAllWindowsFreeSpace()
        {
            foreach (var item in container.GetItems())
            {
                if (item is Window window)
                {
                    int freeSpace = window.CalculateFreeSpace();
                    Console.WriteLine($"Свободное место в окне '{window.NameOfWind}': {freeSpace} пикселей");
                }
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            OformlenieContainer container = new OformlenieContainer();

            container.Add(new Window(ColorType.Yellow, new Dimensions(120, 200), "окно1", 5));
            container.Add(new Menu(ColorType.Purple, new Dimensions(100, 150), "вертикальное", "меню1", 2));
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
            foreach (var menu in foundMenus)
            {
                menu.PrintDetails();
            }
            Console.WriteLine("------------------------------------");

            Console.WriteLine("--- Кнопки с уровнем вложенности 2 ---");
            var foundButtons = controller.FindButtonsByNestingLevel(2);
            foreach (var button in foundButtons)
            {
                button.PrintDetails();
            }
            Console.WriteLine("--------------------------------------");

            Console.WriteLine("--- Расчет свободного места в окнах ---");
            controller.CalculateAllWindowsFreeSpace();
        }
    }
}
