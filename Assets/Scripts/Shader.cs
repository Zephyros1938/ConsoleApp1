using OpenTK.Graphics.ES30;
using OpenTK.Mathematics;

namespace ConsoleApp1
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
}