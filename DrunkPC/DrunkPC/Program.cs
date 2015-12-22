using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

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

        public static int _startupDelaySeconds = 10;
        public static int _totalDurationSeconds = 10;

        /// <summary>
        /// entry point for application
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("DrunkPC Prank application");

            if (args.Length >= 2)
            {
                _startupDelaySeconds = Convert.ToInt32(args[0]);
                _totalDurationSeconds = Convert.ToInt32(args[1]);
            }

            //creates all thread that manipulate all of the inputs and outputs to the system
            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));


            DateTime future = DateTime.Now.AddSeconds(_startupDelaySeconds);

            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }


            //start all of the threads
            drunkMouseThread.Start();
            drunkKeyboardThread.Start();
            drunkSoundThread.Start();
            drunkPopupThread.Start();

            future = DateTime.Now.AddSeconds(_totalDurationSeconds);

            while(future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

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
            int moveX = 0;
            int moveY = 0;

            while (true)
            {
                //Generate Random x and y position movements
                moveX = _random.Next(20) - 10;
                moveY = _random.Next(20) - 10;

                //change random cursor position
                Cursor.Position = new System.Drawing.Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);

                Thread.Sleep(_random.Next(100));
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

                Thread.Sleep(_random.Next(500));
            }
        }
        
        /// <summary>
        /// Creates random system sounds 
        /// </summary>
        public static void DrunkSoundThread()
        {
            while (true)
            {
                //10% of the time play a sound
                if(_random.Next(100) > 90)
                {
                    //gets a randome number to play a random system sound
                    switch(_random.Next(5))
                    {
                        case 0:
                            {
                                SystemSounds.Asterisk.Play();
                                break;
                            }
                        case 1:
                            {
                                SystemSounds.Beep.Play();
                                break;
                            }
                        case 2:
                            {
                                SystemSounds.Exclamation.Play();
                                break;
                            }
                        case 3:
                            {
                                SystemSounds.Hand.Play();
                                break;
                            }
                        case 4:
                            {
                                SystemSounds.Question.Play();
                                break;
                            }
                    }
                }
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Spawns random error messages
        /// </summary>
        public static void DrunkPopupThread()
        {

            while (true)
            {
                //10% of the time display a messagebox
                if (_random.Next(100) > 90)
                {
                    MessageBox.Show("Internet explorer has stopped working",
                    "Internet Explorer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
                    
                Thread.Sleep(10000);
            }
        }
        #endregion //for thread functions region

    }
}
