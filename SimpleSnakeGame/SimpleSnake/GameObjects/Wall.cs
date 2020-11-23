
namespace SimpleSnake.GameObjects
{
    public class Wall : Point
    {
        private const char WallHorizontalSymbol = '\u25A0';
        private const char WallVerticlSymbol = '\u25AE';
        public Wall(int leftX, int topY) 
            : base(leftX, topY)
        {
            this.LeftX = leftX;
            this.TopY = topY;
            this.InitializeWallBorders();
        }
        public bool IsPointOfWall(Point snake)
        {
            return snake.TopY == 0 || snake.LeftX == 0 ||
                (snake.LeftX == this.LeftX - 1) || snake.TopY == this.TopY;
        }
        private void SetHorizontalLine(int topY)
        {
            for (int i = 0; i < this.LeftX; i++)
            {
               this.Draw(i, topY, WallHorizontalSymbol);
            }
        }

        private void SetVerticalLine(int leftX)
        {
            for (int i = 0; i < this.TopY; i++)
            {
                this.Draw(leftX, i, WallVerticlSymbol);
            }
        }

        private void InitializeWallBorders()
        {
            this.SetHorizontalLine(0);
            this.SetHorizontalLine(this.TopY);

            this.SetVerticalLine(0);
            this.SetVerticalLine(this.LeftX - 1);
        }
    }
}
