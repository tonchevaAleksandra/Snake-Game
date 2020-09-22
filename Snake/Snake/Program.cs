using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake
{
    class Program
    {
        struct Position
        {
            public int row;
            public int col;
            public Position(int row, int col)
            {
                this.row = row;
                this.col = col;
            }
        }
        static void Main(string[] args)
        {
            Position[] directions = new Position[]
            {
                new Position(0, 1),// right
                new Position(0,-1), //left
                new Position(1, 0), //down
                new Position(-1, 0), //top
            };

            int direction = 0;
            Console.BufferHeight = Console.WindowHeight;
            Random randomNumGenerator = new Random();
            Position food = new Position(randomNumGenerator.Next(0, Console.WindowHeight), randomNumGenerator.Next(0, Console.WindowWidth));
            Console.SetCursorPosition(food.row, food.col);
            Console.Write("@");
            Queue<Position> snakeElements = new Queue<Position>();

            for (int i = 0; i < 6; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }
            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.col, position.row);
                Console.Write("*");
            }


            while (true)
            {
                if(Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();

                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        direction = 0;
                    }
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        direction = 1;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        direction = 2;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        direction = 3;
                    }
                }
               

                Position snakeHead= snakeElements.Last();
                Position nextDirection = directions[direction];
                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row, snakeHead.col + nextDirection.col);
              
                if(snakeNewHead.row<0 || 
                    snakeNewHead.col<0 || snakeNewHead.row>=Console.WindowHeight ||
                    snakeNewHead.col>=Console.WindowWidth || snakeElements.Contains(snakeNewHead))
                {
                    //Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("GAME OVER!!!");
                    Console.WriteLine($"YOUR SCORE : {(snakeElements.Count-6)*10 }");
                    return;
                }
                snakeElements.Enqueue(snakeNewHead);
                if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
                {
                    food = new Position(randomNumGenerator.Next(0, Console.BufferHeight), randomNumGenerator.Next(0, Console.BufferWidth));
                  
                }
                else
                {
                    snakeElements.Dequeue();
                   
                }
               
                
                Console.Clear();

                foreach (Position position in snakeElements)
                {
                    Console.SetCursorPosition(position.col, position.row);
                    Console.Write("*");
                }

                Console.SetCursorPosition(food.row, food.col);
                Console.Write("@");

                Thread.Sleep(200);

            }
        }
    }
}
