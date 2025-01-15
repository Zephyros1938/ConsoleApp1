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
            GL.Uniform1(location, Value);
        }

        public void SetFloat(string Name, float Value)
        {
            int location = GL.GetUniformLocation(Handle, Name);
            GL.Uniform1(location, Value);
        }

        public void SetMatrix4(string Name, Matrix4 Value)
        {
            int location = GL.GetUniformLocation(Handle, Name);
            GL.UniformMatrix4(location, true, ref Value);
        }

        public void SetVec2(string Name, Vector2 Value)
        {
            int location = GL.GetUniformLocation(Handle, Name);
            GL.Uniform2(location, Value);
        }
    }

    public class ShaderProgram(string vertexPath, string fragmentPath)
    {
        public readonly Shader shader = new(vertexPath, fragmentPath);
        readonly int VertexArrayObject = GL.GenVertexArray();
        int indicesLength = 0;

        Texture[]? textures;

        public void SetIndices(uint[] indices)
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, GL.GenBuffer());
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            indicesLength = indices.Length;
        }

        public static void SetArrayBuffer(int index, int size, VertexAttribPointerType type, bool normalized, int stride, int offset, float[] data)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, GL.GenBuffer());
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(index, size, type, normalized, stride * sizeof(float), offset);
            GL.EnableVertexAttribArray(index);
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

    }
}