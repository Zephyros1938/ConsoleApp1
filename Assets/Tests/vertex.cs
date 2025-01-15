namespace ConsoleApp1.Testing
{
    public class Testing
    {
        public static float[] _vertices =
        {
            // Front
            0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
            0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
            -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
            -0.5f, 0.5f, 0.5f, 0.0f, 1.0f,

            // Back
            0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
            0.5f, -0.5f, -0.5f, 1.0f, 0.0f,
            -0.5f, -0.5f, -0.5f, 0.0f, 0.0f,
            -0.5f, 0.5f, -0.5f, 0.0f, 1.0f,

            // Right
            0.5f, 0.5f, -0.5f, 1.0f, 0.0f,
            0.5f, -0.5f, -0.5f, 0.0f, 0.0f,
            0.5f, -0.5f, 0.5f, 0.0f, 1.0f,

            // Left
            -0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
            -0.5f, 0.5f, -0.5f, 1.0f, 0.0f,
            -0.5f, -0.5f, 0.5f, 0.0f, 1.0f,

            // Top
            -0.5f, 0.5f, -0.5f, 0.0f, 0.0f,

            // Bottom
            0.5f, -0.5f, 0.5f, 1.0f, 1.0f,
        };

        public static uint[] _indices =
        {
            0, 1, 3, // front face
            1, 2, 3,
            4, 5, 7, // back face
            5, 6, 7,
            0, 8, 10, // right face
            8, 9, 10,
            11, 12, 13, // left face
            12, 6, 13,
            0, 8, 3, // top face
            8, 14, 3,
            15, 5, 13, // bottom face
            5, 6, 13
        };
    }
}