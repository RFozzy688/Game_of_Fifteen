using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Timers;

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
    struct Indexes
    {
        public int i;
        public int j;
    }
    public delegate void DelegateMoveCursor();
    internal class Puzzle
    {
        private Fields _sizeField;
        private Shuffle _methodShuffle;
        private Number[,] _array;
        private Point _currentCursorPos;
        private Indexes _currentIndexes;
        private int _lastPosX;
        private int _lastPosY;
        private static DateTime timeStart;
        private static Timer timer;
        private string _formatStr = @"hh\:mm\:ss";
        private int _count;
        public Puzzle(int sizeField, int methodShuffle)
        {
            switch (sizeField)
            {
                case 49: 
                    _sizeField = Fields.field_3x3;
                    _lastPosX = 27;
                    _lastPosY = 18;
                    break;
                case 50: 
                    _sizeField = Fields.field_4x4;
                    _lastPosX = 34;
                    _lastPosY = 22;
                    break;
            }

            switch (methodShuffle)
            {
                case 49: _methodShuffle = Shuffle.machine; break;
                case 50: _methodShuffle = Shuffle.hand; break;
            }

            _count = 0;
        }
        public void DrawGrid()
        {
            int top = 5;
            int left = 10;
            int horizontalLenght = 7 * (int)_sizeField;
            int verticalLenght = 4 * (int)_sizeField;

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
            _array = new Number[(int)_sizeField, (int)_sizeField];

            int num = 1;

            int left = 13;
            int top = 7;

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

            _currentIndexes.i = _array.GetLength(0) - 1;
            _currentIndexes.j = _array.GetLength(1) - 1;
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
        public ConsoleKeyInfo MoveCursor()
        {
            ConsoleKeyInfo key = Console.ReadKey();

            if (CheckKey(key))
            {
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    MoveLeft();
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    MoveRight();
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    MoveUp();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    MoveDown();
                }

                Console.SetCursorPosition(_currentCursorPos.X, _currentCursorPos.Y);

                if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow ||
                    key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow)
                {
                    _count++;
                }
            }

            return key;
        }
        private void MoveRight()
        {
            if (_currentCursorPos.X + 7 <= _lastPosX)
            {
                Swap(_currentIndexes.i, _currentIndexes.j, _currentIndexes.i, _currentIndexes.j + 1);

                PrintCell(_currentIndexes.i, _currentIndexes.j);
                PrintCell(_currentIndexes.i, _currentIndexes.j + 1);

                _currentIndexes.j += 1;

                _currentCursorPos.X += 7;
            }
        }
        private void MoveLeft()
        {
            if (_currentCursorPos.X - 7 >= 13)
            {
                Swap(_currentIndexes.i, _currentIndexes.j, _currentIndexes.i, _currentIndexes.j - 1);

                PrintCell(_currentIndexes.i, _currentIndexes.j);
                PrintCell(_currentIndexes.i, _currentIndexes.j - 1);

                _currentIndexes.j -= 1;

                _currentCursorPos.X -= 7;
            }
        }
        private void MoveUp()
        {
            if (_currentCursorPos.Y - 4 >= 6)
            {
                Swap(_currentIndexes.i, _currentIndexes.j, _currentIndexes.i - 1, _currentIndexes.j);

                PrintCell(_currentIndexes.i, _currentIndexes.j);
                PrintCell(_currentIndexes.i - 1, _currentIndexes.j);

                _currentIndexes.i -= 1;

                _currentCursorPos.Y -= 4;
            }
        }
        private void MoveDown()
        {
            if (_currentCursorPos.Y + 4 <= _lastPosY)
            {
                Swap(_currentIndexes.i, _currentIndexes.j, _currentIndexes.i + 1, _currentIndexes.j);

                PrintCell(_currentIndexes.i, _currentIndexes.j);
                PrintCell(_currentIndexes.i + 1, _currentIndexes.j);

                _currentIndexes.i += 1;

                _currentCursorPos.Y += 4;
            }
        }
        private void Swap(int cur_i, int cur_j, int target_i, int target_j)
        {
            _array[cur_i, cur_j].value = _array[target_i, target_j].value;
            _array[target_i, target_j].value = 0;
        }
        private void PrintCell(int index_i,  int index_j)
        {
            Console.SetCursorPosition(_array[index_i, index_j].pos.X, _array[index_i, index_j].pos.Y);
            Console.Write("  ");
            Console.SetCursorPosition(_array[index_i, index_j].pos.X, _array[index_i, index_j].pos.Y);

            if (_array[index_i, index_j].value != 0)
            {
                Console.Write(_array[index_i, index_j].value);
            }
        }
        public void ShufflePuzzle()
        {
            if (_methodShuffle == Shuffle.machine)
            {
                MachineShuffle();
            }
            else
            {
                HandShuffle();
            }
        }
        private void MachineShuffle()
        {
            Random random = new Random();
            int direction;
            int count;

            DelegateMoveCursor[] directionMove = { MoveLeft, MoveDown, MoveRight, MoveUp };

            for (int i = 0; i < 1000; i++)
            {
                direction = random.Next(0, 4);
                count = random.Next(0, 4);

                for (int j = 1; j <= count; j++)
                {
                    directionMove[direction]();
                }
            }
        }
        private void HandShuffle()
        {
            Console.SetCursorPosition(10, 3);
            Console.Write("Для старта нажмите - ENTER");
            Console.SetCursorPosition(_currentCursorPos.X, _currentCursorPos.Y);

            while (true)
            {
                if (MoveCursor().Key == ConsoleKey.Enter)
                {
                    if (_count == 0)
                    {
                        continue;
                    }

                    break;
                }
            }

            PrintCell(_currentIndexes.i, _currentIndexes.j);

            Console.SetCursorPosition(0, 3);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(_currentCursorPos.X, _currentCursorPos.Y);
        }
        public bool IsCheckOnGameOver() 
        {
            int count = 1;

            for (int i = 0; i < _array.GetLength(0); i++)
            {
                for (int j = 0; j < _array.GetLength(1); j++)
                {
                    if (_array[i, j].value != count) { return false; }

                    count++;

                    if (_sizeField == Fields.field_4x4 && count == 16) { break; }
                    if (_sizeField == Fields.field_3x3 && count == 9) { break; }
                }
            }
            return true;
        }
        public void StartTimer()
        {
            timeStart = DateTime.Now;
            timer = new Timer();

            timer.Interval = 1000;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += OnTimedEvent;
        }
        public void StopTimer() 
        { 
            timer.Stop();
            timer.Dispose();
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            TimeSpan interval = DateTime.Now - timeStart;

            Console.SetCursorPosition(10, 1);
            Console.Write($"Время: {interval.ToString(_formatStr)}");
            Console.SetCursorPosition(_currentCursorPos.X, _currentCursorPos.Y);
        }
        public void PrintCount()
        {
            Console.SetCursorPosition(10, 2);
            Console.Write($"Количество ходов: {_count}");
            Console.SetCursorPosition(_currentCursorPos.X, _currentCursorPos.Y);
        }
        public void ResetCount()
        {
            _count = 0;
        }
        private bool CheckKey(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow ||
                key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow ||
                key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Escape)
            {
                return true;
            }
            else
            {
                Console.SetCursorPosition(_currentCursorPos.X, _currentCursorPos.Y);
                Console.Write(" ");
                Console.SetCursorPosition(_currentCursorPos.X, _currentCursorPos.Y);

                return false;
            }
        }
    }
}
