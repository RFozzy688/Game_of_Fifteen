using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Fifteen
{
    enum Fields
    {
        field_3x3 = 3,
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
        public void DrawGrid()
        {
            int top = 4;
            int left = 10;
            int horizontalLenght = 7 * (int)SizeField;
            int verticalLenght = 4 * (int)SizeField;

            Console.SetCursorPosition(left, top);

            for (int i = 0; i <= verticalLenght; i++)
            {
                for (int j = 0; j <= horizontalLenght; j++)
                {
                    // вверх
                    if (i == 0 && j == 0)
                    {
                        Console.Write("\u2554"); // левый угол двойной вверх
                        continue;
                    }

                    if (i == 0 && j == horizontalLenght)
                    {
                        Console.Write("\u2557"); // правый угол двойной вверх
                        Console.SetCursorPosition(left, ++top);
                        continue;
                    }

                    if (i == 0 && j % 7 == 0)
                    {
                        Console.Write("\u2564"); // горизонтальная двойная перемычка вверх
                        continue;
                    }

                    if (i == 0 && j > 0 && j % 7 != 0)
                    {
                        Console.Write("\u2550"); // горизонтальная двойная линия
                        continue;
                    }

                    // середина
                    if (i % 4 == 0 && i < verticalLenght && j == 0)
                    {
                        Console.Write("\u255f"); // вертикальная двойная перемычка с лева
                        continue;
                    }

                    if (i % 4 == 0 && i < verticalLenght && j == horizontalLenght)
                    {
                        Console.Write("\u2562"); // вертикальная двойная перемычка с права
                        Console.SetCursorPosition(left, ++top);
                        continue;
                    }

                    if (i > 0 && i < verticalLenght && i % 4 == 0 && j > 0 && j < horizontalLenght && j % 7 == 0)
                    {
                        Console.Write("\u253c"); // одинарная крестовина
                        continue;
                    }

                    if (i % 4 == 0 && i < verticalLenght && j > 0 && j < horizontalLenght)
                    {
                        Console.Write("\u2500"); // горизонтальная одинарная линия
                        continue;
                    }

                    if (i > 0 && i < verticalLenght && j % 7 == 0 && j > 0 && j < horizontalLenght)
                    {
                        Console.Write("\u2502"); // вертикальная одинарная линия
                        continue;
                    }

                    if (i > 0 && i < verticalLenght && j == 0)
                    {
                        Console.Write("\u2551"); // вертикальная двойная линия с лева
                        continue;
                    }

                    if (i > 0 && i < verticalLenght && j == horizontalLenght)
                    {
                        Console.Write("\u2551"); // вертикальная двойная линия с права
                        Console.SetCursorPosition(left, ++top);
                        continue;
                    }

                    // низ
                    if (i == verticalLenght && j == 0)
                    {
                        Console.Write("\u255a"); // левый угол двойной низ
                        continue;
                    }

                    if (i == verticalLenght && j == horizontalLenght)
                    {
                        Console.Write("\u255d"); // правый угол двойной низ
                        continue;
                    }

                    if (i == verticalLenght && j % 7 == 0)
                    {
                        Console.Write("\u2567"); // горизонтальная двойная перемычка низ
                        continue;
                    }

                    if (i == verticalLenght && j > 0 && j % 7 != 0)
                    {
                        Console.Write("\u2550"); // горизонтальная двойная линия
                        continue;
                    }

                    Console.Write(" ");
                }
            }
        }
    }
}
