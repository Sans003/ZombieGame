using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Threading;

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

            Console.WriteLine($"Height: {Convert.ToString(Console.WindowHeight)}, Widht: {Convert.ToString(Console.WindowWidth)}");
            for (int i = 0; i < 50; i++)
            {
                randomDanger();
            }

            DrawDangerSpots();
            Player();
        }

        public static void Player()
        {
            const char toWrite = '*';

            int x = Console.WindowWidth/2, y = Console.WindowHeight/2;
            int ox = 0, oy = 0;

            Write(toWrite, x, y);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var command = Console.ReadKey().Key;

                    ox = x; // old x
                    oy = y; // old y

                    switch (command)
                    {
                        case ConsoleKey.DownArrow:
                            if (y < Console.WindowHeight - 1) y++;
                            break;
                        case ConsoleKey.UpArrow:
                            if (y > 0) y--;
                            break;
                        case ConsoleKey.LeftArrow:
                            if (x > 0) x--;
                            break;
                        case ConsoleKey.RightArrow:
                            if (x < Console.WindowWidth - 1) x++;
                            break;
                    }

                    if (IsDangerSpot(ox, oy))
                    {
                        DrawSpot(ConsoleColor.Red, ox, oy);
                    }
                    else
                    {
                        Write(' ', ox, oy);
                    }

                    Write(toWrite, x, y);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        public static bool IsDangerSpot(int x, int y)
        {
            foreach (var spot in dangerSpots)
            {
                if (spot[0] == x && spot[1] == y)
                {
                    return true;
                }
            }
            return false;
        }

        public static void randomDanger()
        {
            int x = rng.Next(0, 120);
            int y = rng.Next(0, 30);
            int size = rng.Next(1, 4);

            saveCoords(x, y);

            for (int i = 0; i <= size; i++)
            {
                Direction direction = (Direction)rng.Next(Enum.GetNames(typeof(Direction)).Length);
                switch (direction)
                {
                    case Direction.Horizontal:
                        x++;
                        break;
                    case Direction.Vertical:
                        y++;
                        break;
                    case Direction.DiagonalRightDown:
                        x++;
                        y++;
                        break;
                    case Direction.DiagonalLeftDown:
                        x--;
                        y++;
                        break;
                    case Direction.DiagonalRightUp:
                        x++;
                        y--;
                        break;
                    case Direction.DiagonalLeftUp:
                        x--;
                        y--;
                        break;
                }
                saveCoords(x, y);
            }
        }

        public static void saveCoords(int x, int y)
        {
            if (x >= 0 && x < Console.WindowWidth && y >= 0 && y < Console.WindowHeight)
            {
                List<int> coords = new List<int>
                {
                    x,
                    y
                };
                dangerSpots.Add(coords);
            }
        }
        public static void DrawSpot(ConsoleColor color, int x, int y)
        {
            if (x >= 0 && x < Console.WindowWidth && y >= 0 && y < Console.WindowHeight)
            {
                Console.BackgroundColor = color;
                Console.ForegroundColor = color;
                Console.SetCursorPosition(x, y);
                Console.Write("█");
                Console.ResetColor();
            }
        }

        public static void Write(char toWrite, int x, int y)
        {
            if (x >= 0 && y >= 0)
            {
                if (IsDangerSpot(x, y))
                {
                    Console.SetCursorPosition(x, y);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(toWrite);

                }
                Console.SetCursorPosition(x, y);
                Console.Write(toWrite);
            }
        }

        public static void DrawDangerSpots()
        {
            foreach (List<int> coords in dangerSpots)
            {
                int x = coords[0];
                int y = coords[1];
                DrawSpot(ConsoleColor.Red, x, y);
            }
        }
    }
}
