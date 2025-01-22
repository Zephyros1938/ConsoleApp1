using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ConsoleApp1.Shaders;
using ConsoleApp1.Viewing;
using ConsoleApp1.World.Tiles;
using ConsoleApp1.World;

namespace ConsoleApp1
{
    public class Game(int width, int height, string title, GameWindowSettings gameWindowSettings) : GameWindow(gameWindowSettings, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
    {
        bool firstMove = true;

        Matrix4 ModelMatrix = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(0.0f));

        public readonly Camera camera = new(new(0.0f, 0.0f, 0.0f));
        ShaderProgram shaderProgram;

        private Thread _WorldThread;

        World.World world = new();

        float[] vertices =
        [
            //front face
            1f,1f,1f,
            -1f,1f,1f,
            1f,1f,-1f,
            -1f,1f,1f,
            -1f,1f,-1f,
            1f,1f,-1f
        ];

        float[] texCoords =
        [
            //front face
            1f,1f,
            0f,1f,
            1f,0f,
            0f,1f,
            0f,0f,
            1f,0f
        ];

        float[] blockData =
        [
            0f,0f,0f,1f,1f,1f
        ];

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            if (firstMove)
            {
                camera.UpdateLastPos(new(e.X, e.Y));
                firstMove = false;
            }
            else
            {
                Vector2 currentPos = new(e.X, e.Y);
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
            world = new();
            world.Generate();
            camera.SetProjection(45.0f, (float)width / height);

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            shaderProgram = new("Assets/Shaders/Default/default.vert", "Assets/Shaders/Default/default.frag");

            shaderProgram.SetMatrix4("model", ModelMatrix);
            shaderProgram.SetMatrix4("view", camera.GetViewMatrix());
            shaderProgram.SetMatrix4("projection", camera.GetProjectionMatrix());
            shaderProgram.AddTexture(new("Assets/Images/textures.png", TextureUnit.Texture0), 0, "solid");
            shaderProgram.AddTexture(new("Assets/Images/specular.png", TextureUnit.Texture1), 1, "solidSpecular");
            shaderProgram.AddTexture(new("Assets/Images/normals.png", TextureUnit.Texture2), 2, "solidNormal");
            shaderProgram.AddTexture(new("Assets/Images/textures_tr.png", TextureUnit.Texture3), 3, "transparent");
            shaderProgram.AddTexture(new("Assets/Images/specular_tr.png", TextureUnit.Texture4), 4, "transparentSpecular");
            shaderProgram.AddTexture(new("Assets/Images/normals_tr.png", TextureUnit.Texture5), 5, "transparentNormal");

            shaderProgram.Bind();

            // Vertices
            shaderProgram.SetArrays(vertices, "vertices");

            // Colors
            shaderProgram.SetArrayBufferF(1, 2, VertexAttribPointerType.Float, false, 2, 0, texCoords, "texCoords");

            // Block Data
            shaderProgram.SetArrayBufferF(2, 1, VertexAttribPointerType.Float, false, 1, 0, blockData, "blockData");

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            //GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);

            CursorState = CursorState.Grabbed;

            // Code goes here
        }

        protected override void OnRenderFrame(FrameEventArgs e) // Render graphics here
        {
            base.OnRenderFrame(e);

            shaderProgram.SetMatrix4("model", ModelMatrix);
            shaderProgram.SetMatrix4("view", camera.GetViewMatrix());
            shaderProgram.SetMatrix4("projection", camera.GetProjectionMatrix());

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shaderProgram.Bind();
            shaderProgram.Use();
            shaderProgram.DrawArrays();

            SwapBuffers();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e) // Adjust the viewport when the window is resized
        {
            base.OnFramebufferResize(e);

            GL.Viewport(this.ClientRectangle);

            camera.SetProjection(45.0f, (float)e.Width / e.Height);

            Console.WriteLine($"New framebuffer size: {e.Width}x{e.Height}");
        }

        protected override void OnUnload() // Clean up resources here
        {
            base.OnUnload();

            shaderProgram.Dispose();

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