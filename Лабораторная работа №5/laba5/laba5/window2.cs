using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5
{
    partial class Window
    {
        public override void PrintDetails()
        {
            Console.WriteLine("Window: " + GetDetails());
        }

        public int CalculateFreeSpace()
        {
            int totalArea = Size.CalculateArea();
            int frameArea = 20 * 20; 
            int totalFramesArea = NumberOfFrames * frameArea;

            return totalArea - totalFramesArea;
        }
    }
}
