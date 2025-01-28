using System.Diagnostics;
using ConsoleApp1.DataManagement;
using OpenTK.Mathematics;

namespace ConsoleApp1.World
{
    public class World
    {
        static Vector3i chunkSize = (25, 25, 25);
        private static readonly Vector2i _worldSize = (7, 7);
        public readonly Vector2i worldSize = (_worldSize.X, _worldSize.Y);
        public HashSet<Chunk> ChunkList { get; } = new(capacity: _worldSize.X * _worldSize.Y);
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
            if (ChunkList.Contains(currentChunk))
            {
                Console.WriteLine($"Chunk {currentChunk.ID} is already in the chunklist! did you mean to generate this chunk?\n\tAborting...");
                return;
            }
            for (int y = 0; y < chunkSize.Y; y++)
            {
                for (int x = 0; x < chunkSize.X; x++)
                {
                    for (int z = 0; z < chunkSize.Z; z++)
                    {
                        int randomBlockID;
                        Vector3 pos = (x + LocationOffset.X, y + LocationOffset.Y, z + LocationOffset.Z);
                        if (pos.Y <= BiomeManagement.caveLevel)
                        {
                            randomBlockID = BiomeManagement.Caves.GetRandomBlock();
                        }
                        else
                        {
                            randomBlockID = BiomeManagement.Grassland.GetRandomBlock();
                            if (randomBlockID == Tiles.TileIDs.dirt && !currentChunk.HasBlockAt(x, y + 1, z))
                            {
                                randomBlockID = Tiles.TileIDs.grassTop;
                            }
                        }
                        currentChunk.BlockData[currentBlock++] = new(pos.X, pos.Y, pos.Z, randomBlockID);
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

        public readonly struct Chunk(Vector3 center)
        {
            public readonly uint ID = chunkIndex++;
            public readonly Block[] BlockData { get; } = new Block[blocksPerChunk];

            public readonly Vector3 Center { get; } = center;

            public (float, float, float, int)[] GetBlockVertices()
            {
                //Console.WriteLine("Obtaining Block Vertices");
                List<(float, float, float, int)> vertices = [];

                for (int y = 0; y < chunkSize.Y; y++)
                {
                    for (int x = 0; x < chunkSize.X; x++)
                    {
                        for (int z = 0; z < chunkSize.Z; z++)
                        {
                            if (IsBlockVisible(x, y, z))
                            {
                                var block = BlockData[x + chunkSize.X * (y + chunkSize.Y * z)];
                                vertices.Add((block.X, block.Y, block.Z, block.ID));
                            }
                        }
                    }
                }
                return [.. vertices];
            }


            public bool IsBlockVisible(int x, int y, int z)
            {
                // Check if neighbors exist
                return !(HasBlockAt(x + 1, y, z) && HasBlockAt(x - 1, y, z) &&
                         HasBlockAt(x, y + 1, z) && HasBlockAt(x, y - 1, z) &&
                         HasBlockAt(x, y, z + 1) && HasBlockAt(x, y, z - 1));
            }

            public bool HasBlockAt(int x, int y, int z)
            {
                // Check if block is inside chunk bounds
                if (x < 0 || y < 0 || z < 0 || x >= chunkSize.X || y >= chunkSize.Y || z >= chunkSize.Z)
                    return false;

                // Check if a block exists at this position
                int index = x + chunkSize.X * (y + chunkSize.Y * z);
                //Console.WriteLine($"{index}: {BlockData[index].ID}: {BlockData[index].ID != 0}");
                return BlockData[index].ID != -1; // Adjust based on how "empty" blocks are defined
            }
        }

        public readonly struct Block(float X, float Y, float Z, int ID)
        {
            public float X { get; } = X;
            public float Y { get; } = Y;
            public float Z { get; } = Z;
            public int ID { get; } = ID;
        }
    }

    #region biome management
    public class BiomeManagement
    {
        public static float caveLevel = -10f;
        public static float skyLevel = 10f;

        public static Biome Caves = new Biome(
            [
                (Tiles.TileIDs.rock, 100f)
            ],
            "Caves"
        );

        public static Biome Grassland = new Biome(
            [
                (Tiles.TileIDs.dirt, 100f),
            ],
            "Grasslands"
        );

        public static Dictionary<string, Biome> BiomeKey = new Dictionary<string, Biome>()
        {
            { "Grasslands", Grassland },
            { "Caves", Caves }
        };

        public struct Biome
        {
            public (int, float)[] internalTileWeights;
            public string Name;
            private readonly Random rand;

            public Biome((int, float)[] TileWeights, string Name)
            {
                this.Name = Name;
                rand = new Random();

                float totalWeight = 0f;
                foreach (var weight in TileWeights)
                {
                    totalWeight += weight.Item2;
                }

                internalTileWeights = new (int, float)[TileWeights.Length];

                float cumulativeWeight = 0;

                for (int i = 0; i < TileWeights.Length; i++)
                {
                    cumulativeWeight += TileWeights[i].Item2 / totalWeight;
                    internalTileWeights[i] = (TileWeights[i].Item1, cumulativeWeight); // Store cumulative weights
                }
            }

            public readonly int GetRandomBlock()
            {
                double randNumber = rand.NextDouble();
                foreach (var tileWeight in internalTileWeights)
                {
                    if (randNumber <= tileWeight.Item2)
                    {
                        return tileWeight.Item1;
                    }
                }
                return -1;
            }
        };
    }
    #endregion
}