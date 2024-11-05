namespace laba6
{
    partial class Window : Oformlenie, IOformlenie
    {
        public string NameOfWindow { get; private set; }
        public int TransparencyLevel { get; private set; }

        public Window(ColorType color, Dimensions size, string nameOfWindow, int transparencyLevel)
            : base(color, size)
        {
            if (transparencyLevel < 0 || transparencyLevel > 10)
                throw new InvalidNestingLevelException("Уровень прозрачности должен быть от 0 до 10.");

            NameOfWindow = nameOfWindow;
            TransparencyLevel = transparencyLevel;
        }
    }
}
