using OpenTK.Mathematics;

namespace ConsoleApp1.World
{
    public class BlockConstants
    {


        public const float CubeSize = 0.5f;

        // Cube vertex offsets
        public static readonly Vector3[] Offsets = new[]
        {
            new Vector3( 1.0f,  1.0f,  1.0f),
            new Vector3( 1.0f,  1.0f, -1.0f),
            new Vector3( 1.0f, -1.0f,  1.0f),
            new Vector3( 1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f,  1.0f,  1.0f),
            new Vector3(-1.0f,  1.0f, -1.0f),
            new Vector3(-1.0f, -1.0f,  1.0f),
            new Vector3(-1.0f, -1.0f, -1.0f),
        };

        // Texture coordinates
        public static readonly Vector2[] TexCoords = new[]
        {
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
        };

        // Face offset indices and texture indices
        public static readonly (int[] OffsetIndices, int[] TexCoordIndices)[] Faces = new[]
        {
            // Top face
            (new[] { 0, 1, 4 }, new[] { 0, 1, 3 }),
            (new[] { 1, 5, 4 }, new[] { 1, 2, 3 }),

            // Bottom face
            (new[] { 2, 6, 3 }, new[] { 0, 3, 1 }),
            (new[] { 6, 7, 3 }, new[] { 3, 2, 1 }),

            // Front face
            (new[] { 0, 2, 1 }, new[] { 0, 1, 3 }),
            (new[] { 2, 3, 1 }, new[] { 1, 2, 3 }),

            // Back face
            (new[] { 4, 5, 6 }, new[] { 0, 3, 1 }),
            (new[] { 5, 7, 6 }, new[] { 3, 2, 1 }),

            // Right face
            (new[] { 0, 4, 2 }, new[] { 0, 1, 3 }),
            (new[] { 4, 6, 2 }, new[] { 1, 2, 3 }),

            // Left face
            (new[] { 1, 3, 5 }, new[] { 0, 3, 1 }),
            (new[] { 3, 7, 5 }, new[] { 3, 2, 1 }),
        };
    }
    public class BlockUtilities
    {
        public static (float[] Vertices, float[] TexCoords) GenerateBlockData((float, float, float, uint)[] blocks)
        {
            // Precompute sizes to avoid resizing arrays
            int vertexCount = blocks.Length * BlockConstants.Faces.Length * 3; // 3 vertices per face triangle
            int texCoordCount = vertexCount; // 2 floats per tex coord (x, y)

            // Allocate arrays
            float[] vertices = new float[vertexCount * 3]; // 3 floats per vertex (x, y, z)
            float[] texCoords = new float[texCoordCount * 2]; // 2 floats per texture coord (u, v)

            int vertexIndex = 0;
            int texCoordIndex = 0;

            foreach (var block in blocks)
            {
                foreach (var (offsetIndices, texCoordIndices) in BlockConstants.Faces)
                {
                    if (offsetIndices.Length != 3 || texCoordIndices.Length != 3)
                    {
                        throw new InvalidOperationException("Each face must have exactly 3 offset indices and 3 texture coordinate indices.");
                    }

                    for (int i = 0; i < 3; i++) // Process each vertex of the triangle
                    {
                        // Validate offset indices
                        if (offsetIndices[i] < 0 || offsetIndices[i] >= BlockConstants.Offsets.Length)
                        {
                            throw new IndexOutOfRangeException($"Offset index {offsetIndices[i]} is out of range.");
                        }

                        // Add vertex coordinates
                        Vector3 vertex = (block.Item1, block.Item2, block.Item3) + BlockConstants.Offsets[offsetIndices[i]] * BlockConstants.CubeSize;
                        vertices[vertexIndex++] = vertex.X;
                        vertices[vertexIndex++] = vertex.Y;
                        vertices[vertexIndex++] = vertex.Z;

                        // Validate texture coordinate indices
                        if (texCoordIndices[i] < 0 || texCoordIndices[i] >= BlockConstants.TexCoords.Length)
                        {
                            throw new IndexOutOfRangeException($"Texture coordinate index {texCoordIndices[i]} is out of range.");
                        }

                        // Add texture coordinates
                        Vector2 texCoord = BlockConstants.TexCoords[texCoordIndices[i]];
                        texCoords[texCoordIndex++] = texCoord.X;
                        texCoords[texCoordIndex++] = texCoord.Y;
                    }
                }
            }

            return (vertices, texCoords);
        }

    }
}