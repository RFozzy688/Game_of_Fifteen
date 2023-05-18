using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Fifteen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Puzzle puzzle = new Puzzle();

            ChoiceField(puzzle);
            ChoiceShuffle(puzzle);

            Console.WriteLine($"field: {puzzle.SizeField} shuffle: {puzzle.MethodShuffle}");
        }

        static void ChoiceField(Puzzle obj) 
        {
            ConsoleKeyInfo consoleKeyInfo;
            int fieldSelection = 0;

            while (fieldSelection != 49 && fieldSelection != 50) // code 49 => 1
            {
                Console.Clear();

                Console.WriteLine("\n Выбор поля\n");
                Console.WriteLine(" 1 - 8 (3x3) | 2 - 15 (4x4)");
                Console.Write(" > ");

                consoleKeyInfo = Console.ReadKey();

                fieldSelection = Convert.ToInt32(consoleKeyInfo.KeyChar);
            }

            switch (fieldSelection)
            {
                case 49: obj.SizeField = Fields.field_3x3; break;
                case 50: obj.SizeField = Fields.field_4x4; break;
            }
        }
        static void ChoiceShuffle(Puzzle obj)
        {
            ConsoleKeyInfo consoleKeyInfo;
            int initialShuffle = 0;

            while (initialShuffle != 49 && initialShuffle != 50) // code 49 => 1
            {
                Console.Clear();

                Console.WriteLine("\n Начальное размешивание\n");
                Console.WriteLine(" 1 - Авто | 2 - Ручное");
                Console.Write(" > ");

                consoleKeyInfo = Console.ReadKey();

                initialShuffle = Convert.ToInt32(consoleKeyInfo.KeyChar);
            }

            switch (initialShuffle)
            {
                case 49: obj.MethodShuffle = Shuffle.machine; break;
                case 50: obj.MethodShuffle = Shuffle.hand; break;
            }
        }
    }
}
