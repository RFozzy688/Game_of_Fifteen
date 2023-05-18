using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Fifteen
{
    enum Fields
    {
        field_3x3,
        field_4x4
    }
    enum Shuffle
    {
        machine,
        hand
    }
    internal class Puzzle
    {
        public Fields SizeField { get; set; }
        public Shuffle MethodShuffle { get; set; }
        //public void DrawGrid()
    }
}
