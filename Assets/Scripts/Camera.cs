using OpenTK.Mathematics;

namespace ConsoleApp1.Camera
{
    public class Camera
    {
        Vector3 cameraPos = new(0.0f, 0.0f, 3.0f);
        Vector3 cameraTarget = Vector3.Zero;
        Vector3 up = Vector3.UnitY;

        Vector3 cameraDirection;
        Vector3 cameraRight;
        Vector3 cameraUp;

        Matrix4 view = Matrix4.LookAt(
            new Vector3(0.0f, 0.0f, 0.3f),
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f)
        );

        public Camera()
        {
            cameraDirection = Vector3.Normalize(cameraPos - cameraTarget);
            cameraRight = Vector3.Normalize(Vector3.Cross(up, cameraDirection));
            cameraUp = Vector3.Cross(cameraDirection, cameraRight);
        }
    }
}