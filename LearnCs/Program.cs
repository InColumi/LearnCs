using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCs
{
    class Program
    {
        class Mountain
        {
            private List<int> _numbers;
            // private List<int> _numbersUp;
            // private List<int> _numbersDown;
            private char[,] _matrix;
            private int _sizePerson;
            private int _max;
            private int _min;
            public Mountain(List<int> numbers)
            {
                // _numbersUp = new List<int>();
                // _numbersDown = new List<int>();
                _numbers = numbers;
                //SetNumberUpAndDown();
                _sizePerson = 3;
                int sumNumers = GetSum(_numbers);
                _max = FindMax();
                _min = FindMin();
                _matrix = new char[_max + _min + _sizePerson, sumNumers + 3];
            }

            private int FindMax()
            {
                int sum = 0;
                int max = sum;
                for (int i = 0; i < _numbers.Count; i++)
                {
                    sum += (i % 2 == 0) ? _numbers[i] : -_numbers[i];
                    if (max < sum)
                    {
                        max = sum;
                    }
                }
                return max;
            }

            private int FindMin()
            {
                int sum = 0;
                int min = sum;
                for (int i = 0; i < _numbers.Count; i++)
                {
                    sum += (i % 2 == 0) ? _numbers[i] : -_numbers[i];
                    if (min > sum)
                    {
                        min = sum;
                    }
                }
                return Math.Abs(min);
            }
            public void Calculate()
            {
                FilMatrix(' ');
                bool isUp = true;
                bool isSit = false;
                int i = _matrix.GetLength(0) - _min - 1;
                int j = 0;
                int iStartPersone = 0;
                int JStartPersone = 0;
                for (int k = 0; k < _numbers.Count; k++)
                {
                    if (isUp)
                    {
                        MoveUp(ref i, ref j, _numbers[k]);
                        isUp = false;
                    }
                    else
                    {
                        MoveDown(ref i, ref j, _numbers[k]);
                        isUp = true;
                    }
                    ++j;
                    if (i == _sizePerson && isSit == false)
                    {
                        ++j;
                        iStartPersone = i - 1;
                        JStartPersone = j - 1;
                        isSit = true;
                    }

                }
                AddPerson(iStartPersone, JStartPersone);
            }

            private void AddPerson(int i, int j)
            {
                _matrix[i - 1, j] = 'o';
                _matrix[i, j] = '|';
                _matrix[i, j - 1] = '/';
                _matrix[i, j + 1] = '\\';
                _matrix[i + 1, j - 1] = '<';
                _matrix[i + 1, j] = ' ';
                _matrix[i + 1, j + 1] = '>';
            }

            private List<int> GetNumber()
            {
                string numbersInLine = Console.ReadLine();
                string[] numbersString = numbersInLine.Split(' ');

                for (int i = 0; i < numbersString.Length; i++)
                {
                    _numbers.Add(int.Parse(numbersString[i]));
                }

                return _numbers;
            }

            private int GetSum(List<int> numbers)
            {
                int sum = 0;
                foreach (var number in numbers)
                {
                    sum += number;
                }
                return sum;
            }

            public void Show()
            {
                for (int i = 0; i < _matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < _matrix.GetLength(1); j++)
                    {
                        Console.Write(_matrix[i, j]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            public void FilMatrix(char symbol)
            {
                for (int i = 0; i < _matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < _matrix.GetLength(1); j++)
                    {
                        _matrix[i, j] = symbol;
                    }
                }
            }

            private void MoveUp(ref int startI, ref int startJ, int countSteps)
            {
                for (int i = 0; i < countSteps - 1; i++)
                {
                    _matrix[startI, startJ] = '/';
                    --startI;
                    ++startJ;
                }
                _matrix[startI, startJ] = '/';
            }

            private void MoveDown(ref int startI, ref int startJ, int countSteps)
            {
                for (int i = 0; i < countSteps - 1; i++)
                {
                    _matrix[startI, startJ] = '\\';
                    ++startI;
                    ++startJ;
                }
                _matrix[startI, startJ] = '\\';
            }
        }

        static void Main(string[] args)
        {
            Random rand = new Random();
            int countNumers = 3;
            string input = string.Empty;
            List<int> numbers = new List<int>();
            //while (input != "exit")
            //{
            //    //input = Console.ReadLine();
            //    //countNumers = int.Parse(input);
            while (Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                for (int i = 0; i < countNumers; i++)
                {
                    numbers.Add(rand.Next(1, countNumers));
                }
                //numbers = new List<int>() { 4, 1, 1, 4, 4 };
                for (int i = 0; i < numbers.Count; i++)
                {
                    Console.Write(numbers[i]);
                    Console.Write(',');
                }
                Console.WriteLine();
                Mountain mountain = new Mountain(numbers);
                mountain.Calculate();
                mountain.Show();
                numbers.Clear();
            }

            //  }

            Console.ReadKey();
        }
    }
}