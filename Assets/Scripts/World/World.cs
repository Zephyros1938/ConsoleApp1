using OpenTK.Mathematics;

namespace ConsoleApp1.World
{
    public class World
    {
        Vector3 renderDistance = (2f, 2f, 2f);
        Vector3 chunkSize = (10f, 10f, 10f);
        (int, Vector3) chunkList;
        float cubeSize = 0.5f;

        Vector3[] pointOffsets =
        {
            new(1,1,1),
            new(1,-1,1),
            new(-1,-1,1),
            new(-1,1,1),
            new(1,1,-1),
            new(1,-1,-1),
            new(-1,-1,-1),
            new(-1,1,-1),
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

            for (int n = 0; n < pointOffsets.Length; n++)
            {
                pointOffsets[n] *= this.cubeSize;
            }
        }

        public void Generate()
        {
            Console.WriteLine("Generating..");
        }
    }

    public readonly struct Chunk(Vector3 center)
    {
        public readonly (uint, Vector3) BlockData { get; }
        public readonly Vector3 Center { get; } = center;

        public void Generate()
        {

        }
    }

    public readonly struct Block(float X, float Y, float Z)
    {
        // public int ID { get; } = ID;
        // public TexCoord TexCoordStart { get; } = TexCoordStart;

        public Vector3 BlockVec3 { get; } = new(X,Y,Z);
        public float ID { get; } = X;
    }
}