namespace SnakeGame
{
    internal class Snake
    {
        public List<byte[]> TailPos { get; set; }
        public byte TailCount { get; set; } = 2;
        public char Symbol { get; } = '#';        
        public byte XPos { get; set; }
        public byte YPos { get; set; }

        byte screenX, screenY;

        public Snake(byte x, byte y, byte maxX, byte maxY)
        {
            XPos = x; 
            YPos = y;

            screenX = maxX;
            screenY = maxY;

            TailPos = new List<byte[]>();
        }

        public void AddTail()
        {
            TailCount++;
        }

        public void MoveSnake(InputValues dir)
        {
            byte[] oldPos = { XPos, YPos };

            switch (dir)
            {
                case InputValues.Up:
                    YPos--;
                    break;
                case InputValues.Down:
                    YPos++;
                    break;
                case InputValues.Left:
                    XPos--;
                    break;
                case InputValues.Right:
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
            if (XPos > screenX - 1 || XPos < 0 || YPos > screenY - 1 || YPos <= 0)
                return true;

            foreach (byte[] pos in TailPos)
                if (pos[0] == XPos && pos[1] == YPos) return true;
            
            return false;
        }

        public bool CheckTailCol(byte x, byte y)
        {
            foreach (byte[] pos in TailPos)
                if (pos[0] == x && pos[1] == y) return true;

            return false;
        }
    }
}
