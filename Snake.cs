namespace SnakeGame
{
    internal class Snake
    {
        public List<byte[]> TailPos { get; set; } = new List<byte[]>();
        public byte TailCount { get; set; } = 2;
        public char Symbol { get; } = '#';        
        public byte XPos { get; set; }
        public byte YPos { get; set; }

        byte screenX, screenY;

        public Snake(byte x, byte y, byte maxX, byte maxY)
        {
            this.XPos = x; 
            this.YPos = y;

            this.screenX = maxX;
            this.screenY = maxY;
        }

        public void AddTail()
        {
            this.TailCount++;
        }

        public void MoveSnake(byte dir)
        {
            byte[] oldPos = { XPos, YPos };

            switch (dir)
            {
                case Program.UP:
                    YPos--;
                    break;
                case Program.DOWN:
                    YPos++;
                    break;
                case Program.LEFT:
                    XPos--;
                    break;
                case Program.RIGHT:
                    XPos++;
                    break;
                default:
                    break;
            }

            TailPos.Insert(0, oldPos);

            while (TailPos.Count() > TailCount)
                TailPos.RemoveAt(TailPos.Count() - 1);
        }

        public bool CheckTailCol()
        {
            if (XPos > this.screenX - 1 || XPos < 0 || YPos > this.screenY - 1 || YPos <= 0)
                return true;

            foreach (byte[] pos in this.TailPos)
                if (pos[0] == this.XPos && pos[1] == this.YPos) return true;
            
            return false;
        }

        public bool CheckTailCol(byte x, byte y)
        {
            foreach (byte[] pos in this.TailPos)
                if (pos[0] == x && pos[1] == y) return true;

            return false;
        }
    }
}
