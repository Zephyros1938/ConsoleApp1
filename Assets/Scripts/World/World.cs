using System.Diagnostics;
using System.Runtime.CompilerServices;
using ConsoleApp1.DataManagement;
using OpenTK.Mathematics;

namespace ConsoleApp1.World
{
    public class World
    {
        private static readonly Vector2i _worldSize = (10, 10);
        public readonly Vector2i worldSize = (_worldSize.X, _worldSize.Y);
        private readonly string worldName;

        #region Chunk Variables
        public static readonly Vector3i chunkSize = (25, 25, 25);
        private HashSet<Chunk> _ChunkList = new(capacity: _worldSize.X * _worldSize.Y * _worldSize.X);
        public class ChunkListEventArgs : EventArgs
        {
            public HashSet<Chunk>? ChunkList { get; set; }
        }
        public delegate void ChunkListChangedHandler(object source, ChunkListEventArgs e);
        public event ChunkListChangedHandler? ChunkListChanged;
        protected virtual void OnChunkListChanged()
        {
            ChunkListChanged?.Invoke(this, new ChunkListEventArgs { ChunkList = _ChunkList });
        }
        public HashSet<Chunk> ChunkList { get { return _ChunkList; } set { _ChunkList = value; OnChunkListChanged(); } }
        public static readonly int blocksPerChunk = chunkSize.X * chunkSize.Y * chunkSize.Z;
        static uint chunkIndex = 0;

        #endregion
        public World(string worldName)
        {
            this.worldName = worldName;
            UserData.WorldManagement.DeleteFile(worldName);
            Console.WriteLine($"Worlds Path: {UserData._WorldsPath}");
        }
        [MethodImpl(512)]
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
                        int randomBlockID = 512;
                        Vector3 pos = (x + LocationOffset.X, y + LocationOffset.Y, z + LocationOffset.Z);
                        // if (pos.Y <= BiomeManagement.caveLevel)
                        // {
                        //     randomBlockID = BiomeManagement.Caves.GetRandomBlock();
                        // }
                        // else if (pos.Y >= BiomeManagement.skyLevel)
                        // {
                        //     randomBlockID = BiomeManagement.SkyIslands.GetRandomBlock();
                        // }
                        // else
                        // {
                        //     randomBlockID = BiomeManagement.Grassland.GetRandomBlock();
                        //     // if (randomBlockID == Tiles.TileIDs.dirt && !currentChunk.HasBlockAt(x, y + 1, z))
                        //     // {
                        //     //     randomBlockID = Tiles.TileIDs.grassTop;
                        //     // }
                        // }
                        foreach (var item in BiomeManagement.BiomeKey)
                        {
                            if(pos.Y<=item.Value.heightLevel)
                            {
                                randomBlockID = item.Value.GetRandomBlock();
                            }
                        }
                        //randomBlockID = 0;
                        currentChunk.BlockData[currentBlock++] = new(pos.X, pos.Y, pos.Z, randomBlockID);
                    }
                }
            }
            ChunkList.Add(currentChunk);
        }

        [MethodImpl(512)]
        public void SaveChunkToFile(Chunk chunk)
        {
            using MemoryStream ms = new();
            Span<byte> buffer = stackalloc byte[4];
            // Write Chunk Header
            //BitConverter.TryWriteBytes(buffer, 0xFEFEFEFE);
            //ms.Write(buffer);
            BitConverter.TryWriteBytes(buffer, chunk.Center.X);
            ms.Write(buffer);
            BitConverter.TryWriteBytes(buffer, chunk.Center.Y);
            ms.Write(buffer);
            BitConverter.TryWriteBytes(buffer, chunk.Center.Z);
            ms.Write(buffer);
            BitConverter.TryWriteBytes(buffer, chunk.ID);
            ms.Write(buffer);

            int PreviousBlock = -0xff;
            uint PreviousBlockCount = 1;

            // Write Block Data
            //Console.WriteLine("Parsing block data...");
            foreach (var block in chunk.BlockData)
            {
                //Console.WriteLine($"{block.ID} : {PreviousBlock} | {PreviousBlockCount}");
                //Console.WriteLine(block.ID == PreviousBlock);
                if (block.ID != PreviousBlock)
                {
                    if (PreviousBlock != -0xff)
                    {
                        //Console.WriteLine("Call Block Count");
                        BitConverter.TryWriteBytes(buffer, 0xffffffff);
                        ms.Write(buffer);
                        //Console.WriteLine($"Previous Block Count is {PreviousBlockCount}");
                        BitConverter.TryWriteBytes(buffer, PreviousBlockCount);
                        ms.Write(buffer);
                        //Console.WriteLine($"Block ID is {PreviousBlock}");
                        BitConverter.TryWriteBytes(buffer, PreviousBlock);
                        ms.Write(buffer);
                    }
                    PreviousBlockCount = 1;
                }
                else
                {
                    PreviousBlockCount++;
                }
                PreviousBlock = block.ID;
            }
            if (PreviousBlock != -0xff || PreviousBlock != chunk.BlockData.Last().ID)
            {
                BitConverter.TryWriteBytes(buffer, 0xffffffff);
                ms.Write(buffer);
                //Console.WriteLine($"Previous Block Count is {PreviousBlockCount}");
                BitConverter.TryWriteBytes(buffer, PreviousBlockCount);
                ms.Write(buffer);
                //Console.WriteLine($"Block ID is {PreviousBlock}");
                BitConverter.TryWriteBytes(buffer, PreviousBlock);
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

            public (float, float, float, int)[] GetBlockVerticesLossless()
            {
                //Console.WriteLine("Obtaining Block Vertices");
                List<(float, float, float, int)> vertices = [];

                for (int y = 0; y < chunkSize.Y; y++)
                {
                    for (int x = 0; x < chunkSize.X; x++)
                    {
                        for (int z = 0; z < chunkSize.Z; z++)
                        {
                            var block = BlockData[x + chunkSize.X * (y + chunkSize.Y * z)];
                            vertices.Add((block.X, block.Y, block.Z, block.ID));
                        }
                    }
                }
                return [.. vertices];
            }

            [MethodImpl(512)]
            public bool IsBlockVisible(int x, int y, int z)
            {
                // Check if neighbors exist
                return !(HasBlockAt(x + 1, y, z) && HasBlockAt(x - 1, y, z) &&
                         HasBlockAt(x, y + 1, z) && HasBlockAt(x, y - 1, z) &&
                         HasBlockAt(x, y, z + 1) && HasBlockAt(x, y, z - 1));
            }
            [MethodImpl(512)]
            public bool HasBlockAt(int x, int y, int z)
            {
                // Check if block is inside chunk bounds
                if (x < 0 || y < 0 || z < 0 || x >= chunkSize.X || y >= chunkSize.Y || z >= chunkSize.Z)
                {
                    //Console.WriteLine($"Attempted to get block at ({x},{y},{z}) which was outside of chunk borders ({chunkSize})");
                    return false;
                }

                // Check if a block exists at this position
                int index = x + chunkSize.X * (y + chunkSize.Y * z);
                //Console.WriteLine($"{index}: {BlockData[index].ID}: {BlockData[index].ID != 0}");
                return BlockData[index].ID != 512; // Adjust based on how "empty" blocks are defined
            }
            [MethodImpl(512)]
            public bool HasBlockAt(Vector3i loc)
            {
                // Check if block is inside chunk bounds
                if (loc.X < 0 || loc.Y < 0 || loc.Z < 0 || loc.X >= chunkSize.X || loc.Y >= chunkSize.Y || loc.Z >= chunkSize.Z)
                {
                    //Console.WriteLine($"Attempted to get block at ({x},{y},{z}) which was outside of chunk borders ({chunkSize})");
                    return false;
                }

                // Check if a block exists at this position
                int index = loc.X + chunkSize.X * (loc.Y + chunkSize.Y * loc.Z);
                //Console.WriteLine($"{index}: {BlockData[index].ID}: {BlockData[index].ID != 0}");
                return BlockData[index].ID != 512; // Adjust based on how "empty" blocks are defined
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
        private static Biome Caves = new(
            [
                (Tiles.TileIDs.rock, 100f),
                (Tiles.TileIDs.rockOreCoal, 10f),
                (Tiles.TileIDs.rockOreIron, 1f),
                (Tiles.TileIDs.rockOreGold, 0.1f)
            ],
            "Caves",
            -50f
        );

        private static Biome SkyIslands = new(
            [
                (Tiles.TileIDs.air, 100f),
                (Tiles.TileIDs.cloud, 50f),
                (Tiles.TileIDs.cloudStormy, 5f),
            ],
            "SkyIslands",
            50f
        );

        private static Biome Grassland = new(
            [
                (Tiles.TileIDs.dirt, 100f),
                //(Tiles.TileIDs.air, 5f),
            ],
            "Grasslands"
        );

        public static Dictionary<string, Biome> BiomeKey = new()
        {
            { "SkyIslands", SkyIslands },
            { "Grasslands", Grassland },
            { "Caves", Caves },
        };

        public struct Biome
        {
            public (int, float)[] internalTileWeights;
            public float heightLevel;
            public string Name;
            private readonly Random rand;

            

            public Biome((int, float)[] TileWeights, string Name, float heightLevel = 0f)
            {
                this.Name = Name;
                this.heightLevel = heightLevel;
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
            [MethodImpl(512)]
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
                return 512;
            }
        };
    }
    #endregion
}