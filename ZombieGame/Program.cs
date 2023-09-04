using System;
using System.Security.Cryptography;

namespace zombieGame
{
    public class Program
    {
        public static List<List<int>> dangerSpots = new List<List<int>>();
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
            while (i < 10) {
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

                    Write(toWrite, x, y);
                    renderDanger();
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        public static void renderDanger()
        {
            foreach (List<int> coords in dangerSpots)
            {
                int x = coords[0];
                int y = coords[1];
                int size = coords[1];

                DrawSpot(ConsoleColor.Red, size, x, y);
            }
        }
        public static void randomDanger()
        {
            List<int> coords = new List<int>();
            Random rnd = new Random();
            int x = rnd.Next(0, 20);
            int y = rnd.Next(0, 20);
            var color = System.ConsoleColor.Red;
            int size = rnd.Next(1, 4);
            DrawSpot(color, size, x, y);
            coords.Add(x);
            coords.Add(y);
            coords.Add(size);
            dangerSpots.Add(coords);
        }

       public static void DrawSpot(ConsoleColor color, int size, int x, int y)
        {
            Console.BackgroundColor = color; // background color
            Console.ForegroundColor = color; // foreground color
            Console.SetCursorPosition(x, y);

            for (int i = 0; i < size; i++)
            {
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
                }
            }
            catch (Exception)
            {
            }
        }
    }
}