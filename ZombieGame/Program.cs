using System;
using System.Security.Cryptography;

namespace zombieGame
{
    public class Program
    {
        public static List<List<int>> dangerSpots = new List<List<int>>();
        static Random rng = new Random();
        public enum Direction
        {
            Horizontal,
            Vertical,
            DiagonalRightDown,
            DiagonalLeftDown,
            DiagonalRightUp,
            DiagonalLeftUp
        }
        public static void Main(string[] args)
        {
            int i = 0;
            while (i < 15)
            {
                randomDanger();
                i++;
            }

            while (true)
            {
                Player();
            }
        }

        public static void Player()
        {

            const char toWrite = '*';

            int x = 0, y = 0;

            Write(toWrite);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var command = Console.ReadKey().Key;

                    switch (command)
                    {
                        case ConsoleKey.DownArrow:
                            y++;
                            break;
                        case ConsoleKey.UpArrow:
                            if (y > 0)
                            {
                                y--;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (x > 0)
                            {
                                x--;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            x++;
                            break;
                    }

                    rendering(toWrite, x, y);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        public static void rendering(char toWrite, int xx, int yy)
        {
            Write(toWrite, xx, yy);
        }
        public static void randomDanger()
        {
            List<int> coords = new List<int>();
            Random rnd = new Random();
            int x = rnd.Next(0, 20);
            int y = rnd.Next(0, 20);
            var color = System.ConsoleColor.Red;
            int size = rnd.Next(1, 4);
            coords.Add(x);
            coords.Add(y);
            saveCoords(x, y);
            for (int i = 0; i <= size; i++)
            {

                Direction direction = (Direction)rng.Next(Enum.GetNames(typeof(Direction)).Length);
                switch (direction)
                {
                    case Direction.Horizontal:
                        x++; // move right
                        saveCoords(x, y);
                        break;
                    case Direction.Vertical:
                        y++; // move down
                        saveCoords(x, y);
                        break;
                    case Direction.DiagonalRightDown:
                        x++; // move right
                        y++; // move down
                        saveCoords(x, y);
                        break;
                    case Direction.DiagonalLeftDown:
                        x--; // move left
                        y++; // move down
                        saveCoords(x, y);
                        break;
                    case Direction.DiagonalRightUp:
                        x++; // move right
                        y--; // move up
                        saveCoords(x, y);
                        break;
                    case Direction.DiagonalLeftUp:
                        x--; // move left
                        y--; // move up
                        saveCoords(x, y);
                        break;
                }

            }
        }


        public static void saveCoords(int x, int y)
        {
            List<int> coords = new List<int>();
            coords.Add(x);
            coords.Add(y);
            dangerSpots.Add(coords);

        }
        public static void DrawSpot(ConsoleColor color, int x, int y, int m)
        {
            Console.BackgroundColor = color; // background color
            Console.ForegroundColor = color; // foreground color
            Console.SetCursorPosition(x, y);


            for (int i = 0; i < m; i++)
            {

                Console.SetCursorPosition(x, y);
                Console.Write("█");

            }
            Console.ResetColor(); // Reset color
        }

        public static void Write(char toWrite, int x = 0, int y = 0)
        {

            try
            {
                if (x >= 0 && y >= 0)
                {
                    Console.Clear();
                    Console.SetCursorPosition(x, y);
                    Console.Write(toWrite);
                    foreach (List<int> coords in dangerSpots)
                    {
                        int xx = coords[0];
                        int yy = coords[1];

                        DrawSpot(ConsoleColor.Red, xx, yy, dangerSpots.Count());
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}