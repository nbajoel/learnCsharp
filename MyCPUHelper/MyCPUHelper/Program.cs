using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;


namespace MyCPUHelper
{
    class Program
    {
        //initializes the speech synthesizer
        private static SpeechSynthesizer synth = new SpeechSynthesizer();

        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //This will greet the user
            JoelSpeak("Welcome to my C P U helper", VoiceGender.Male, 3);

            #region My Performance Counters
            //This will pull the current CPU load in percentage
            PerformanceCounter perfCpuCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            perfCpuCount.NextValue();

            //This will pull the current available memory in MB
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");
            perfMemCount.NextValue();

            //This will pull the current system uptime in seconds
            PerformanceCounter perfUptimeCount = new PerformanceCounter("System", "System Up Time");
            perfUptimeCount.NextValue();

            #endregion

            //gets time span for uptime
            TimeSpan uptimeSpan = TimeSpan.FromSeconds(perfUptimeCount.NextValue());

            string systemUptimeMessage = string.Format("The current system uptime is {0} days {1} hours {2} minutes {3} seconds",
                (int) uptimeSpan.TotalDays,
                (int) uptimeSpan.Hours,
                (int) uptimeSpan.Minutes,
                (int) uptimeSpan.Seconds);

            //print to user the current uptime
            JoelSpeak(systemUptimeMessage, VoiceGender.Female, 5);

            //Infinite While Loop
            while (true)
            {
                //get current performance counter values
                int currentCpuPercentage = (int) perfCpuCount.NextValue();
                float currentMemAvailable = perfMemCount.NextValue();

                //Every 1 second print status
                Console.WriteLine("CPU Load: {0}%", currentCpuPercentage);
                Console.WriteLine("Available Memory: {0}MBs", currentMemAvailable);
                
                if(currentCpuPercentage > 80)
                {
                    string cpuLoadVocalMessage = String.Format("The current CPU load is {0} percent", currentCpuPercentage);
                    JoelSpeak(cpuLoadVocalMessage, VoiceGender.Male);
                    
                }
                if(currentMemAvailable < 2048)
                {
                    string memAvailableVocalMessage = String.Format("Currently have {0} megabytes of memory available", currentMemAvailable);
                    synth.Speak(memAvailableVocalMessage);
                    JoelSpeak(memAvailableVocalMessage, VoiceGender.Female);
                }
                
                Thread.Sleep(1000);
                
            }
        }


        /// <summary>
        /// speaks with a selected voice
        /// </summary>
        /// <param name="message"></param>
        /// <param name="voicegender"></param>
        public static void JoelSpeak(string message, VoiceGender voicegender)
        {
            synth.SelectVoiceByHints(voicegender);
            synth.Speak(message);
        }

        /// <summary>
        /// speaks with a selected voice at a selected rate
        /// </summary>
        /// <param name="message"></param>
        /// <param name="voicegender"></param>
        /// <param name="rate"></param>
        public static void JoelSpeak(string message, VoiceGender voicegender, int rate)
        {
            synth.Rate = rate;
            JoelSpeak(message, voicegender);
        }
    }
}
