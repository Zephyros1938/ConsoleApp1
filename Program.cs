using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;

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
            using (var game = new Game(Resolutions.Medium_1280x720.X, Resolutions.Medium_1280x720.Y, "Hello World!", GameWindowSettings.Default))
            {

                // Get the monitor resolution and set the window size to the best resolution
                var res = new Vector2i(game.CurrentMonitor.HorizontalResolution, game.CurrentMonitor.VerticalResolution);
                Console.WriteLine($"Monitor Resolution: {res.X}x{res.Y}");

                // Center the window on the screen
                game.CenterWindow();

                // Run the game
                game.Run();
            }
        }
    }
}