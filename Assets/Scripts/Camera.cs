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

        Vector3 cameraDirection; // unused
        Vector3 cameraRight; // unused
        Vector3 cameraUp; // unused

        Vector2 lastPos = new Vector2(0.0f, 0.0f); 

        Matrix4 view; 
        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), 100 / 100, 0.1f, 100f);

        /// <summary>
        /// Creates a camera object with a position and projection matrix.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="projection"></param>
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

        /// <summary>
        /// Sets the position of the camera.
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(Vector3 position) => this.position = position;
        /// <summary>
        /// Sets the speed of the camera.
        /// </summary>
        /// <param name="speed"></param>
        public void SetSpeed(float speed) => this.speed = speed;
        /// <summary>
        /// Sets the sensitivity of the camera.
        /// </summary>
        /// <param name="sensitivity"></param>
        public void SetSensitivity(float sensitivity) => this.sensitivity = sensitivity;
        /// <summary>
        /// Sets the projection matrix of the camera.
        /// </summary>
        /// <param name="fov"></param>
        /// <param name="aspect"></param>
        /// <param name="near"></param>
        /// <param name="far"></param>
        public void SetProjection(float fov, float aspect, float near, float far) => projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), aspect, near, far);

        /// <summary>
        /// Moves the camera forward.
        /// </summary>
        /// <param name="delta"></param>/
        public void Forward(float delta) => position += front * speed * delta;
        /// <summary>
        /// Moves the camera backward.
        /// </summary>
        /// <param name="delta"></param>
        public void Backward(float delta) => position -= front * speed * delta;
        /// <summary>
        /// Moves the camera to the left.
        /// </summary>
        /// <param name="delta"></param>
        public void Left(float delta) => position -= Vector3.Normalize(Vector3.Cross(front, up)) * speed * delta;
        /// <summary>
        /// Moves the camera to the right.
        /// </summary>
        /// <param name="delta"></param>
        public void Right(float delta) => position += Vector3.Normalize(Vector3.Cross(front, up)) * speed * delta;
        /// <summary>
        /// Moves the camera up.
        /// </summary>
        /// <param name="delta"></param>
        public void Up(float delta) => position += up * speed * delta;
        /// <summary>
        /// Moves the camera down.
        /// </summary>
        /// <param name="delta"></param>
        public void Down(float delta) => position -= up * speed * delta;

        public void Rotate()
        {
            front.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
            front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
            front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
            front = Vector3.Normalize(front);
        }

        public void UpdateLastPos(Vector2 currentPos) => lastPos = currentPos;
        public void UpdateLastPos(float X, float Y) => lastPos = new Vector2(X, Y);

        /// <summary>
        /// Updates the rotation of the camera.
        /// `currentPos` is the current position of the mouse, this is used to calculate the change in rotation.
        /// </summary>
        /// <param name="currentPos"></param>
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

        /// <summary>
        /// Returns the view matrix of the camera.
        /// </summary>
        /// <returns></returns>
        public Matrix4 GetViewMatrix() { view = Matrix4.LookAt(position, position + front, up); return view; }
        /// <summary>
        /// Returns the projection matrix of the camera.
        /// </summary>
        /// <returns></returns>
        public Matrix4 GetProjectionMatrix() => projection;
        /// <summary>
        /// Returns the position of the camera.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetPosition() => position;
        /// <summary>
        /// Returns the front vector of the camera.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetFront() => front;
    }
}