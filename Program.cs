using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace ConsoleApp1
{
    public class Game(int width, int height, string title, GameWindowSettings gameWindowSettings) : GameWindow(gameWindowSettings, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
    {

        int VertexBufferObject;
        int VertexArrayObject;
        int ElementBufferObject;

        Matrix4 ModelMatrix = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-55.0f));
        Matrix4 ViewMatrix = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
        Matrix4 ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), 100 / 100, 0.1f, 100f);

        Texture T1;

        Shader shader;

        Vector2 size = new(8.0f, 10.0f);
        float index = 10;

        float[] Vertices =
        {
            //Position          Texture coordinates
            0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
            0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        uint[] indices = {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
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
            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), CurrentMonitor.HorizontalResolution / CurrentMonitor.VerticalResolution, 0.1f, 100f);

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            shader = new Shader("Assets/Shaders/Default/default.vert", "Assets/Shaders/Default/default.frag");

            shader.SetMatrix4("model", ModelMatrix);
            shader.SetMatrix4("view", ViewMatrix);
            shader.SetMatrix4("projection", ProjectionMatrix);
            shader.SetVec2("texSizes", size);
            shader.SetFloat("texIndice", index);

            VertexBufferObject = GL.GenBuffer();
            ElementBufferObject = GL.GenBuffer();
            VertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(VertexArrayObject);

            // Indices
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            // Vertices
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            T1 = new Texture("Assets/Images/textures.png", shader.Handle);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.Enable(EnableCap.DepthTest);

            // Code goes here
        }

        protected override void OnRenderFrame(FrameEventArgs e) // Render graphics here
        {
            base.OnRenderFrame(e);

            shader.SetMatrix4("model", ModelMatrix);
            shader.SetMatrix4("view", ViewMatrix);
            shader.SetMatrix4("projection", ProjectionMatrix);
            shader.SetVec2("texSizes", size);
            shader.SetFloat("texIndice", index);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shader.Use();

            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            // Code goes here

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e) // Adjust the viewport when the window is resized
        {
            base.OnFramebufferResize(e);

            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), CurrentMonitor.HorizontalResolution / CurrentMonitor.VerticalResolution, 0.1f, 100f);

            GL.Viewport(0, 0, e.Width, e.Height);

            Console.WriteLine($"New framebuffer size: {e.Width}x{e.Height}");
        }

        protected override void OnUnload() // Clean up resources here
        {
            base.OnUnload();

            shader.Dispose();

            // Code goes here
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
            Size = Resolution.GetBestResolution(resolution.Value);
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