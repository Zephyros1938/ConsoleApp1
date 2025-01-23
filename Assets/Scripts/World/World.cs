using OpenTK.Mathematics;

namespace ConsoleApp1.World
{
    public class World
    {
        Vector3 renderDistance = (2f, 2f, 2f);
        Vector3 chunkSize = (10f, 10f, 10f);
        (int, Chunk) chunkList;
        float cubeSize = 0.5f;

        Vector4[] offsets = new Vector4[]
        {
            new Vector4( 1.0f,  1.0f,  1.0f,  0.0f), //+++ 0
            new Vector4( 1.0f,  1.0f, -1.0f,  0.0f), //++- 1
            new Vector4( 1.0f, -1.0f,  1.0f,  0.0f), //+-+ 2
            new Vector4( 1.0f, -1.0f, -1.0f,  0.0f), //+-- 3
            new Vector4(-1.0f,  1.0f,  1.0f,  0.0f), //-++ 4
            new Vector4(-1.0f,  1.0f, -1.0f,  0.0f), //-+- 5
            new Vector4(-1.0f, -1.0f,  1.0f,  0.0f), //--+ 6
            new Vector4(-1.0f, -1.0f, -1.0f,  0.0f)  //--- 7
        };

        // Texture coordinates
        Vector2[] texCoords = new Vector2[]
        {
            new Vector2(1.0f, 1.0f), //++ 0
            new Vector2(1.0f, 0.0f), //+- 1
            new Vector2(0.0f, 0.0f), //-- 2
            new Vector2(0.0f, 1.0f)  //-+ 3
        };

        // Face offset indexes
        Matrix2x3[] faceOffsetIndexes = new Matrix2x3[]
        {
            // Top face
            new Matrix2x3(0, 1, 4, 0, 1, 3),
            new Matrix2x3(1, 5, 4, 1, 2, 3),
            
            // Bottom face
            new Matrix2x3(2, 6, 3, 0, 3, 1),
            new Matrix2x3(6, 7, 3, 3, 2, 1),

            // Front face
            new Matrix2x3(0, 2, 1, 0, 1, 3),
            new Matrix2x3(2, 3, 1, 1, 2, 3),

            // Back face
            new Matrix2x3(4, 5, 6, 0, 3, 1),
            new Matrix2x3(5, 7, 6, 3, 2, 1),

            // Right face
            new Matrix2x3(0, 4, 2, 0, 1, 3),
            new Matrix2x3(4, 6, 2, 1, 2, 3),

            // Left face
            new Matrix2x3(1, 3, 5, 0, 3, 1),
            new Matrix2x3(3, 7, 5, 3, 2, 1)
        };

        public World(Vector3? renderDistance = null, Vector3? chunkSize = null, float? cubeSize = null)
        {
            if (renderDistance.HasValue)
            {
                this.renderDistance = renderDistance.Value;
            }
            if (chunkSize.HasValue)
            {
                this.chunkSize = chunkSize.Value;
            }
            if (cubeSize.HasValue)
            {
                this.cubeSize = cubeSize.Value;
            }

            for (int n = 0; n < offsets.Length; n++)
            {
                offsets[n] *= this.cubeSize;
            }
        }

        public float GetBlockSize()
        {
            return cubeSize;
        }

        public void Generate()
        {
            for (int x = 0; x < chunkSize.X; x++)
            {
                for (int y = 0; x < chunkSize.Y; y++)
                {
                    for (int z = 0; x < chunkSize.Z; z++)
                    {

                    }
                }
            }
        }
    }

    public readonly struct Chunk(Vector3 center)
    {
        public readonly (uint, Vector3)[] BlockData { get; }
        public readonly Vector3 Center { get; } = center;
    }

    public readonly struct BlockFace(float X, float Y, float Z)
    {
        // public int ID { get; } = ID;
        // public TexCoord TexCoordStart { get; } = TexCoordStart;

        public Vector3 BlockVec3 { get; } = new(X, Y, Z);
        public float ID { get; } = X;
    }

    public readonly struct Block(float X, float Y, float Z, uint ID)
    {
        public Vector3 Position { get; } = new(X, Y, Z);
        public uint ID { get; } = ID;
    }
}