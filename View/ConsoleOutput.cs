using System;
using System.Threading;
using static consgam.Model.Data;

namespace consgam.ViewThings
{

    internal static class View
    {

        public static void Display()
        {
            Console.SetCursorPosition(1, 1);
            int i = 0;
            Console.CursorVisible = false;//Hide cursor
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            while (true)
            {
                //Console.Clear();
                Draw();
                Thread.Sleep(65);
                // WriteAt(30, 20, "k");

            }
        }


        private static void Draw()
        {
            // background
            for (int row = 0; row < 9; ++row)
            {
                for (int col = 0; col < 17; ++col)
                {
                    Console.SetCursorPosition(col, row);
                    setConsoleColors(Background.foreColor[row, col], Background.backColor[row, col]);
                    // Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(Background.image[row, col]);
                    setConsoleColors('w', 'L');
                }
            }


            //Console.BackgroundColor = ConsoleColor.Blue;


            // "sprites"
            foreach (Character each in charactersList)
            {
                try
                {
                    Console.SetCursorPosition(each.x, each.y);
                }
                catch (Exception e)
                {

                }
                int index = 0;
                for (int rows = 0; rows < 3; rows++)
                {
                    for (int cols = 0; cols < 3; cols++)
                    {
                        try
                        {
                            Console.SetCursorPosition(each.x + cols, each.y + rows);
                        }
                        catch (Exception e)
                        {

                        }
                        setConsoleColors(each.image[1, index], each.image[2, index]);
                        Console.Write(each.image[0, index++]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
            }
        }

        public static void setConsoleColors(char fore, char back)
        {
            switch (fore)
            {
                case 'R':
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case 'B':
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case 'G':
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case 'Y':
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case 'M':
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 'C':
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case 'g':
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 'y':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 'r':
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 'c':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 'b':
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 'm':
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 'L':
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case 'K':
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 'k':
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 'w':
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 'x':
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

            }
            switch (back)
            {
                case 'R':
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                case 'B':
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                case 'G':
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case 'Y':
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                case 'M':
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 'C':
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                case 'g':
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case 'y':
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                case 'r':
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case 'c':
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;
                case 'b':
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case 'm':
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case 'L':
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case 'K':
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case 'k':
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case 'w':
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case 'x':

                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }
    }
}
