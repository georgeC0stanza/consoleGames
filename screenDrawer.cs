using System;
using System.Threading;
using System.IO;
using System.Text;
using Newtonsoft.Json;

/******************************************
 * i want to add features but man this really needs to be cleaned up baaad!
 * //todo change to pascal case because i didn't realize that was a c# thing.
 * i wonder what else i'm not doing the c# way..
 * ****************************************/
namespace ConsoleApp1
{
    class Program
    {
        public const int SIZE_WIDTH = 100;
        public const int SIZE_HEIGHT = 50;
        public static int posLeft = 0;
        public static int posTop = 0;

        private static char[] colorList = new char[] { 'M', 'm', 'R', 'r', 'Y', 'y', 'G', 'g', 'C', 'c', 'B', 'b', 'L', 'K', 'k', 'W' };
        private static char[] shapeList = new char[] { ' ', '░', '▒', '▓', '█' };
        private static bool isSelect = false;
        private static bool isF = false;
        private static bool isV = false;
        private static char selectedFore = 'w';
        private static char selectedBack = 'L';
        private static char selectedShape = ' ';
        private static char[] currentConsoleColors = new char[2];
        private static char[,] consoleCharmap = new char[SIZE_HEIGHT, SIZE_WIDTH];
        private static char[,] consoleForegroundColorCharmap = new char[SIZE_HEIGHT, SIZE_WIDTH];
        private static char[,] consoleBackgroundColorCharmap = new char[SIZE_HEIGHT, SIZE_WIDTH];

        // todo make char colors an enum? load charmaps and save them to disk.
        // put the charmap into a struct and maybe the selected into one aswell.
        // charmap.character
        // charmap.foregroundcolor
        // charmap.backgroundcolor
        // it would change the save and load to be more simpler and succinct

        /** 
         * we need to break this up.. 
         * and man is it ever bad. what's with the terrible logic?
         ******************************************************************************/
        static void Main(string[] args)
        {
            // initialize the game
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "C# console doodler";
            Thread ConsoleKeyListener = new Thread(new ThreadStart(ListenerKeyBoardEvent));
            ConsoleKeyListener.Start();


            // initialize the screen
            for (int row = 0; row < SIZE_HEIGHT; ++row)
            {
                for (int col = 0; col < SIZE_WIDTH; ++col)
                {
                    consoleCharmap[row, col] = ' ';
                    consoleForegroundColorCharmap[row, col] = 'W';
                    consoleBackgroundColorCharmap[row, col] = 'L';
                }
            }

            try
            {
                System.Console.WindowWidth = SIZE_WIDTH + 10; // to allow a buffer zone to reduce flashing
                System.Console.WindowHeight = SIZE_HEIGHT + 10; // to allow a buffer zone to reduce flashing
                                                                //Console.Clear();
            }
            catch
            {
                Console.WriteLine("please resize your window and set the font to something good..");
            }
            Console.CursorVisible = false;//Hide cursor

            // run game
            while (true)
            {

                // main screen - color picking screen
                Console.SetCursorPosition(3, 2);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Press Esc to quit.....\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\tChoose a color + shape!");
                Console.WriteLine("\tPress space to select + enter to start drawing!");



                // choose foreground color
                Console.SetCursorPosition(5, 8);
                Console.WriteLine("Foreground Color:");
                Console.SetCursorPosition(5, 10);

                posLeft += 5;
                posTop += 10;
                while (!isSelect) // run until *spacebar* is depressed
                {
                    Console.SetCursorPosition(5, 10);
                    foreach (char color in colorList)
                    {
                        setConsoleColors(color, 'L');
                        Console.Write('█');
                    }

                    boundsKeeper(5, 4 + colorList.Length, 10, 10, ref posLeft, ref posTop);
                    Console.SetCursorPosition(posLeft, posTop);
                    setConsoleColors('w', 'L');
                    Console.Write('░'); // makeshift cursor

                }

                selectedFore = colorList[posLeft - 5]; // set the color to the one selected
                isSelect = false; // reset *spacebar*
                posTop = posLeft = 0;



                // choose the background color
                Console.SetCursorPosition(5, 15);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Background Color:");
                Console.SetCursorPosition(5, 17);
                posLeft += 5;
                posTop += 17;
                while (!isSelect) // run until *spacebar* is depressed
                {
                    Console.SetCursorPosition(5, 17);
                    foreach (char color in colorList)
                    {
                        setConsoleColors(color, 'L');
                        Console.Write('█');
                    }
                    boundsKeeper(5, 4 + colorList.Length, 17, 17, ref posLeft, ref posTop);
                    Console.SetCursorPosition(posLeft, posTop);
                    setConsoleColors('w', 'L');
                    Console.Write('░'); // makeshift cursor

                }
                selectedBack = colorList[posLeft - 5]; // set the color to the one selected
                isSelect = false; // reset *spacebar*
                posTop = posLeft = 0;



                // choose the drawing character/shape
                Console.SetCursorPosition(5, 20);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("And your shape please:");
                Console.SetCursorPosition(5, 23);
                posLeft += 5;
                posTop += 23;
                while (!isSelect)// run until *spacebar* is depressed
                {
                    Console.SetCursorPosition(5, 23);
                    setConsoleColors(selectedFore, selectedBack);

                    foreach (char shape in shapeList)
                    {
                        Console.Write(shape);
                    }
                    boundsKeeper(5, 4 + shapeList.Length, 23, 23, ref posLeft, ref posTop);
                    Console.SetCursorPosition(posLeft, posTop);
                    setConsoleColors('c', 'L');
                    Console.Write('░'); // makeshift cursor

                }
                selectedShape = shapeList[posLeft - 5]; // set the color to the one selected
                isSelect = false; // reset *spacebar*
                posTop = posLeft = 0;


                // need a goto for when we press return

                Console.Clear();
                Console.SetCursorPosition(0, 0);
                posTop = posLeft = 0;

                setConsoleColors(selectedFore, selectedBack);
                while (!isF)
                {
                    boundsKeeper(0, SIZE_WIDTH - 1, SIZE_HEIGHT - 1, 0, ref posLeft, ref posTop);
                    Console.SetCursorPosition(posLeft, posTop);
                    Console.Write(selectedShape);
                    // Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    //Console.BackgroundColor = ConsoleColor.Black;
                    for (int row = 0; row < SIZE_HEIGHT; ++row)
                    {
                        for (int col = 0; col < SIZE_WIDTH; ++col)
                        {
                            setConsoleColors(consoleForegroundColorCharmap[row, col], consoleBackgroundColorCharmap[row, col]);
                            Console.Write(consoleCharmap[row, col]);
                        }
                        Console.WriteLine(' ');
                    }
                    boundsKeeper(0, SIZE_WIDTH - 1, SIZE_HEIGHT - 1, 0, ref posLeft, ref posTop);
                    Console.SetCursorPosition(posLeft, posTop);
                    Console.Write(selectedShape);
                    if (isSelect)
                    {
                        boundsKeeper(0, SIZE_WIDTH - 1, SIZE_HEIGHT - 1, 0, ref posLeft, ref posTop);
                        consoleCharmap[posTop, posLeft] = selectedShape;
                        consoleForegroundColorCharmap[posTop, posLeft] = selectedFore;
                        consoleBackgroundColorCharmap[posTop, posLeft] = selectedBack;
                        isSelect = false;
                    }
                    //Thread.Sleep(100);
                }
                isF = false;
                saveToDisk("test");
            }
        }

        /** 
         * save a drawing to disk  
         * INPUT: string filename-what the file should be saved as
         ******************************************************************************/
        public static void saveToDisk(string filename)
        {
            string path = @"c:\consoleGames\" + filename + ".txt";

            var jsonConsoleCharmap = JsonConvert.SerializeObject(consoleCharmap);
            var jsonConsoleForegroundColorCharmap = JsonConvert.SerializeObject(consoleForegroundColorCharmap);
            var jsonConsoleBackgroundColorCharmap = JsonConvert.SerializeObject(consoleBackgroundColorCharmap);
            string[] jsonArray = new string[] { jsonConsoleCharmap, jsonConsoleForegroundColorCharmap, jsonConsoleBackgroundColorCharmap };

            string savedText = JsonConvert.SerializeObject(jsonArray);

            File.WriteAllText(path, savedText);
            
        }


        /** 
         * //todo
         * load a saved drawing as a json file at c:\consoleGames\" + filename + ".txt"
         * INPUT: string filename-what the file is saved as
         ******************************************************************************/
        public static void readFromDisk(string filename)
        {
            string path = @"c:\consoleGames\" + filename + ".txt";
            // Open the file to read from.
            string readText = File.ReadAllText(path);
            //todo
        }


        /** 
         * listens to the keyboard and modifies variables accordingly
         ******************************************************************************/
        public static void ListenerKeyBoardEvent()
        {
            while (true)
            {
                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.Spacebar:
                        isSelect = true;
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        --posTop;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        ++posTop;
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        ++posLeft;
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        --posLeft;
                        break;
                    case ConsoleKey.F:
                        isF = true;
                        break;
                    case ConsoleKey.V:
                        isV = true;
                        break;
                }
            }
        }

        /**
        * keeps a given x and y coordinate in the given bounds
        *****************************************************************************/
        static void boundsKeeper(int leftBound, int rightBound, int lowerBound, int upperBound, ref int X, ref int Y)
        {
            if (X < leftBound)
            {
                X = leftBound;
            }
            else if (X > rightBound)
            {
                X = rightBound;
            }
            if (Y < upperBound)
            {
                Y = upperBound;
            }
            else if (Y > lowerBound)
            {
                Y = lowerBound;
            }
        }

        /**
         * sets the console color scheme
         *****************************************************************************/
        public static void setConsoleColors(char fore, char back)
        {
            switch (fore)
            {
                case 'R':
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case 'B':
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
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
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
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
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }
    }
}

