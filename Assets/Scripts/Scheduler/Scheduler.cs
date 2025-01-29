using System;

namespace ConsoleApp1.Scheduler
{
    class Routine
    {
        private static System.Timers.ElapsedEventHandler? func;
        private static int interval;

        public Routine(System.Timers.ElapsedEventHandler func, int interval){
            Routine.func = func;
            Routine.interval = interval;
        }
        
        private static void StartMemoryManagement()
        {
            using (var timer = new System.Timers.Timer(interval)) // Set interval to 2000 ms
            {
                timer.Elapsed += func;
                timer.Start();

                // Keep the thread alive as long as the timer is active
                while (true)
                {
                    Thread.Sleep(100); // Prevent excessive CPU usage
                }
            }
        }
    }
}