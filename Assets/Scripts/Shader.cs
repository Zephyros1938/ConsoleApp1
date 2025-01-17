using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;

namespace ConsoleApp1.Shaders
{
    public class Shader
    {

        public readonly int Handle;
        private bool disposedValue;

        public Shader(string vertexPath, string fragmentPath)
        {
            string VertexShaderSource = FileUtils.LoadFile(vertexPath);
            string FragmentShaderSource = FileUtils.LoadFile(fragmentPath);

            int VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            int FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);

            GL.CompileShader(VertexShader);
            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int vertexSuccess);
            if (vertexSuccess == 0)
            {
                throw new Exception($"Error compiling vertex shader: {GL.GetShaderInfoLog(VertexShader)}");
            }

            GL.CompileShader(FragmentShader);
            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int fragmentSuccess);
            if (fragmentSuccess == 0)
            {
                throw new Exception($"Error compiling fragment shader: {GL.GetShaderInfoLog(FragmentShader)}");
            }

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int linkSuccess);
            if (linkSuccess == 0)
            {
                throw new Exception($"Error linking program: {GL.GetProgramInfoLog(Handle)}");
            }

            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(VertexShader);
            GL.DeleteShader(FragmentShader);
        }

        public void Use() => GL.UseProgram(Handle);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~Shader()
        {
            if (!disposedValue)
            {
                Console.WriteLine($"GPU Resource Leak! Did you forget to call Dispose()?\n\tShader was not properly disposed: {Handle}");
                Dispose(disposing: false);
            }
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        public void SetInt(string Name, int Value)
        {
            int location = GL.GetUniformLocation(Handle, Name);
            GL.UseProgram(Handle);
            //Console.WriteLine($"Set int with name {Name} and value {Value}");
            GL.Uniform1(location, Value);
        }

        public void SetFloat(string Name, float Value)
        {
            int location = GL.GetUniformLocation(Handle, Name);
            //Console.WriteLine($"Set float with name {Name} and value {Value}");
            GL.Uniform1(location, Value);
        }

        public void SetMatrix4(string Name, Matrix4 Value)
        {
            int location = GL.GetUniformLocation(Handle, Name);
            //Console.WriteLine($"Set matrix with name {Name} and value {Value}");
            GL.UniformMatrix4(location, true, ref Value);
        }

        public void SetVec2(string Name, Vector2 Value)
        {
            int location = GL.GetUniformLocation(Handle, Name);
            //Console.WriteLine($"Set vec2 with name {Name} and value {Value}");
            GL.Uniform2(location, Value);
        }

        public void SetVec3(string Name, Vector3 Value)
        {
            int location = GL.GetUniformLocation(Handle, Name);
            //Console.WriteLine($"Set vec3 with name {Name} and value {Value}");
            GL.Uniform3(location, Value);
        }
    }

    public class ShaderProgram(string vertexPath, string fragmentPath)
    {
        public readonly Shader shader = new(vertexPath, fragmentPath);
        readonly int VertexArrayObject = GL.GenVertexArray();
        int indicesLength = 0;

        readonly List<(String name, int ID)> buffers = [];
        readonly List<Texture> textures = [];
        public void SetIndices(uint[] indices, string name = "UNNAMED")
        {
            int ID = GL.GenBuffer();
            buffers.Add((name, ID));
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            indicesLength = indices.Length;
        }

        public void SetArrayBufferF(int index, int size, VertexAttribPointerType type, bool normalized, int stride, int offset, float[] data, string name = "UNNAMED")
        {
            int ID = GL.GenBuffer();
            buffers.Add((name, ID));
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(index, size, type, normalized, stride * sizeof(float), offset);
            GL.EnableVertexAttribArray(index);
        }

        public void SetArrayBufferI(int index, int size, VertexAttribPointerType type, bool normalized, int stride, int offset, int[] data, string name = "UNNAMED")
        {
            int ID = GL.GenBuffer();
            buffers.Add((name, ID));
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(int), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(index, size, type, normalized, stride * sizeof(int), offset);
            GL.EnableVertexAttribArray(index);
        }

        public void SetArrayBufferUI(int index, int size, VertexAttribPointerType type, bool normalized, int stride, int offset, uint[] data, string name = "UNNAMED")
        {
            int ID = GL.GenBuffer();
            buffers.Add((name, ID));
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(uint), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(index, size, type, normalized, stride * sizeof(uint), offset);
            GL.EnableVertexAttribArray(index);
        }

        public void AddTexture(Texture texture, int location, string name)
        {
            textures.Add(texture);
            SetInt(name, location);
        }

        public void Bind()
        {
            GL.BindVertexArray(VertexArrayObject);
        }

        public static void Unbind()
        {
            GL.BindVertexArray(0);
        }

        public void Draw()
        {
            GL.DrawElements(PrimitiveType.Triangles, indicesLength, DrawElementsType.UnsignedInt, 0);
        }

        public void Use()
        {
            shader.Use();
        }

        public void InitTextures()
        {
            foreach (Texture texture in textures)
            {
                //Console.WriteLine($"Using texture {texture.Handle} with unit {texture.unit}");
                texture.Use();
            }
        }

        public int GetAttribLocation(string attribName)
        {
            return shader.GetAttribLocation(attribName);
        }

        public int GetShaderHandle() => shader.Handle;

        public void SetInt(string Name, int Value)
        {
            shader.SetInt(Name, Value);
        }

        public void SetFloat(string Name, float Value)
        {
            shader.SetFloat(Name, Value);
        }

        public void SetMatrix4(string Name, Matrix4 Value)
        {
            shader.SetMatrix4(Name, Value);
        }

        public void SetVec2(string Name, Vector2 Value)
        {
            shader.SetVec2(Name, Value);
        }

        public void SetVec3(string Name, Vector3 Value)
        {
            shader.SetVec3(Name, Value);
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing ShaderProgram");
            Console.WriteLine($"Deleting VAO {VertexArrayObject}");
            GL.DeleteVertexArray(VertexArrayObject);
            foreach ((String name, int ID) in buffers)
            {
                Console.WriteLine($"Deleting Buffer {ID} with name {name}");
                GL.DeleteBuffer(ID);
            }
            foreach (Texture texture in textures)
            {
                Console.WriteLine($"Deleting Texture {texture.Handle} with path {texture.Path}");
                GL.DeleteTexture(texture.Handle);
            }
            Console.WriteLine($"Deleting Shader {shader.Handle}");
            shader.Dispose();
            Console.WriteLine("Disposed ShaderProgram");
        }

    }
}