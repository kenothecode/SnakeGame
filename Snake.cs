namespace SnakeGame
{
    internal class Snake
    {
        public List<byte[]> tailPos = new List<byte[]>();
        public byte tailCount { get; set; } = 2;
        public char symbol { get; } = '#';
        public char tailSymbol { get; } = 'S';
        public byte xPos { get; set; }
        public byte yPos { get; set; }

        byte screenX, screenY;

        public Snake(byte x, byte y, byte maxX, byte maxY)
        {
            this.xPos = x; 
            this.yPos = y;

            this.screenX = maxX;
            this.screenY = maxY;
        }

        public void AddTail()
        {
            this.tailCount++;
        }

        public void MoveSnake(byte dir)
        {
            byte[] oldPos = { xPos, yPos };

            switch (dir)
            {
                case Program.UP:
                    yPos--;
                    break;
                case Program.DOWN:
                    yPos++;
                    break;
                case Program.LEFT:
                    xPos--;
                    break;
                case Program.RIGHT:
                    xPos++;
                    break;
                default:
                    break;
            }

            tailPos.Insert(0, oldPos);

            while (tailPos.Count() > tailCount)
                tailPos.RemoveAt(tailPos.Count() - 1);
        }

        public bool CheckTailCol()
        {
            if (xPos > this.screenX - 1 || xPos < 0 || yPos > this.screenY - 1 || yPos <= 0)
                return true;

            foreach (byte[] pos in this.tailPos)
                if (pos[0] == this.xPos && pos[1] == this.yPos) return true;
            
            return false;
        }

        public bool CheckTailCol(byte x, byte y)
        {
            foreach (byte[] pos in this.tailPos)
                if (pos[0] == x && pos[1] == y) return true;

            return false;
        }
    }
}
