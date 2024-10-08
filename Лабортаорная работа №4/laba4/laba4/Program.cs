using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
    interface IOformlenie
    {
        string Color { get; }
        string GetDetails();
        void PrintDetails();
    }

    abstract class Oformlenie
    {
        public string Color { get; set; }

        public Oformlenie(string color)
        {
            Color = color;
        }

        public abstract string GetDetails();

        public virtual void PrintDetails()
        {
            Console.WriteLine("Вывод через оформление: " + GetDetails());
        }

        public override string ToString()
        {
            return $"Oformlenie (Type: {this.GetType().Name}) {{ Color='{Color}' }}";
        }
    }

    class Window : Oformlenie, IOformlenie
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
            return $"Window{{ Color='{Color}', NameOfWind='{NameOfWind}', CountOfWind={CountOfWind} }}";
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

    class Menu : Oformlenie, IOformlenie
    {
        public string TypeOfMenu { get; private set; }
        public string NameOfMenu { get; private set; }

        public Menu(string color, string typeOfMenu, string nameOfMenu) : base(color)
        {
            TypeOfMenu = typeOfMenu;
            NameOfMenu = nameOfMenu;
        }

        public override string GetDetails()
        {
            return $"Menu{{ Color='{Color}', TypeOfMenu='{TypeOfMenu}', NameOfMenu='{NameOfMenu}' }}";
        }

        public override void PrintDetails()
        {
            Console.WriteLine(" Oformlenie (Menu): " + GetDetails());
        }

        public override string ToString()
        {
            return $"Menu (Type: {this.GetType().Name}) {{ Color='{Color}', TypeOfMenu='{TypeOfMenu}', NameOfMenu='{NameOfMenu}' }}";
        }

        string IOformlenie.GetDetails()
        {
            string ddd = $"Interface Menu{{ Color='{Color}', TypeOfMenu='{TypeOfMenu}', NameOfMenu='{NameOfMenu}' }}";
            Console.WriteLine(ddd);
            return ddd;
        }
    }

    sealed class Button : Oformlenie, IOformlenie
    {
        public string ButtonText { get; private set; }

        public Button(string color, string buttonText) : base(color)
        {
            ButtonText = buttonText;
        }

        public override string GetDetails()
        {
            return $"Button{{ Color='{Color}', ButtonText='{ButtonText}' }}";
        }

        public override void PrintDetails()
        {
            Console.WriteLine("Oformlenie (Button): " + GetDetails());
        }

        public override string ToString()
        {
            return $"Button (Type: {this.GetType().Name}) {{ Color='{Color}', ButtonText='{ButtonText}' }}";
        }
    }

    class Printer
    {
        public void IAmPrinting(IOformlenie someobj)
        {
            someobj.PrintDetails();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IOformlenie[] objects = new IOformlenie[]
            {
                new Window("желтый", "окно1", 5),
                new Menu("фиолетовый", "вертикальное", "меню1"),
                new Window("оранжевый", "окно2", 3),
                new Menu("коричневый", "горизонтальное", "меню2"),
                new Button("зеленый", "Кнопка1") 
            };

            Printer printer = new Printer();

            foreach (var obj in objects)
            {
                printer.IAmPrinting(obj);

                Window window = obj as Window;
                if (window != null)
                {
                    Console.WriteLine("Это объект класса Window :)");
                    Console.WriteLine("Приведение к Window прошло успешно, окно называется: " + window.NameOfWind);
                }

                Menu menu = obj as Menu;
                if (menu != null)
                {
                    Console.WriteLine("Это объект класса Menu :)");
                    Console.WriteLine("1111Приведение к Menu прошло успешно, тип меню: " + menu.TypeOfMenu);

                    IOformlenie tr = menu;
                    tr.GetDetails();
                    

                }
                Oformlenie dd = new Menu("красный", "вертикальное", "меню1");
                dd.GetDetails();
                Console.WriteLine(dd.GetDetails());

                Console.WriteLine();
            }
        }
    }
}
