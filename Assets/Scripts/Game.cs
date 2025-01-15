using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ConsoleApp1.Shaders;
using ConsoleApp1.Viewing;

namespace ConsoleApp1
{
    public class Game(int width, int height, string title, GameWindowSettings gameWindowSettings) : GameWindow(gameWindowSettings, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
    {

        int VertexBufferObject;
        int VertexArrayObject;
        int ElementBufferObject;
        bool firstMove = true;

        Matrix4 ModelMatrix = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(0.0f));

        public readonly Camera camera = new(new(0.0f, 0.0f, 3.0f));

        Texture T1;

        Shader shader;

        Vector2 size = new(8.0f, 10.0f);
        float index = 0;

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            if (firstMove)
            {
                camera.UpdateLastPos(new Vector2(e.X, e.Y));
                firstMove = false;
            }
            else
            {
                Vector2 currentPos = new Vector2(e.X, e.Y);
                camera.UpdateRotation(currentPos);
            }
            camera.Rotate();

        }

        protected override void OnUpdateFrame(FrameEventArgs e) // Update game logic here
        {
            base.OnUpdateFrame(e);

            if (!IsFocused)
            {
                return;
            }

            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            if (input.IsKeyDown(Keys.S))
            {
                camera.Backward((float)e.Time);
            }

            if (input.IsKeyDown(Keys.W))
            {
                camera.Forward((float)e.Time);
            }

            if (input.IsKeyDown(Keys.A))
            {
                camera.Left((float)e.Time);
            }

            if (input.IsKeyDown(Keys.D))
            {
                camera.Right((float)e.Time);
            }

            if (input.IsKeyDown(Keys.Space))
            {
                camera.Up((float)e.Time);
            }

            if (input.IsKeyDown(Keys.LeftShift))
            {
                camera.Down((float)e.Time);
            }
        }

        protected override void OnLoad() // Load graphics here
        {
            base.OnLoad();
            camera.SetProjection(45.0f, CurrentMonitor.HorizontalResolution / CurrentMonitor.VerticalResolution, 0.1f, 100f);

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            shader = new("Assets/Shaders/Default/tile.vert", "Assets/Shaders/Default/tile.frag");
            T1 = new("Assets/Tests/UV_checker_Map_byValle.png", shader.Handle);

            shader.SetMatrix4("model", ModelMatrix);
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
            shader.SetVec2("texSizes", size);
            shader.SetFloat("texIndice", index);

            VertexBufferObject = GL.GenBuffer();
            ElementBufferObject = GL.GenBuffer();
            VertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(VertexArrayObject);

            // Indices
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Testing.Testing._indices.Length * sizeof(uint), Testing.Testing._indices, BufferUsageHint.StaticDraw);

            // Vertices
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Testing.Testing._vertices.Length * sizeof(float), Testing.Testing._vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.Enable(EnableCap.DepthTest);

            CursorState = CursorState.Grabbed;

            // Code goes here
        }

        protected override void OnRenderFrame(FrameEventArgs e) // Render graphics here
        {
            base.OnRenderFrame(e);

            shader.SetMatrix4("model", ModelMatrix);
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
            shader.SetVec2("texSizes", size);
            shader.SetFloat("texIndice", index);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shader.Use();

            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, Testing.Testing._indices.Length, DrawElementsType.UnsignedInt, 0);

            // Code goes here

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e) // Adjust the viewport when the window is resized
        {
            base.OnFramebufferResize(e);

            camera.SetProjection(45.0f, CurrentMonitor.HorizontalResolution / CurrentMonitor.VerticalResolution, 0.1f, 100f);

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
}