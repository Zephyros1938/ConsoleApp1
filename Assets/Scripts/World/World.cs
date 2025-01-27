using ConsoleApp1.DataManagement;
using OpenTK.Mathematics;

namespace ConsoleApp1.World
{
    public class World
    {
        static Vector3i chunkSize = (5, 5, 5);
        private static readonly Vector2i _worldSize = (2, 2);
        public readonly Vector2i worldSize = (_worldSize.X, _worldSize.Y);
        public List<Chunk> ChunkList { get; } = new(capacity: _worldSize.X * _worldSize.Y);
        static readonly int blocksPerChunk = chunkSize.X * chunkSize.Y * chunkSize.Z;

        public static uint chunkIndex = 0;
        private readonly string worldName;
        public World(string worldName)
        {
            this.worldName = worldName;
            UserData.WorldManagement.DeleteFile(worldName);
            Console.WriteLine($"Worlds Path: {UserData._WorldsPath}");
        }

        public void Generate(Vector3 location)
        {
            Chunk currentChunk = new(location);
            Vector3 LocationOffset = chunkSize * location;
            uint currentBlock = 0x00;
            for (int y = 0; y < chunkSize.Y; y++)
            {
                for (int x = 0; x < chunkSize.X; x++)
                {
                    for (int z = 0; z < chunkSize.Z; z++)
                    {
                        //Console.WriteLine($"Generating x{x} y{y} z{z} b{currentBlock}");
                        currentChunk.BlockData[currentBlock++] = new(x + LocationOffset.X, y + LocationOffset.Y, z + LocationOffset.Z, 1);
                    }
                }
            }
            ChunkList.Add(currentChunk);
        }

        public void SaveChunkToFile(Chunk chunk)
        {
            using MemoryStream ms = new();
            Span<byte> buffer = stackalloc byte[4];
            // Write Chunk Header
            BitConverter.TryWriteBytes(buffer, chunk.Center.X);
            ms.Write(buffer);
            BitConverter.TryWriteBytes(buffer, chunk.Center.Y);
            ms.Write(buffer);
            BitConverter.TryWriteBytes(buffer, chunk.Center.Z);
            ms.Write(buffer);
            BitConverter.TryWriteBytes(buffer, chunk.ID);
            ms.Write(buffer);

            // Write Block Data
            foreach (var block in chunk.BlockData)
            {
                BitConverter.TryWriteBytes(buffer, block.X);
                ms.Write(buffer);
                BitConverter.TryWriteBytes(buffer, block.Y);
                ms.Write(buffer);
                BitConverter.TryWriteBytes(buffer, block.Z);
                ms.Write(buffer);
                BitConverter.TryWriteBytes(buffer, block.ID);
                ms.Write(buffer);
            }

            UserData.WorldManagement.AppendToFile(worldName, ms.ToArray());
        }

        public Chunk GetChunk(int ID)
        {
            return ChunkList[ID];
        }

        public readonly struct Chunk(Vector3 center)
        {
            public readonly uint ID = chunkIndex++;
            public readonly Block[] BlockData { get; } = new Block[blocksPerChunk];

            public readonly Vector3 Center { get; } = center;

            public (float, float, float)[] GetBlockVertices()
            {
                //Console.WriteLine("Obtaining Block Vertices");
                List<(float, float, float)> vertices = [];

                for (int y = 0; y < chunkSize.Y; y++)
                {
                    for (int x = 0; x < chunkSize.X; x++)
                    {
                        for (int z = 0; z < chunkSize.Z; z++)
                        {
                            if (!IsBlockVisible(x, y, z))
                            {
                                var block = BlockData[x + chunkSize.X * (y + chunkSize.Y * z)];
                                vertices.Add((block.X, block.Y, block.Z));
                            }
                        }
                    }
                }
                return [.. vertices];
            }


            public bool IsBlockVisible(int x, int y, int z)
            {
                // Check if neighbors exist
                return (HasBlockAt(x + 1, y, z) && HasBlockAt(x - 1, y, z) &&
                         HasBlockAt(x, y + 1, z) && HasBlockAt(x, y - 1, z) &&
                         HasBlockAt(x, y, z + 1) && HasBlockAt(x, y, z - 1));
            }

            private bool HasBlockAt(int x, int y, int z)
            {
                // Check if block is inside chunk bounds
                if (x < 0 || y < 0 || z < 0 || x >= chunkSize.X || y >= chunkSize.Y || z >= chunkSize.Z)
                    return false;

                // Check if a block exists at this position
                int index = x + chunkSize.X * (y + chunkSize.Y * z);
                //Console.WriteLine($"{index}: {BlockData[index].ID}: {BlockData[index].ID != 0}");
                return BlockData[index].ID != 0u; // Adjust based on how "empty" blocks are defined
            }
        }

        public readonly struct Block(float X, float Y, float Z, uint ID)
        {
            public float X { get; } = X;
            public float Y { get; } = Y;
            public float Z { get; } = Z;
            public uint ID { get; } = ID;
        }
    }

    public class BiomeManagement
    {
        public float caveLevel = -10f;
        public float skyLevel = 10f;

        public Biome Caves = new Biome(
            [
                (Tiles.TileIDs.rock, 1f)
            ],
            "Caves"
        );

        public Biome Grassland = new Biome(
            [
                (Tiles.TileIDs.dirt, 10f),
                (Tiles.TileIDs.sandSoft, 1f)
            ],
            "Grasslands"
        );

        public struct Biome
        {
            public (uint, float)[] internalTileWeights;
            public string Name;
            private readonly Random rand;

            public Biome((uint, float)[] TileWeights, string Name)
            {
                this.Name = Name;
                rand = new Random();

                float totalWeight = 0f;
                foreach (var weight in TileWeights)
                {
                    totalWeight += weight.Item2;
                }

                internalTileWeights = new (uint, float)[TileWeights.Length];

                float cumulativeWeight = 0;

                for (int i = 0; i < TileWeights.Length; i++)
                {
                    cumulativeWeight += TileWeights[i].Item2 / totalWeight;
                    internalTileWeights[i] = (TileWeights[i].Item1, cumulativeWeight); // Store cumulative weights
                }
            }

            public readonly uint GetRandomBlock() 
            {
                double randNumber = rand.NextDouble();
                foreach(var tileWeight in internalTileWeights)
                {
                    if(randNumber<=tileWeight.Item2)
                    {
                        return tileWeight.Item1;
                    }
                }
                return 0;
            }
        };
    }
}