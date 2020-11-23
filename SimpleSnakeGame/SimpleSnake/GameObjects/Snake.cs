using System;
using System.Linq;
using System.Collections.Generic;

using SimpleSnake.GameObjects.Foods;

namespace SimpleSnake.GameObjects
{
    public class Snake
    {
        private const char SnakeSymbol = '\u25CF';
        private const char EmptySpace = ' ';

        private Queue<Point> snakeELements;
        private Food[] food;
        private Wall wall;
        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;
        public Snake(Wall wall)    
        {
            this.wall = wall;
            this.snakeELements = new Queue<Point>();
            this.food = new Food[3];
            this.foodIndex = this.RandomFoodNumber;
            this.GetFoods();
            this.CreateSnake();
        }
        public int TotalPoints { get; private set; }
        private int RandomFoodNumber => new Random().Next(0, this.food.Length);

        private void CreateSnake()
        {
            for (int i = 1; i <= 6; i++)
            {
                this.snakeELements.Enqueue(new Point(2, i));
            }
        }

        private void GetFoods()
        {
            this.food[0] = new FoodHash(this.wall);
            this.food[1] = new FoodDollar(this.wall);
            this.food[2] = new FoodAsterisk(this.wall);
        }

        public bool IsMoving(Point direction)
        {
            Point currentSnakeHead = this.snakeELements.Last();

            this.GetNextPoint(direction, currentSnakeHead);

            bool isPointOfSnake = this.snakeELements.Any(x => x.LeftX == this. nextLeftX && x.TopY == this.nextTopY);

            if (isPointOfSnake)
            {
                return false;
            }

            Point snakeNewHead = new Point(this.nextLeftX, this.nextTopY);
         
            if (this.wall.IsPointOfWall(snakeNewHead))
            {
                return false;
            }


            if(this.food[this.foodIndex].IsFoodPoint(snakeNewHead))
            {
                this.Eat(direction, currentSnakeHead);
            }

            this.snakeELements.Enqueue(snakeNewHead);
            snakeNewHead.Draw(SnakeSymbol);
            Point snakeTail = this.snakeELements.Dequeue();
            snakeTail.Draw(EmptySpace);

            return true;
        }

        private void Eat(Point direction, Point currentSnakeHead)
        {
            this.TotalPoints+= this.food[this.foodIndex].FoodPoints;
            int length = this.food[this.foodIndex].FoodPoints;
            for (int i = 0; i < length; i++)
            {
                this.snakeELements.Enqueue(new Point(this.nextLeftX, this.nextTopY));
                GetNextPoint(direction, currentSnakeHead);
            }

            this.foodIndex = this.RandomFoodNumber;
            this.food[this.foodIndex].SetRandomPosition(this.snakeELements);
        }

       
        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;

        }
    }
}
