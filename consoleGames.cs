using System;
using System.Collections.Generic;
using System.Threading;

class SplitAnalyser
{
    public static bool Terminate = false;
    public static int posLeft = 0; // not in use?
    public static int posTop = 0; // not in use?
    public static char[,] davidImage = new char[,] { {' ','☺',' ','→','╬','←','/',' ','\\'},   // ascii art - mod 3
                                                     {'Y','y','Y','Y','Y','Y','Y','Y','Y'},   // foreground color
                                                     {'x','x','x','x','x','x','x','x','x'} }; // background color
    public static char[,] bobbyImage = new char[,] { {' ','☺',' ','╒','╬','╕','╔','╩','╗'},   // ascii art - mod 3
                                                     {'g','g','g','g','g','g','g','g','g'},   // foreground color
                                                     {'x','x','x','x','x','x','x','x','x'} }; // background color
    public static List<Character> charactersList = new List<Character>();
    public static Background backgroundImage = new Background();

    static void Main(string[] args)
    {       
        Console.Clear(); 
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Title = "lets play games BETA";
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Weird game thing!");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Press Esc to quit.....");
        Thread DisplayThread = new Thread(new ThreadStart(display));
        Thread ConsoleKeyListener = new Thread(new ThreadStart(ListerKeyBoardEvent));
        Thread LogicThread = new Thread(new ThreadStart(logic));
        LogicThread.Name = "LogicThread";
        DisplayThread.Name = "Display";
        ConsoleKeyListener.Name = "KeyListener";

        Thread.Sleep(1000);
        System.Console.WindowWidth = 100;
        System.Console.WindowHeight = 50;

        DisplayThread.Start();
        ConsoleKeyListener.Start();
        LogicThread.Start();


        Character charact = new Character("David", 5, 5, davidImage);
        charactersList.Add(charact);
        charact = new Character("Bobby", 5, 5, bobbyImage);
        charactersList.Add(charact);

        while (true)
        {
            if (Terminate)
            {
                Console.WriteLine("Hope you enjoyed yourself!!");
                DisplayThread.Abort();
                ConsoleKeyListener.Abort();

                Thread.Sleep(2000);
                LogicThread.Abort();
                DisplayThread.Abort();
                Thread.CurrentThread.Abort();
                return;
            }
        }
    }

    public static void ListerKeyBoardEvent()
    {
        while (true)
        {
            switch (Console.ReadKey(false).Key)
            {
                case ConsoleKey.Spacebar:
                    Console.WriteLine("Spacebar");
                    break;
                case ConsoleKey.UpArrow:
                    ++posTop;
                    charactersList[0].y -= 1;
                    break;
                case ConsoleKey.DownArrow:
                    --posTop;
                    charactersList[0].y += 1;
                    break;
                case ConsoleKey.RightArrow:
                    ++posLeft;
                    charactersList[0].x += 1;
                    break;
                case ConsoleKey.LeftArrow:
                    --posLeft;
                    charactersList[0].x -= 1;
                    break;
                case ConsoleKey.D:
                    ++posLeft;
                    break;
                case ConsoleKey.A:
                    --posLeft;
                    break;
                case ConsoleKey.Escape:
                    Terminate = true;
                    break;
            }
            if (charactersList[0].x < 1)
            {
                charactersList[0].x = 1;
            }
            else if (charactersList[0].x > 195)
            {
                charactersList[0].x = 195;
            }
            if (charactersList[0].y < 1)
            {
                charactersList[0].y = 1;
            }
            else if (charactersList[0].y > 46)
            {
                charactersList[0].y = 46;
            }
        }
    }


    public static void logic()
    {
        while (true)
        {
            foreach (Character charac1 in charactersList)
            {
                foreach (Character charac2 in charactersList)
                {
                    if (charac1.x == charac2.x && charac1.y == charac2.y)
                    {
                        
                    }
                } 
            }
        }
    }


    public static void display()
    {
        Console.SetCursorPosition(1, 1);
        int i = 0;
        Console.CursorVisible = false;//Hide cursor
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.Clear();
        while (true)
        {
            //Console.Clear();
            draw();
            Thread.Sleep(65);
            // WriteAt(30, 20, "k");

        }
    }


    private static void draw()
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

class Character
{
    public string name;
    public int x;
    public int y;
    public char[,] image = new char[9, 3]; // "sprite" grid; three layers: zero = shape, one = foreground, two = background
    public Character(string name, int x, int y, char[,] image)
    {
        this.name = name;
        this.x = x;
        this.y = y;
        this.image = image;
    }
}


class Background 
{
    public static char[,] image = new char[,]
    {
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/', },
            {' ','/','≡','≡','≡','≡','≡','≡','\\','_','_','_','|',' ','|',' ','-','/' },
            {'/','|','≡','≡','≡','≡','≡','≡','|','\\','_','_','|',' ','|',' ','-','/' },
            {' ','|','≡','|',' ','|','≡','≡','|','/','/',' ','|',' ','|',' ','-','/' },
            {' ','|','≡','|','•','|','≡','≡','|','/',' ',' ','|',' ','|',' ','-','/' },
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/' },
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/' },
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/' },
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/' }
    };
    public static char[,] foreColor = new char[,]
    {
            {' ',' ','Y','Y','Y','Y','Y','Y','Y','Y','Y','Y','w',' ','w','\\','-','/' },
            {' ','Y','Y','Y','Y','Y','Y','Y','Y','Y','Y','Y','w',' ','w','\\','-','/' },
            {'Y','Y','Y','Y','Y','Y','Y','Y','Y','Y','Y','Y','w',' ','w','\\','-','/' },
            {' ','Y','Y','Y',' ','Y','Y','Y','Y','Y','Y',' ','w',' ','w','\\','-','/' },
            {' ','Y','Y','Y',' ','Y','Y','Y','Y','Y',' ',' ','w',' ','w','\\','-','/' },
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/' },
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/' },
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/' },
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/' }
    };
    public static char[,] backColor = new char[,]
    {
            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','\\','|',' ','|','\\','-','/' },
            {' ','C','C','C','C','C','C','C','C','C','C','C','|',' ','|','\\','-','/' },
            {'C','C','C','C','C','C','C','C','C','C','C','C','|',' ','|','\\','-','/' },
            {' ',' ','C','R','R',' ','C','C','C','C','G','G','|',' ','|','\\','-','/' },
            {' ',' ','C','R','R',' ','C','C','C','G','G','G','|',' ','|','\\','-','/' },
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/' },
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/' },
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/' },
            {' ',' ','_','_','_','_','_','_','_','_','_','_','|',' ','|',' ','-','/' }
    };
}

