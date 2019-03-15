using System;

using System.Threading;

using consgam.Model;


namespace consgam
{
    class Controller
    {
        public const int SIZE_WIDTH = 100;
        public const int SIZE_HEIGHT = 50;

        public static bool Terminate = false;
        public static int posLeft = 0; // not in use?
        public static int posTop = 0; // not in use?


        static void Main(string[] args)
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Title = "lets play games BETA";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Weird game thing!");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press Esc to quit.....");


            Console.SetCursorPosition(10, 10);
            Console.WriteLine("Press d to draw or p to play!");
            if (Console.Read() == 'p')
            {
                ConsoleGameStart();
            }
            else  
            {
                ScreenDrawer.ScreenDrawerApp.ScreenDrawMain();
            }

        }

        private static void ConsoleGameStart()
        {
            Thread DisplayThread = new Thread(new ThreadStart(ViewThings.View.Display));
            Thread AIThread = new Thread(new ThreadStart(AI));
            Thread ConsoleKeyListener = new Thread(new ThreadStart(ListerKeyBoardEvent));
            Thread LogicThread = new Thread(new ThreadStart(logic));
            LogicThread.Name = "LogicThread";
            DisplayThread.Name = "Display";
            ConsoleKeyListener.Name = "KeyListener";

            //Thread.Sleep(1000);
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
            DisplayThread.Start();
            ConsoleKeyListener.Start();
            LogicThread.Start();



            Model.Data.Character charact = new Model.Data.Character("David", 5, 5, Model.Data.davidImage);
            Model.Data.charactersList.Add(charact);
            charact = new Model.Data.Character("Bobby", 5, 5, Model.Data.bobbyImage);
            Model.Data.charactersList.Add(charact);
            AIThread.Start();
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

        private static void AI()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 100);

            while (true)
            {
                Thread.Sleep(200);
                Model.Data.charactersList[1].y += random.Next(-1, 1);
                Thread.Sleep(200);
                Model.Data.charactersList[1].x += random.Next(-1, 1);
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
                        Model.Data.charactersList[0].y -= 1;
                        break;
                    case ConsoleKey.DownArrow:
                        --posTop;
                        Model.Data.charactersList[0].y += 1;
                        break;
                    case ConsoleKey.RightArrow:
                        ++posLeft;
                        Model.Data.charactersList[0].x += 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        --posLeft;
                        Model.Data.charactersList[0].x -= 1;
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
                if (Data.charactersList[0].x < 1)
                {
                    Data.charactersList[0].x = 1;
                }
                else if (Data.charactersList[0].x > 195)
                {
                    Data.charactersList[0].x = 195;
                }
                if (Data.charactersList[0].y < 1)
                {
                    Data.charactersList[0].y = 1;
                }
                else if (Data.charactersList[0].y > 46)
                {
                    Data.charactersList[0].y = 46;
                }
            }
        }


        public static void logic()
        {
            while (true)
            {
                foreach (Data.Character charac1 in Data.charactersList)
                {
                    foreach (Data.Character charac2 in Data.charactersList)
                    {
                        if (charac1.x == charac2.x && charac1.y == charac2.y)
                        {

                        }
                    }
                }
            }
        }



    }
}
