using System;
using System.Threading;
using System.Collections.Generic;

using SimpleSnake.Enums;
using SimpleSnake.GameObjects;

namespace SimpleSnake.Core
{
    public class Engine
    {
        private readonly Dictionary<Direction, Point> directionPoints;
        private readonly Snake snake;
        private readonly Wall wall;
        private Direction direction;
        private double sleepTime;
        private int level;
        public Engine(Wall wall, Snake snake)
        {
            this.wall = wall;
            this.snake = snake;
            this.direction = Direction.Right;
            this.directionPoints = new Dictionary<Direction, Point>();
            this.sleepTime = 100;
            this.level = 1;
           
        }
        public int Level { get; private set; }
       
        public void Run()
        {
            this.CreateDirections();
            while (true)
            {
               
                if(Console.KeyAvailable)
                {
                    this.GetNextDirection();
                }

                Point currentPointDirection = this.directionPoints[this.direction];
               bool isMoved= this.snake.IsMoving(currentPointDirection);
                
                if (!isMoved)
                {
                    this.AskUserForRestart();
                }
                this.sleepTime -= 0.01;
                
                Thread.Sleep((int)sleepTime);
            }

        }
        private void GetNextDirection()
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.RightArrow:
                    if (this.direction!=Direction.Left)
                    {
                        this.direction = Direction.Right;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (this.direction != Direction.Right)
                    {
                        this.direction = Direction.Left;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (this.direction != Direction.Up)
                    {
                        this.direction = Direction.Down;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (this.direction != Direction.Down)
                    {
                        this.direction = Direction.Up;
                    }
                    break;
               
            }

            Console.CursorVisible = false;
        }

        private void AskUserForRestart()
        {
            int leftX = this.wall.LeftX + 1;
            int topY = 3;

            Console.SetCursorPosition(leftX, topY);
            Console.Write("Would you like to continue? y/n");
            string input = Console.ReadLine();

            if (input=="y")
            {
                Console.Clear();
                StartUp.Main();
            }
            else
            {
                StopGame();
            }
        }

        private void StopGame()
        {
            Console.Clear();
            Console.SetCursorPosition(20, 10);
            Console.WriteLine("GAME OVER!!!");
            Console.WriteLine($"Total points: {snake.TotalPoints}");
            Console.WriteLine($"Level: {this.level}");
            Environment.Exit(0);
        }
        private void CreateDirections()
        {
            this.directionPoints.Add(Direction.Right, new Point(1, 0));
            this.directionPoints.Add(Direction.Left, new Point(-1, 0));
            this.directionPoints.Add(Direction.Down, new Point(0, 1));
            this.directionPoints.Add(Direction.Up, new Point(0, -1));
            this.Level++;
        }
    }
}
