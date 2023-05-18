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
            int fieldSelection = 0, initialShuffle = 0;

            ChoiceField(ref fieldSelection);
            ChoiceShuffle(ref initialShuffle);

            Console.WriteLine($"field: {fieldSelection} shuffle: {initialShuffle}");
        }

        static void ChoiceField(ref int fieldSelection) 
        {
            ConsoleKeyInfo consoleKeyInfo;

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
                case 49: fieldSelection = 1; break;
                case 50: fieldSelection = 2; break;
            }
        }
        static void ChoiceShuffle(ref int initialShuffle)
        {
            ConsoleKeyInfo consoleKeyInfo;

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
                case 49: initialShuffle = 1; break;
                case 50: initialShuffle = 2; break;
            }
        }
    }
}
