using OpenTK.Mathematics;

namespace ConsoleApp1.World
{
    public class World()
    {
        static Vector3i chunkSize = (10, 10, 10);
        readonly List<Chunk> chunkList = [];

        public void Generate()
        {
            Chunk currentChunk = new((0f, 0f, 0f));
            uint currentBlock = 0x00;
            for (int y = 0; y < chunkSize.Y; y++)
            {
                for (int x = 0; x < chunkSize.X; x++)
                {
                    for (int z = 0; z < chunkSize.Z; z++)
                    {
                        Console.WriteLine($"Generating x{x} y{y} z{z} b{currentBlock}");
                        currentChunk.BlockData[currentBlock] = new(x, y, z, currentBlock++);
                    }
                }
            }
            chunkList.Add(currentChunk);
        }

        public Chunk GetChunk(int ID)
        {
            return chunkList[ID];
        }

        public readonly struct Chunk(Vector3 center)
        {
            public readonly Block[] BlockData { get; } = new Block[chunkSize.X * chunkSize.Y * chunkSize.Z];
            public readonly Vector3 Center { get; } = center;

            public Vector3[] GetBlockVertices()
            {
                Console.WriteLine("Obtaining Block Vertices");
                Vector3[] blocks = BlockData.Select(p => p.Position).ToArray();
                return blocks;
            }
        }

        public readonly struct BlockFace(float X, float Y, float Z)
        {
            public Vector3 BlockVec3 { get; } = new(X, Y, Z);
            public float ID { get; } = X;
        }

        public readonly struct Block(float X, float Y, float Z, uint ID)
        {
            public Vector3 Position { get; } = new(X, Y, Z);
            public uint ID { get; } = ID;
        }
    }


}