namespace SnakeGame
{
    internal class Apple
    {
        public byte XPos { get; set; }
        public byte YPos { get; set; }
        public char Symbol { get; } = 'O';
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
                XPos = (byte)rnd.Next(0, (int)this.screenX - 1);
                YPos = (byte)rnd.Next(1, (int)this.screenY - 1);
            } while (snake.CheckTailCol(XPos, YPos));
        }
    }
}
