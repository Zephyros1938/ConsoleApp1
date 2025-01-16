using OpenTK.Mathematics;

namespace ConsoleApp1.World.Tiles
{
    /// <summary>
    /// Contains all the IDs for the solid tiles in the game.
    /// All tiles are stored in an 8x10 grid.
    /// Start at bottom-left, go right, move up, repeat.
    /// The first tile is at (0,0) and the last tile is at (7,9).
    /// This is due to how texture coordinates are processed in our game.
    /// 
    /// Reference /Assets/Images/textures.png for the texture map.
    /// </summary>
    public readonly struct TileIDsSolid
    {
        // Row 0
        public readonly Block grassTop = new(0, new(0, 0));
        public readonly Block grassSide = new(1, new(1, 0));
        public readonly Block sandSoft = new(2, new(2, 0));
        public readonly Block sandRough = new(3, new(3, 0));
        public readonly Block rock = new(4, new(4, 0));
        public readonly Block rockOreGold = new(5, new(5, 0));
        public readonly Block coconut = new(6, new(6, 0));
        public readonly Block snow = new(7, new(7, 0));

        // Row 1
        public readonly Block dirt = new(8, new(0, 1));
        public readonly Block logSideNormal = new(9, new(1, 1));
        public readonly Block logTop = new(10, new(2, 1));
        public readonly Block logSideBirch = new(11, new(3, 1));
        public readonly Block rockOreSaphire = new(12, new(4, 1));
        public readonly Block rockOreEmerald = new(13, new(5, 1));
        public readonly Block logSidePalm = new(14, new(6, 1));
        public readonly Block oreBlockIron = new(15, new(7, 1));

        // Row 2
        public readonly Block rockQuartz = new(16, new(0, 2));
        public readonly Block craftingStationFurnaceFront = new(17, new(1, 2));
        public readonly Block rockOreRuby = new(18, new(2, 2));
        public readonly Block rockMagma = new(19, new(3, 2));
        public readonly Block bricksSnow = new(20, new(4, 2));
        public readonly Block rockOreCoal = new(21, new(5, 2));
        public readonly Block planksNormal = new(22, new(6, 2));
        public readonly Block bricksRed = new(23, new(7, 2));

        // Row 3
        public readonly Block rockMagmaHot = new(24, new(0, 3));
        public readonly Block craftingStationCauldronFront = new(25, new(1, 3));
        public readonly Block bricksMagma = new(26, new(2, 3));
        public readonly Block tntSide = new(27, new(3, 3));
        public readonly Block tntTop = new(28, new(4, 3));
        public readonly Block lampSide = new(29, new(5, 3));
        public readonly Block craftingStationWorkbenchBottom = new(30, new(6, 3));
        public readonly Block rockOreSulfur = new(31, new(7, 3));

        // Row 4
        public readonly Block sandGravel = new(32, new(0, 4));
        public readonly Block rockOreIron = new(33, new(1, 4));
        public readonly Block bricksStones = new(34, new(2, 4));
        public readonly Block bricksStonesMossy = new(35, new(3, 4));
        public readonly Block bookcase = new(36, new(4, 4));
        public readonly Block oreBlockGold = new(37, new(5, 4));
        public readonly Block bricksSandstone = new(38, new(6, 4));
        public readonly Block rockOreUnobtainium = new(39, new(7, 4));

        // Row 5
        public readonly Block rockOreKlorium = new(40, new(0, 5));
        public readonly Block rockOreCryptonite = new(41, new(1, 5));
        public readonly Block craftingStationPaintingTableTop = new(42, new(2, 5));
        public readonly Block pumkinFront = new(43, new(3, 5));
        public readonly Block pumkinTop = new(44, new(4, 5));
        public readonly Block pumkinSide = new(45, new(5, 5));
        public readonly Block bricksForgottenStoneMossy = new(46, new(6, 5));
        public readonly Block bricksForgottenStoneRune = new(47, new(7, 5));

        // Row 6
        public readonly Block bricksForgottenStone = new(48, new(0, 6));
        public readonly Block rockSmooth = new(49, new(1, 6));
        public readonly Block mobSpawner = new(50, new(2, 6));
        public readonly Block sponge = new(51, new(3, 6));
        public readonly Block carpet = new(52, new(4, 6));
        public readonly Block rockPath = new(53, new(5, 6));
        public readonly Block oreBlockPlating = new(54, new(6, 6));
        public readonly Block craftingStationWorkbenchTop = new(55, new(7, 6));

        // Row 7
        public readonly Block craftingStationWorkbenchSide = new(56, new(0, 7));
        public readonly Block craftingStationAnvil = new(57, new(1, 7));
        public readonly Block planksWaxed = new(58, new(2, 7));
        public readonly Block melonSide = new(59, new(3, 7));
        public readonly Block melonTop = new(60, new(4, 7));
        public readonly Block planksBirch = new(61, new(5, 7));
        public readonly Block planksPalm = new(62, new(6, 7));
        public readonly Block electricitySource = new(63, new(7, 7));

        // Row 8
        public readonly Block rockQuartzSoft = new(64, new(0, 8));
        public readonly Block rockQuartzSmooth = new(65, new(1, 8));
        public readonly Block magicStone = new(66, new(2, 8)); // No idea wtf this is supposed to be in the texture so i just set it to this lol - Zephyros1938
        public readonly Block magicStoneSide = new(67, new(3, 8));
        public readonly Block magicStoneTop = new(68, new(4, 8));
        public readonly Block magicMoss = new(69, new(5, 8));
        public readonly Block magicGold = new(70, new(6, 8));
        public readonly Block pillarBase = new(71, new(7, 8));

        // Row 9
        public readonly Block pillarMid = new(72, new(0, 9));
        public readonly Block pillarTop = new(73, new(1, 9));
        public readonly Block lampTop = new(74, new(2, 9));
        public readonly Block alienMoss1 = new(75, new(3, 9)); // I also have no idea what this is supposed to be either help - Zephyros1938
        public readonly Block alienMoss2 = new(76, new(4, 9));
        public readonly Block alienMoss3 = new(77, new(5, 9));
        public readonly Block alienMoss4 = new(78, new(6, 9));
        public readonly Block alienMoss5 = new(79, new(7, 9));

        public TileIDsSolid()
        {
        }
    }

    /// <summary>
    /// Contains all the IDs for the solid tiles in the game.
    /// All tiles are stored in an 8x10 grid.
    /// Start at bottom-left, go right, move up, repeat.
    /// The first tile is at (0,0) and the last tile is at (7,9).
    /// This is due to how texture coordinates are processed in our game.
    /// 
    /// Reference /Assets/Images/textures_tr.png for the texture map.
    /// </summary>
    public readonly struct TileIDsTransparent
    {
        // Row 0
        public readonly Block leavesOak = new(0, new(0, 0));
        public readonly Block cactiSide = new(1, new(1, 0));
        public readonly Block bushBerry = new(2, new(2, 0));
        public readonly Block thatchFlowers = new(3, new(3, 0));
        public readonly Block leavesOakApples = new(4, new(4, 0));
        public readonly Block leavesOakBirdnest = new(5, new(5, 0));
        public readonly Block bones = new(6, new(6, 0));
        public readonly Block cobwebs = new(7, new(7, 0));

        // Row 1
        public readonly Block foliage = new(8, new(0, 1));
        public readonly Block cactiCenter = new(9, new(1, 1));
        public readonly Block gemblockEmerald = new(10, new(2, 1));
        public readonly Block gemblockSaphire = new(11, new(3, 1));
        public readonly Block gemblockRuby = new(12, new(4, 1));
        public readonly Block thatchIron = new(13, new(5, 1));
        public readonly Block leavesSpruce = new(14, new(6, 1));
        public readonly Block leavesAcacia = new(15, new(7, 1));

        // Row 2
        public readonly Block liquidAcid = new(16, new(0, 2));
        public readonly Block leavesPine = new(17, new(1, 2));
        public readonly Block leavesTropical = new(18, new(2, 2));
        public readonly Block liquidWater = new(19, new(3, 2));
        public readonly Block ice = new(20, new(4, 2));
        public readonly Block liquidWaterLillypad = new(21, new(5, 2));
        public readonly Block bushIvy = new(22, new(6, 2));
        public readonly Block bushBlueberries = new(23, new(7, 2));

        // Row 3
        public readonly Block liquidMagma = new(24, new(0, 3));
        public readonly Block thatchOvergrown = new(25, new(1, 3));
        public readonly Block leavesJungleThick = new(26, new(2, 3));
        public readonly Block sapplingRedwood = new(27, new(3, 3));
        public readonly Block thatch = new(28, new(4, 3));
        public readonly Block flowerYellow = new(29, new(5, 3));
        public readonly Block flowerRed = new(30, new(6, 3));
        public readonly Block flowerWhite = new(31, new(7, 3));

        // Row 4
        public readonly Block glass = new(32, new(0, 4));
        public readonly Block leavesAcaciaBirdnest = new(33, new(1, 4));
        public readonly Block leavesJungleSparse = new(34, new(2, 4));
        public readonly Block leavesJungle = new(35, new(3, 4));
        public readonly Block leavesJungleFlowering = new(36, new(4, 4));
        public readonly Block leavesBirch = new(37, new(5, 4));
        public readonly Block cloud = new(38, new(6, 4));
        public readonly Block cloudStormy = new(39, new(7, 4));

        // Row 5
        public readonly Block glassPane = new(40, new(0, 5));
        public readonly Block demonWeeds = new(41, new(1, 5));
        public readonly Block gemblockCoal = new(42, new(2, 5));
        public readonly Block beehive = new(43, new(3, 5));
        public readonly Block shrubTwiggy = new(44, new(4, 5));
        public readonly Block shrubFan = new(45, new(5, 5));
        public readonly Block shrub = new(46, new(6, 5));
        public readonly Block candle = new(47, new(7, 5));

        // Row 6
        public readonly Block wheat1 = new(48, new(0, 6));
        public readonly Block wheat2 = new(49, new(1, 6));
        public readonly Block wheat3 = new(50, new(2, 6));
        public readonly Block wheat4 = new(51, new(3, 6));
        public readonly Block shrubCacti = new(52, new(4, 6));
        public readonly Block sapplingTreeApples = new(53, new(5, 6));
        public readonly Block sapplingTreeTropical = new(54, new(6, 6));
        public readonly Block grass1 = new(55, new(7, 6));

        // Row 7
        public readonly Block sapplingTreeAcaciaLong = new(56, new(0, 7));
        public readonly Block sapplingTreeSwamp = new(57, new(1, 7));
        public readonly Block sapplingTreePalm = new(58, new(2, 7));
        public readonly Block sapplingTreeBirch = new(59, new(3, 7));
        public readonly Block sapplingTreeAcacia = new(60, new(4, 7));
        public readonly Block sapplingTreeTwiggy = new(61, new(5, 7));
        public readonly Block sapplingTreePine = new(62, new(6, 7));
        public readonly Block sapplingTreeOak = new(63, new(7, 7));

        // Row 8
        public readonly Block alienFlowers = new(64, new(0, 8));
        public readonly Block cloudDroplets = new(65, new(1, 8));
        public readonly Block alienGrass = new(66, new(2, 8));
        public readonly Block alienShrubFlower = new(67, new(3, 8));
        public readonly Block grass2 = new(68, new(4, 8));
        public readonly Block moss = new(69, new(5, 8));
        public readonly Block alienMoss = new(70, new(6, 8));
        public readonly Block vines1 = new(71, new(7, 8));

        // Row 9
        public readonly Block chain = new(72, new(0, 9));
        public readonly Block vines2 = new(73, new(1, 9));
        public readonly Block grass3 = new(74, new(2, 9));
        public readonly Block coral = new(75, new(3, 9));

        public TileIDsTransparent()
        {
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

    public readonly struct Block(uint ID, TexCoord TexCoordStart)
    {
        public uint ID { get; } = ID;
        public TexCoord TexCoordStart { get; } = TexCoordStart;
    }

    public readonly struct TexCoord(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
    }
}