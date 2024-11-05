using System;

namespace laba6
{
    partial class Window
    {
        string IOformlenie.Color => Color.ToString();

        public override string GetDetails()
        {
            return $"Window{{ Color='{Color}', NameOfWindow='{NameOfWindow}', TransparencyLevel={TransparencyLevel}, Size={Size} }}";
        }

        public override void PrintDetails()
        {
            Console.WriteLine("Window: " + GetDetails());
        }
    }
}
