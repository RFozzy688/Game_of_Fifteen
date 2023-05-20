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
            int fieldSelection = 0;
            int initialShuffle = 0;
            ConsoleKeyInfo key;

            while (true)
            {
                ChoiceField(ref fieldSelection);
                ChoiceShuffle(ref initialShuffle);

                Puzzle puzzle = new Puzzle(fieldSelection, initialShuffle);

                fieldSelection = 0;
                initialShuffle = 0;

                while (true)
                {
                    Console.Clear();

                    puzzle.DrawGrid();
                    puzzle.CreateArray();
                    puzzle.PrintArray();

                    Console.SetCursorPosition(10, 3);
                    Console.Write("Закончить игру - ESC");

                    puzzle.ShufflePuzzle();
                    puzzle.StartTimer();
                    puzzle.ResetCount();

                    while (true)
                    {
                        key = puzzle.MoveCursor();
                        puzzle.PrintCount();

                        if (key.Key == ConsoleKey.Escape)
                        {
                            break;
                        }

                        if (puzzle.IsCheckOnGameOver() == true)
                        {
                            break;
                        }
                    }

                    puzzle.StopTimer();
                    puzzle.ResetCount();

                    while (true)
                    {
                        Console.SetCursorPosition(10, 3);
                        Console.Write("Начать заново - ENTER | Выйти в меню - ESC");
                        Console.SetCursorPosition(0, 4);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(10, 4);
                        Console.Write("> ");

                        key = Console.ReadKey();

                        if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Escape) 
                        { 
                            break;
                        }
                    }

                    if (key.Key == ConsoleKey.Escape) { break; }
                }
            }
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
        }
    }
}
