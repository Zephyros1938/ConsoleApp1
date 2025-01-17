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
            using (var game = new Game(Resolutions.FullHD_1920x1080.X, Resolutions.FullHD_1920x1080.Y, "Hello World!", GameWindowSettings.Default))
            {
                // Center the window on the screen
                game.CenterWindow();

                // Run the game
                game.Run();
            }
        }
    }
}