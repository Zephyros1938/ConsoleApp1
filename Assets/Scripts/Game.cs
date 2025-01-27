using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ConsoleApp1.Shaders;
using ConsoleApp1.Viewing;
using ConsoleApp1.World;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class Game(int width, int height, string title, GameWindowSettings gameWindowSettings) : GameWindow(gameWindowSettings, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
    {
        bool firstMove = true;
        bool cameraControl = true;

        Matrix4 ModelMatrix = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(0.0f));

        public readonly Camera camera = new(new(0.0f, 0.0f, 0.0f));
        List<ShaderProgram> ShaderProgramList = [];
        private static readonly Thread _WorldThreadGeneration = new(new ThreadStart(WorldGeneration));
        private static readonly Thread _WorldThreadSaving = new(new ThreadStart(WorldSaving));
        private static readonly Thread _WorldThreadBlockProcessing = new(new ThreadStart(WorldLoadBlockData));

        private static readonly World.World world = new("test.wld");
        private static (float[], float[]) blockData = new();

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            if (cameraControl)
            {
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

            if (input.IsKeyPressed(Keys.T))
            {
                cameraControl = !cameraControl;
                CursorState = cameraControl == true ? CursorState = CursorState.Grabbed : CursorState = CursorState.Normal;
            }

            if (input.IsKeyPressed(Keys.R))
            {
                ShaderProgramList[0].ToggleDebug();
            }

            if (input.IsKeyPressed(Keys.Q))
            {
                ShaderProgramList[0].SetDrawMode(PrimitiveType.Lines);
            }

            if (input.IsKeyPressed(Keys.E))
            {
                ShaderProgramList[0].SetDrawMode(PrimitiveType.Triangles);
            }
        }

        static void WorldGeneration()
        {
            Console.WriteLine("Starting World Generation...");
            Vector2i worldDimensions = (world.worldSize.X / 2, world.worldSize.Y / 2);

            int minY = -worldDimensions.Y;
            int maxY = worldDimensions.Y;
            int minX = -worldDimensions.X;
            int maxX = worldDimensions.X;

            Parallel.For(minY, maxY, y =>
            {
                Console.WriteLine($"Generating Y-Level {y}...");
                for (int x = minX; x < maxX; x++)
                {
                    for (int z = minX; z < maxX; z++)
                    {
                        world.Generate((x, y, z));
                    }
                }
                Console.WriteLine($"Finished Generating Y-Level {y}");
            });


            Console.WriteLine($"World ChunkList Length: {world.ChunkList.Count}");
        }

        static void WorldSaving()
        {
            Console.WriteLine("Saving World...");
            foreach (var chunk in world.ChunkList)
            {
                world.SaveChunkToFile(chunk);
            }
        }


        static void WorldLoadBlockData()
        {
            Console.WriteLine("Loading Block Data...");
            Stopwatch s = Stopwatch.StartNew();

            var (Vertices, TexCoords) = BlockUtilities.GenerateBlockData(world.GetChunk(0).GetBlockVertices());
            List<float> allData1 = new(Vertices);
            List<float> allData2 = new(TexCoords);

            for (int i = 1; i < world.ChunkList.Count; i++) // Start at chunk 2.
            {
                (float[], float[]) data = BlockUtilities.GenerateBlockData(world.GetChunk(i).GetBlockVertices());
                allData1.AddRange(data.Item1);
                allData2.AddRange(data.Item2);
            }

            blockData = (allData1.ToArray(), allData2.ToArray());

            s.Stop();
            Console.WriteLine($"Took {s.ElapsedMilliseconds / 1000.0} seconds to calculate block vertices");
        }

        protected override void OnLoad() // Load graphics here
        {
            base.OnLoad();

            Title += ": OpenGL Version: " + GL.GetString(StringName.Version);

            _WorldThreadGeneration.Start();
            _WorldThreadGeneration.Join();
            _WorldThreadSaving.Start();
            _WorldThreadBlockProcessing.Start();
            _WorldThreadBlockProcessing.Join();

            camera.SetProjection(45.0f, (float)width / height);

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            ShaderProgram shaderProgram = new("Assets/Shaders/Default/default.vert", "Assets/Shaders/Default/default.frag"/*, "Assets/Shaders/DefaultGeometry/default.geom"*/);

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
            shaderProgram.SetArrays(blockData.Item1, "vertices");

            // Colors
            shaderProgram.SetArrayBufferF(1, 2, VertexAttribPointerType.Float, false, 2, 0, blockData.Item2, "texCoords");
            blockData = default;

            // Block Data
            shaderProgram.SetArrayBufferF(2, 1, VertexAttribPointerType.Float, false, 1, 0, Testing.Testing2.blockData, "blockData");

            ShaderProgram.Unbind();

            ShaderProgramList.Add(shaderProgram);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);

            CursorState = CursorState.Grabbed;

            // Code goes here
        }

        protected override void OnRenderFrame(FrameEventArgs e) // Render graphics here
        {
            base.OnRenderFrame(e);

            //shaderProgram.SetMatrix4("model", ModelMatrix);
            ShaderProgramList[0].SetMatrix4("view", camera.GetViewMatrix());
            ShaderProgramList[0].SetMatrix4("projection", camera.GetProjectionMatrix());

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (ShaderProgram s in ShaderProgramList)
            {
                s.Bind();
                s.Use();
                s.DrawArrays();
            }

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

            foreach (ShaderProgram s in ShaderProgramList)
            {
                s.Dispose();
            }
            _WorldThreadGeneration.Join();
            _WorldThreadSaving.Join();
            _WorldThreadBlockProcessing.Join();

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