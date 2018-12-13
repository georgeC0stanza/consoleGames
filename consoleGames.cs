using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class SplitAnalyser
{
    public static bool Terminate = false;
    public static int posLeft = 0;
    public static int posTop = 0;
    public static char[,] davidImage = new char[,] { {'*','*','*','*',' ','*','*','*','*'},   // ascii art
                                                     {'R','G','Y','B','G','Y','Y','G','B'},   // color
                                                     {'A','A','A','A','A','A','A','A','A'} }; // transparancy todo

    public static List<Character> charactersList = new List<Character>();

    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Weird game thing!");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Press Esc to quit.....");
        Thread DisplayThread = new Thread(new ThreadStart(display));
        Thread ConsoleKeyListener = new Thread(new ThreadStart(ListerKeyBoardEvent));
        DisplayThread.Name = "Display";
        ConsoleKeyListener.Name = "KeyListener";
        DisplayThread.Start();
        ConsoleKeyListener.Start();


        Character david = new Character("David", 5, 5, davidImage);

        charactersList.Add(david);

        while (true)
        {
            if (Terminate)
            {
                Console.WriteLine("Hope you enjoyed yourself!!");
                DisplayThread.Abort();
                ConsoleKeyListener.Abort();
                Thread.Sleep(2000);
                Thread.CurrentThread.Abort();
                return;
            }
        }
    }

    public static void ListerKeyBoardEvent()
    {
        do
        {
            while (true)
            {

                var ch = Console.ReadKey(false).Key;
                switch (ch)
                {
                    case ConsoleKey.Spacebar:
                        Console.WriteLine("Spacebar");
                        break;
                    case ConsoleKey.UpArrow:
                        ++posTop;
                        charactersList[0].y -= 4;
                        break;
                    case ConsoleKey.DownArrow:
                        --posTop;
                        charactersList[0].y += 4;
                        break;
                    case ConsoleKey.RightArrow:
                        ++posLeft;
                        charactersList[0].x += 4;
                        break;
                    case ConsoleKey.LeftArrow:
                        --posLeft;
                        charactersList[0].x -= 4;
                        break;
                    case ConsoleKey.D:
                        ++posLeft;
                        break;
                    case ConsoleKey.A:
                        --posLeft;
                        break;
                    case ConsoleKey.Escape:
                        Console.WriteLine("Escape");
                        Terminate = true;
                        break;
                }
            }
        } while (true);
    }

    public static void display()
    {
        Console.SetCursorPosition(1, 1);
        int i = 0;
        Console.CursorVisible = false;//Hide cursor
        while (true)
        {
            draw();
            // WriteAt(30, 20, "k");

        }
    }


    private static void draw()
    {
        foreach (Character each in charactersList)
        {
            Console.SetCursorPosition(each.x, each.y);
            int index = 0;
            for (int rows = 0; rows < 3; rows++)
            {
                for (int cols = 0; cols < 3; cols++)
                {
                    Console.SetCursorPosition(each.x + cols, each.y + rows);


                    switch (each.image[1, index])
                    {
                        case 'B':
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case 'R':
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case 'G':
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case 'Y':
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                    }
                    Console.Write(each.image[0, index++]);
                }
            }
        }
    }

    public static void WriteAt(int l, int t, string s)
    {
        int currentLeft = Console.CursorLeft;
        int currentTop = Console.CursorTop;

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(s);
        Console.SetCursorPosition(posLeft, posTop);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(s);

        Console.SetCursorPosition(currentLeft, currentTop);
    }

}

class Character
{
    public string name;
    public int x;
    public int y;
    public char[,] image = new char[9, 3];
    public Character(String name, int x, int y, char[,] image)
    {
        this.name = name;
        this.x = x;
        this.y = y;
        this.image = image;
    }
}
