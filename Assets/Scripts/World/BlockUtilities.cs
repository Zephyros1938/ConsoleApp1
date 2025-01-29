using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
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

        public static readonly Vector3i[] directions = new Vector3i[]
        {
            new Vector3i(0, 0, 1),
            new Vector3i(0, 0, -1),
            new Vector3i(0, 1, 0),
            new Vector3i(0, -1, 0),
            new Vector3i(1, 0, 0),
            new Vector3i(-1, 0, 0),
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
        public static (float[] Vertices, float[] TexCoords, int[] TileIDs) GenerateBlockData((float, float, float, int)[] blocks)
        {
            // Precompute sizes to avoid resizing arrays
            int vertexCount = blocks.Length * BlockConstants.Faces.Length * 3; // 3 vertices per face triangle
            int texCoordCount = vertexCount; // 2 floats per tex coord (x, y)
            int tileIDCount = vertexCount;

            // Allocate arrays
            float[] vertices = new float[vertexCount * 3]; // 3 floats per vertex (x, y, z)
            float[] texCoords = new float[texCoordCount * 2]; // 2 floats per texture coord (u, v)
            int[] tileIDs = new int[tileIDCount];

            int vertexIndex = 0;
            int texCoordIndex = 0;
            int tileIDIndex = 0;

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

                        tileIDs[tileIDIndex++] = block.Item4;
                    }
                }
            }

            return (vertices, texCoords, tileIDs);
        }

        [MethodImpl(512)]
        public static (float[] Vertices, float[] TexCoords, int[] TileIDs) GenerateBlockDataChunked(World.Chunk[] chunks)
        {
            // Use dynamic lists to collect data
            List<float> vertices = new List<float>();
            List<float> texCoords = new List<float>();
            List<int> tileIDs = new List<int>();

            foreach (World.Chunk chunk in chunks)
            {
                for (int y = 0; y < World.chunkSize.Y; y++)
                {
                    for (int x = 0; x < World.chunkSize.X; x++)
                    {
                        for (int z = 0; z < World.chunkSize.Z; z++)
                        {
                            World.Block b = chunk.BlockData[x + World.chunkSize.X * (y + World.chunkSize.Y * z)];
                            int tileID = b.ID;
                            if (tileID == 512)
                                continue;
                            Vector3 blockPosition = new Vector3(b.X, b.Y, b.Z);

                            // Check visibility for each face
                            for (int faceIndex = 0; faceIndex < BlockConstants.Faces.Length; faceIndex++)
                            {
                                var (offsetIndices, texCoordIndices) = BlockConstants.Faces[faceIndex];

                                // Skip face if an adjacent block exists
                                Vector3i direction = BlockConstants.directions[faceIndex / 2];
                                //Console.WriteLine($"Direction: {direction}");


                                if (chunk.HasBlockAt((x, y, z) + direction))
                                    continue;

                                // Add face vertices and texture coordinates
                                for (int i = 0; i < 3; i++)
                                {

                                    // Add vertex coordinates
                                    Vector3 vertex = blockPosition + BlockConstants.Offsets[offsetIndices[i]] * BlockConstants.CubeSize;
                                    vertices.Add(vertex.X);
                                    vertices.Add(vertex.Y);
                                    vertices.Add(vertex.Z);

                                    // Add texture coordinates
                                    Vector2 texCoord = BlockConstants.TexCoords[texCoordIndices[i]];
                                    texCoords.Add(texCoord.X);
                                    texCoords.Add(texCoord.Y);

                                    // Add tile ID (for this face)
                                    tileIDs.Add(tileID);
                                }
                            }
                        }
                    }
                }
            }

            // Convert lists to arrays
            return (vertices.ToArray(), texCoords.ToArray(), tileIDs.ToArray());
        }

        [MethodImpl(512)]
        public static void GenerateBlockDataChunkedAdditive(World w, (float[] Vertices, float[] TexCoords, int[] TileIDs) dataList)
        {
            w.ChunkListChanged += (o, e) =>
            {
                dataList = new();
                foreach (World.Chunk chunk in w.ChunkList)
                {
                    for (int y = 0; y < World.chunkSize.Y; y++)
                    {
                        for (int x = 0; x < World.chunkSize.X; x++)
                        {
                            for (int z = 0; z < World.chunkSize.Z; z++)
                            {
                                World.Block b = chunk.BlockData[x + World.chunkSize.X * (y + World.chunkSize.Y * z)];
                                int tileID = b.ID;
                                if (tileID == 512)
                                    continue;
                                Vector3 blockPosition = new Vector3(b.X, b.Y, b.Z);

                                // Check visibility for each face
                                for (int faceIndex = 0; faceIndex < BlockConstants.Faces.Length; faceIndex++)
                                {
                                    var (offsetIndices, texCoordIndices) = BlockConstants.Faces[faceIndex];

                                    // Skip face if an adjacent block exists
                                    Vector3i direction = BlockConstants.directions[faceIndex / 2];
                                    //Console.WriteLine($"Direction: {direction}");


                                    if (chunk.HasBlockAt((x, y, z) + direction)/* || !chunk.IsBlockVisible(x, y, z)*/)
                                        continue;

                                    // Add face vertices and texture coordinates
                                    for (int i = 0; i < 3; i++)
                                    {

                                        // Add vertex coordinates
                                        Vector3 vertex = blockPosition + BlockConstants.Offsets[offsetIndices[i]] * BlockConstants.CubeSize;
                                        dataList.Vertices.Append(vertex.X);
                                        dataList.Vertices.Append(vertex.Y);
                                        dataList.Vertices.Append(vertex.Z);

                                        // Add texture coordinates
                                        Vector2 texCoord = BlockConstants.TexCoords[texCoordIndices[i]];
                                        dataList.TexCoords.Append(texCoord.X);
                                        dataList.TexCoords.Append(texCoord.Y);

                                        // Add tile ID (for this face)
                                        dataList.TileIDs.Append(tileID);
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}