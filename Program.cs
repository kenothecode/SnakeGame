using System.Diagnostics;
using static System.Console;

namespace SnakeGame
{
    internal class Program
    {
        static byte SCREEN_X = (byte)(WindowWidth / 2 - 4); 
        static byte SCREEN_Y = (byte)(WindowHeight - 1); 
        public const byte UP = 101, DOWN = 102, LEFT = 103, RIGHT = 104, ESCAPE = 144;

        static void Main(string[] args)
        {
            byte userInput = 0;
            byte currentDir = UP;
            var timer = new Stopwatch();

            Snake snake = new Snake((byte)(SCREEN_X/2), (byte)(SCREEN_Y/2), SCREEN_X, SCREEN_Y);
            Apple apple;

            if (args.Length > 0 && int.TryParse(args[0], out int seed))
            {
                apple = new Apple(SCREEN_X, SCREEN_Y, seed);
            }
            else
                apple = new Apple(SCREEN_X, SCREEN_Y);


            apple.RandomizePos(snake);

            // Setting up border
            string bottomLine = "";
            string sideLine = "";

            for (int i = 0; i < SCREEN_Y; i++)
            {
                sideLine += "|\n";
                for (int j = 0; j < SCREEN_X; j++)
                    sideLine += "  ";
            }

            for (int i = 0; i < SCREEN_X; i++)
                bottomLine += "--";

            timer.Start();
            Clear();

            while (true) // Main loop
            {
                ForegroundColor = ConsoleColor.DarkYellow;
                WriteAt($"Score: {snake.TailCount - 2}", 0, 0); // Displays score

                // prints border
                ForegroundColor = ConsoleColor.DarkGray;
                WriteAt(sideLine, SCREEN_X, 0);
                WriteAt(bottomLine, 0, SCREEN_Y);

                if (KeyAvailable) // Checks if key has been pressed recently
                {
                    userInput = ReadUserInput(ReadKey().Key); // Converts user input into a number

                    if (userInput != 0)
                        currentDir = userInput; // Assign the direction the snake moves in to be the user input

                    if (userInput == ESCAPE) break; // Exit game by pressing escape
                }

                ForegroundColor = ConsoleColor.Red;
                WriteAt(apple.Symbol, apple.XPos, apple.YPos);

                // move snake in current direction and print it to screen
                snake.MoveSnake(currentDir);
                WriteAt(snake.Symbol, snake.XPos, snake.YPos);

                //print the tail of the snake as well
                for (byte i = 0; i < snake.TailPos.Count(); i++)
                {
                    // Some nice color variation to make the movement more visible
                    if (i % 5 == 0 && i % 3 == 0)
                        ForegroundColor = ConsoleColor.Yellow;
                    else if (i % 5 == 0)
                        ForegroundColor = ConsoleColor.DarkYellow;
                    else
                        ForegroundColor = ConsoleColor.Green;
                    WriteAt(snake.Symbol, snake.TailPos[i][0], snake.TailPos[i][1]);
                }

                // Check for any collisions
                if (snake.XPos == apple.XPos && snake.YPos == apple.YPos)
                {
                    snake.AddTail();
                    apple.RandomizePos(snake);
                }

                if (snake.CheckTailCol())
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteAt("GAME OVER!", SCREEN_X/2 - 2, SCREEN_Y/2);
                    break;
                }

                double delta = (timer.Elapsed.TotalMilliseconds);
                double fps = 1200 - delta;
                fps = (fps / 11) + 2;
                
                timer.Restart();
                Thread.Sleep((int)fps); // Slows game down to a playable speed (~11 fps)
            }

            timer.Stop();
            ResetColor();
            WriteAt("Press enter to exit...", 0, SCREEN_Y - 1);
            ReadLine();
        }

        static void WriteAt(char outChar, int xPos, int yPos)
        {
            xPos = Math.Clamp(xPos, 0, SCREEN_X);
            yPos = Math.Clamp(yPos, 0, SCREEN_Y);

            SetCursorPosition(xPos*2, yPos);
            Write(outChar);
        }

        static void WriteAt(string outString, int xPos, int yPos)
        {
            xPos = Math.Clamp(xPos, 0, SCREEN_X);
            yPos = Math.Clamp(yPos, 0, SCREEN_Y);

            SetCursorPosition(xPos * 2, yPos);
            Write(outString);
        }

        static byte ReadUserInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    return UP;
                case ConsoleKey.DownArrow:
                    return DOWN;
                case ConsoleKey.LeftArrow:
                    return LEFT;
                case ConsoleKey.RightArrow:
                    return RIGHT;
                case ConsoleKey.Escape:
                    return ESCAPE;
                default:
                    return 0;
            }
        }

    }
}
