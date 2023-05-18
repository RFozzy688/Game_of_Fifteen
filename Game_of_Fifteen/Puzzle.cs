using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
    struct Number
    {
        public int value;
        public Point pos;
    }
    internal class Puzzle
    {
        public Fields SizeField { get; set; }
        public Shuffle MethodShuffle { get; set; }
        private Number[,] _array;
        private Point _currentCursorPos;
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
        public void CreateArray()
        {
            _array = new Number[(int)SizeField, (int)SizeField];

            int num = 1;

            int left = 13;
            int top = 6;

            for (int i = 0; i < _array.GetLength(0); i++)
            {
                for (int j = 0;  j < _array.GetLength(1); j++)
                {
                    _array[i, j].value = num++;
                    _array[i, j].pos.X = left;
                    _array[i, j].pos.Y = top;

                    left += 7;
                }

                top += 4;
                left = 13;
            }

            _array[_array.GetLength(0) - 1, _array.GetLength(1) - 1].value = 0;
        }
        public void PrintArray()
        {
            for (int i = 0; i < _array.GetLength(0); i++)
            {
                for (int j = 0; j < _array.GetLength(1); j++)
                {
                    if (i == _array.GetLength(0) - 1 && j == _array.GetLength(1) - 1)
                    {
                        Console.SetCursorPosition(_array[i, j].pos.X, _array[i, j].pos.Y);
                    }
                    else
                    {
                        Console.SetCursorPosition(_array[i, j].pos.X, _array[i, j].pos.Y);
                        Console.Write(_array[i, j].value);
                    }
                }
            }

            _currentCursorPos = new Point(Console.CursorLeft, Console.CursorTop);
        }
        public void MoveCursor()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            int lastPosX, lastPosY;

            if (SizeField == Fields.field_3x3)
            {
                lastPosX = 27;
                lastPosY = 14;
            }
            else
            {
                lastPosX = 34;
                lastPosY = 18;
            }

            if (key.Key == ConsoleKey.LeftArrow)
            {
                MoveLeft();
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                MoveRight(lastPosX);
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                MoveUp();
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                MoveDown(lastPosY);
            }

            Console.SetCursorPosition(_currentCursorPos.X, _currentCursorPos.Y);
        }
        private void MoveRight(int lastPosX)
        {
            if (_currentCursorPos.X + 7 <= lastPosX)
            {
                _currentCursorPos.X += 7;
            }
        }
        private void MoveLeft()
        {
            if (_currentCursorPos.X - 7 >= 13)
            {
                _currentCursorPos.X -= 7;
            }
        }
        private void MoveUp()
        {
            if (_currentCursorPos.Y - 4 >= 6)
            {
                _currentCursorPos.Y -= 4;
            }
        }
        private void MoveDown(int lastPosY)
        {
            if (_currentCursorPos.Y + 4 <= lastPosY)
            {
                _currentCursorPos.Y += 4;
            }
        }
    }
}
