using OpenTK.Mathematics;

namespace ConsoleApp1.Viewing
{
    public class Camera
    {
        float speed = 1.5f;
        float sensitivity = 0.1f;

        public float pitch;
        public float yaw;

        Vector3 cameraTarget = Vector3.Zero;

        Vector3 position = new(0.0f, 0.0f, 3.0f);
        Vector3 front = new(0.0f, 0.0f, -1.0f);
        Vector3 up = Vector3.UnitY;

        Vector3 cameraDirection;
        Vector3 cameraRight;
        Vector3 cameraUp;

        Vector2 lastPos = new Vector2(0.0f, 0.0f);

        Matrix4 view;
        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), 100 / 100, 0.1f, 100f);

        public Camera(Vector3? position = null, Matrix4? projection = null)
        {
            if (position.HasValue)
            {
                this.position = position.Value;
            }
            if (projection.HasValue)
            {
                this.projection = projection.Value;
            }
            cameraDirection = Vector3.Normalize(this.position - cameraTarget);
            cameraRight = Vector3.Normalize(Vector3.Cross(up, cameraDirection));
            cameraUp = Vector3.Cross(cameraDirection, cameraRight);
            view = Matrix4.LookAt(this.position, this.position + front, up);
        }

        public void SetPosition(Vector3 position) => this.position = position;
        public void SetSpeed(float speed) => this.speed = speed;
        public void SetProjection(float fov, float aspect, float near, float far) => projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), aspect, near, far);

        public void Forward(float delta) => position += front * speed * delta;
        public void Backward(float delta) => position -= front * speed * delta;
        public void Left(float delta) => position -= Vector3.Normalize(Vector3.Cross(front, up)) * speed * delta;
        public void Right(float delta) => position += Vector3.Normalize(Vector3.Cross(front, up)) * speed * delta;
        public void Up(float delta) => position += up * speed * delta;
        public void Down(float delta) => position -= up * speed * delta;

        public void Rotate()
        {
            front.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
            front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
            front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
            front = Vector3.Normalize(front);
        }

        public void UpdateLastPos(Vector2 currentPos) => lastPos = currentPos;

        public void UpdateRotation(Vector2 currentPos)
        {
            float deltaX = currentPos.X - lastPos.X;
            float deltaY = currentPos.Y - lastPos.Y;
            this.lastPos = currentPos;

            this.yaw += deltaX * this.sensitivity;
            if (pitch > 89.0f)
            {
                pitch = 89.0f;
            }
            else if (pitch < -89.0f)
            {
                pitch = -89.0f;
            }
            else
            {
                pitch -= deltaY * this.sensitivity;
            }
        }

        public Matrix4 GetViewMatrix() { view = Matrix4.LookAt(position, position + front, up); return view; }
        public Matrix4 GetProjectionMatrix() => projection;
        public Vector3 GetPosition() => position;
        public Vector3 GetFront() => front;
        public Vector2 GetLastPos() => lastPos;
    }
}