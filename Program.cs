using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;

namespace ConsoleApp1
{
    public class Game(int width, int height, string title, GameWindowSettings gameWindowSettings) : GameWindow(gameWindowSettings, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
    {

        int VertexBufferObject;
        int VertexArrayObject;

        Shader shader;

        float[] Vertices = {
            -0.5f, -0.5f, 0.0f, // Bottom-left vertex
            0.5f, -0.5f, 0.0f, // Bottom-right vertex
            0.0f,  0.5f, 0.0f  // Top vertex
        };

        protected override void OnUpdateFrame(FrameEventArgs e) // Update game logic here
        {
            base.OnUpdateFrame(e);

            Keys? pressedKey = null;

            // Find the first key that is pressed
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (key.GetHashCode() > 0)
                {
                    if (KeyboardState.IsKeyPressed(key))
                    {
                        pressedKey = key;
                        break;
                    }
                }

            }

            if (pressedKey.HasValue)
            {
                switch (pressedKey.Value)
                {
                    case Keys.Escape:
                        Close();
                        break;

                    case Keys.Backspace:
                        Console.Write("\b \b");
                        break;

                    case Keys.Space:
                        Console.Write(' ');
                        break;

                    case Keys.Enter:
                        Console.Write('\n'); // Equivalent to '\n'
                        break;

                    case Keys.Tab:
                        Console.Write('\t');
                        break;

                    default:
                        // Print the key if it's valid
                        if (pressedKey.Value.GetHashCode() is > 0 and < 512)
                        {
                            Console.Write(pressedKey.Value);
                        }
                        break;
                }
            }
        }

        protected override void OnLoad() // Load graphics here
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            shader = new Shader("Assets/Shaders/Default/default.vert", "Assets/Shaders/Default/default.frag");

            VertexBufferObject = GL.GenBuffer();

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // Code goes here
        }

        protected override void OnRenderFrame(FrameEventArgs e) // Render graphics here
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);


            // Code goes here

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e) // Adjust the viewport when the window is resized
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);

            Console.WriteLine($"New framebuffer size: {e.Width}x{e.Height}");
        }

        protected override void OnUnload() // Clean up resources here
        {
            base.OnUnload();

            shader.Dispose();

            // Code goes here
        }

        Vector2i GetBestResolution(Vector2i monitorResolution)
        {
            // Get all resolutions from the Resolutions class
            var resolutions = typeof(Resolutions)
                .GetFields()
                .Select(static f => (Vector2i)f.GetValue(null))
                .OrderByDescending(r => r.X * r.Y) // Sort by resolution area (width * height)
                .ToList();

            // Find the largest resolution that fits within the monitor dimensions
            foreach (var resolution in resolutions)
            {
                if (resolution.X <= monitorResolution.X && resolution.Y <= monitorResolution.Y)
                {
                    return resolution;
                }
            }

            // If no suitable resolution is found, return the smallest resolution as a fallback
            Console.WriteLine("No suitable resolution found. Using the smallest resolution as a fallback.");
            return resolutions.Last();
        }

        /// <summary>
        /// Set the window size to the best resolution that fits within the monitor dimensions
        /// </summary>
        /// <param name="resolution"></param>
        public void SetBestResolution(Vector2i? resolution = null)
        {
            if (resolution == null)
            {
                resolution = new Vector2i(CurrentMonitor.HorizontalResolution, CurrentMonitor.VerticalResolution);
            }
            Size = GetBestResolution(resolution.Value);
        }

        /// <summary>
        /// Run the game
        /// </summary>
        public new void Run()
        {
            IsVisible = true;
            Console.WriteLine("Running the game...");
            base.Run();
        }
    }

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
                // Set the window border to a fixed size
                game.WindowBorder = WindowBorder.Fixed;

                // Get the monitor resolution and set the window size to the best resolution
                var res = new Vector2i(game.CurrentMonitor.HorizontalResolution, game.CurrentMonitor.VerticalResolution);
                Console.WriteLine($"Monitor Resolution: {res.X}x{res.Y}");
                game.SetBestResolution(res);

                // Center the window on the screen
                game.WindowBorder = WindowBorder.Hidden;
                game.CenterWindow();

                // Run the game
                game.Run();
            }
        }
    }
}