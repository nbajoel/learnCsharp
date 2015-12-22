using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

//
//  Application Name: Drunk PC
//  Description: Application that generates erratic mouse and keyboard movements and input and generates system sounds and fake dialogs to confuse the user
//  Topics:
//      1) Threads
//      2) System.Windows.Forms namespace & assembly
//      3) Hidden application


namespace DrunkPC
{
    
    class Program
    {
        //global random generator
        public static Random _random = new Random();

        /// <summary>
        /// entry point for application
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("DrunkPC Prank application");

            //creates all thread that manipulate all of the inputs and outputs to the system
            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));

            //start all of the threads
            drunkMouseThread.Start();
            drunkKeyboardThread.Start();
            drunkSoundThread.Start();
            drunkPopupThread.Start();

            //Wait for user input
            Console.Read();

            //Aborts all threads
            drunkMouseThread.Abort();
            drunkKeyboardThread.Abort();
            drunkSoundThread.Abort();
            drunkPopupThread.Abort();

        }


        #region Thread Functions
        /// <summary>
        /// This thread will randomly affect the mouse movements
        /// </summary>
        public static void DrunkMouseThread()
        {
            Console.WriteLine("DrunkMouseThread started.");
            int moveX = 0;
            int moveY = 0;

            while (true)
            {
                //Generate Random x and y position movements
                moveX = _random.Next(20) - 10;
                moveY = _random.Next(20) - 10;

                //change random cursor position
                Cursor.Position = new System.Drawing.Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);

                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Thread that creates random keyboard inputs
        /// </summary>
        public static void DrunkKeyboardThread()
        {
           
            while (true)
            {
                //Generate a random capital letter
                char key = (char)(_random.Next(25) + 65);


                //50 percent chance to make it a lowercase letter
                if (_random.Next(2) == 0)
                {
                    key = Char.ToLower(key);
                }


                //enters random key
                SendKeys.SendWait(key.ToString());

                Thread.Sleep(500);
            }
        }
        
        /// <summary>
        /// Creates random system sounds 
        /// </summary>
        public static void DrunkSoundThread()
        {
            Console.WriteLine("DrunkSoundThread started.");
            while (true)
            {
                //Console.WriteLine("DrunkSoundThread looped.");
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Spawns random error messages
        /// </summary>
        public static void DrunkPopupThread()
        {
            Console.WriteLine("DrunkPopupThread started.");
            while (true)
            {
                //Console.WriteLine("DrunkPopupThread looped.");
                Thread.Sleep(500);
            }
        }
        #endregion //for thread functions region

    }
}
