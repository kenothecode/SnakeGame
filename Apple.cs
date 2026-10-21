namespace SnakeGame
{
    internal class Apple
    {
        public byte xPos { get; set; }
        public byte yPos { get; set; }
        public char symbol { get; } = 'O';
        byte screenX, screenY;
        private Random rnd;
        public Apple(byte maxX, byte maxY, int? seed = null)
        {
            this.screenX = maxX;
            this.screenY = maxY;
            if (seed != null)
                this.rnd = new Random((int)seed);
            else
                this.rnd = new Random();
        }

        public void RandomizePos(Snake snake)
        {
            do
            {
                xPos = (byte)rnd.Next(0, (int)this.screenX - 1);
                yPos = (byte)rnd.Next(1, (int)this.screenY - 1);
            } while (snake.CheckTailCol(xPos, yPos));
        }
    }
}
