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
        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //This will greet the user
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Speak("Welcome to my C P U helper");

            #region My Performance Counters
            //This will pull the current CPU load in percentage
            PerformanceCounter perfCpuCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

            //This will pull the current available memory in MB
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");
            
            //This will pull the current system uptime in seconds
            PerformanceCounter perfUptimeCount = new PerformanceCounter("System", "System Up Time");
            #endregion

            //Infinite While Loop
            while (true)
            {
                //get current performance counter values
                int currentCpuPercentage = (int) perfCpuCount.NextValue();
                float currentMemAvailable = perfMemCount.NextValue();
                float currentUptime = perfUptimeCount.NextValue();

                //Every 1 second print status
                Console.WriteLine("CPU Load: {0}%", currentCpuPercentage);
                Console.WriteLine("Available Memory: {0}MBs", currentMemAvailable);
                Console.WriteLine("Uptime: {0}secs", currentUptime);
                
                if(currentCpuPercentage > 80)
                {
                    string cpuLoadVocalMessage = String.Format("The current CPU load is {0} percent", currentCpuPercentage);
                    synth.Speak(cpuLoadVocalMessage);
                    
                }
                if(currentMemAvailable < 2048)
                {
                    string memAvailableVocalMessage = String.Format("Currently have {0} megabytes of memory available", currentMemAvailable);
                    synth.Speak(memAvailableVocalMessage);
                }
                
                Thread.Sleep(1000);
                
            }
        }
    }
}
