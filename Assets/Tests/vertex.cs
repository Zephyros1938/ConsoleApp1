namespace ConsoleApp1.Testing
{
    public class Testing
    {
        public static float[] _vertices =
        {
            // Front 0
            0.5f, 0.5f, 0.5f, 1.0f, 1.0f,
            0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
            -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
            -0.5f, 0.5f, 0.5f, 0.0f, 1.0f,

            // Back 4
            0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
            0.5f, -0.5f, -0.5f, 1.0f, 0.0f,
            -0.5f, -0.5f, -0.5f, 0.0f, 0.0f,
            -0.5f, 0.5f, -0.5f, 0.0f, 1.0f,

            // Right 8
            0.5f, 0.5f, 0.5f, 0.0f, 1.0f,
            0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
            0.5f, -0.5f, -0.5f, 1.0f, 0.0f,
            0.5f, -0.5f, 0.5f, 0.0f, 0.0f,

            // Left 12
            -0.5f, 0.5f, 0.5f, 0.0f, 1.0f,
            -0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, 1.0f, 0.0f,
            -0.5f, -0.5f, 0.5f, 0.0f, 0.0f,

            // Top 16
            0.5f, 0.5f, 0.5f, 0.0f, 0.0f,
            0.5f, 0.5f, -0.5f, 0.0f, 1.0f,
            -0.5f, 0.5f, -0.5f, 1.0f, 1.0f,
            -0.5f, 0.5f, 0.5f, 1.0f, 0.0f,

            // Bottom 20
            0.5f, -0.5f, 0.5f, 0.0f, 0.0f,
            0.5f, -0.5f, -0.5f, 0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, 1.0f, 1.0f,
            -0.5f, -0.5f, 0.5f, 1.0f, 0.0f,
        };

        public static uint[] _indices =
        {
            0, 1, 3, // front face
            1, 2, 3,
            4,5,7, // back face
            5,6,7,
            8,9,11, // right face
            9,10,11,
            12,13,15, // left face
            13,14,15,
            16, 17, 19, // top face
            17, 18, 19,
            20, 21, 23, // bottom face
            21, 22, 23
        };
    }
}