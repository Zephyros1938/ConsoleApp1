using ConsoleApp1.DataManagement;
using OpenTK.Mathematics;

namespace ConsoleApp1.World
{
    public class World
    {
        static Vector3i chunkSize = (2, 2, 2);
        static Vector2i worldSize = (10, 10);
        public readonly List<Chunk> chunkList = [];
        static UInt64 chunkIndex = 0;
        string worldName;
        public World(string worldName)
        {
            this.worldName = worldName;
            Console.WriteLine($"Worlds Path: {UserData._WorldsPath}");
        }

        public void Generate(Vector3 location)
        {
            Chunk currentChunk = new(location);
            uint currentBlock = 0x00;
            Console.WriteLine($"Current Chunk: {currentChunk.Center}");
            for (int y = 0; y < chunkSize.Y; y++)
            {
                Console.WriteLine($"\tGenerating Y-Level {y}");
                for (int x = 0; x < chunkSize.X; x++)
                {
                    for (int z = 0; z < chunkSize.Z; z++)
                    {
                        //Console.WriteLine($"Generating x{x} y{y} z{z} b{currentBlock}");
                        currentChunk.BlockData[currentBlock] = new(x, y, z, currentBlock++);
                    }
                }
            }
            chunkList.Add(currentChunk);
        }

        public void SaveChunkToFile(Chunk chunk)
        {
            Console.WriteLine($"Chunk Blockdata Length: {chunk.BlockData.Length}");
            UserData.DeleteFile(worldName);
            using (MemoryStream ms = new())
            {
                ms.Write(BitConverter.GetBytes(chunk.ID), 0, 8);
                chunk.BlockData.ToList().ForEach(new Action<Block>((Block p) =>
                {
                    ms.Write(BitConverter.GetBytes(p.Data.Item1), 0, 4);
                    ms.Write(BitConverter.GetBytes(p.Data.Item2), 0, 4);
                    ms.Write(BitConverter.GetBytes(p.Data.Item3), 0, 4);
                    ms.Write(BitConverter.GetBytes(p.Data.Item4), 0, 4);
                }));
                UserData.AppendToFile(worldName, ms.ToArray());
            }
        }

        public Chunk GetChunk(int ID)
        {
            return chunkList[ID];
        }

        public readonly struct Chunk(Vector3 center)
        {
            public readonly UInt64 ID = chunkIndex++;
            public readonly Block[] BlockData { get; } = new Block[chunkSize.X * chunkSize.Y * chunkSize.Z];
            public readonly Vector3 Center { get; } = center;

            public (float, float, float)[] GetBlockVertices()
            {
                Console.WriteLine("Obtaining Block Vertices");
                Vector3 centerOffset = new(1 + 1 * center.X, 1 + 1 * center.Y, 1 + 1 * center.Z);
                (float, float, float)[] blocks = BlockData.Select(p => (p.Position.Item1 * centerOffset.X, p.Position.Item2 * centerOffset.Y, p.Position.Item3 * centerOffset.Z)).ToArray();
                return blocks;
            }
        }

        public readonly struct Block(float X, float Y, float Z, uint ID)
        {
            public (float, float, float) Position { get; } = new(X, Y, Z);
            public uint ID { get; } = ID;
            public (float, float, float, uint) Data { get; } = (X, Y, Z, ID);
        }
    }


}