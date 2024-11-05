using laba4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace laba5
{
    partial class Window : Oformlenie, IOformlenie
    {
        public string NameOfWind { get; private set; }
        public int NumberOfFrames { get; private set; }

        public Window(ColorType color, Dimensions size, string nameOfWind, int numberOfFrames)
            : base(color, size)
        {
            NameOfWind = nameOfWind;
            NumberOfFrames = numberOfFrames;
        }

        string IOformlenie.Color => Color.ToString();

        public override string GetDetails()
        {
            return $"Window{{ Color='{Color}', NameOfWind='{NameOfWind}', NumberOfFrames={NumberOfFrames}, Size={Size} }}";
        }
    }
}
