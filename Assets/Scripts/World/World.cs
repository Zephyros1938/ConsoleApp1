using ConsoleApp1.DataManagement;
using OpenTK.Mathematics;

namespace ConsoleApp1.World
{
    public class World
    {
        static Vector3i chunkSize = (2, 2, 2);
        public readonly Vector2i worldSize = (100, 100);
        public List<Chunk> ChunkList {get;} = [];
        static uint chunkIndex = 0;
        private readonly string worldName;
        public World(string worldName)
        {
            this.worldName = worldName;
            UserData.DeleteFile(worldName);
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
            ChunkList.Add(currentChunk);
        }

        public void SaveChunkToFile(Chunk chunk)
        {
            //Console.WriteLine($"Chunk Blockdata Length: {chunk.BlockData.Length}");
            //UserData.DeleteFile(worldName);
            using (MemoryStream ms = new())
            {
                ms.Write(BitConverter.GetBytes(chunk.Center.X), 0, 4);
                ms.Write(BitConverter.GetBytes(chunk.Center.Y), 0, 4);
                ms.Write(BitConverter.GetBytes(chunk.Center.Z), 0, 4);
                ms.Write(BitConverter.GetBytes(chunk.ID), 0, 4);
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
            return ChunkList[ID];
        }

        public readonly struct Chunk(Vector3 center)
        {
            public readonly uint ID = chunkIndex++;
            public readonly Block[] BlockData { get; } = new Block[chunkSize.X * chunkSize.Y * chunkSize.Z];
            public readonly Vector3 Center { get; } = center;

            public (float, float, float)[] GetBlockVertices()
            {
                Console.WriteLine("Obtaining Block Vertices");
                Vector3 centerOffset = new( 1 * Center.X,  1 * Center.Y,  1 * Center.Z);
                (float, float, float)[] blocks = BlockData.Select(p => (
                    p.Position.Item1 + centerOffset.X,
                    p.Position.Item2 + centerOffset.Y,
                    p.Position.Item3 + centerOffset.Z)
                ).ToArray();
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