using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class Program
    {
        /// <summary>
        /// Main entry point of the application
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    p.PriorityClass = ProcessPriorityClass.High;
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("Could not set system priority to high.");
            }
            using (var game = new Game(Resolutions.Medium_1280x720.X, Resolutions.Medium_1280x720.Y, "ConsoleApp1", GameWindowSettings.Default))
            {
                game.WindowBorder = WindowBorder.Fixed;
                // Center the window on the screen
                game.CenterWindow();

                // Run the game
                game.Run();
            }
        }
    }
}