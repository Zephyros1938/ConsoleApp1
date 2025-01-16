using OpenTK.Mathematics;

namespace ConsoleApp1.World.Tiles
{

    /// <summary>
    /// Contains all the IDs for all tiles in the game.
    /// All tiles are stored in an 8x10 grid.
    /// Start at bottom-left, go right, move up, repeat.
    /// The first tile is at (0,0) and the last tile is at (7,9).
    /// This is due to how texture coordinates are processed in our game.
    /// 
    /// Reference /Assets/Images/textures.png for the solid texture map.
    /// Reference /Assets/Images/textures_tr.png for the transparent texture map.
    /// </summary>
    public readonly struct TileIDs
    {
        // Nontransparent Blocks
        // Row 0
        public static readonly Block grassTop = new(0,0, 0);
        public static readonly Block grassSide = new(1,1, 0);
        public static readonly Block sandSoft = new(2,2, 0);
        public static readonly Block sandRough = new(3,3, 0);
        public static readonly Block rock = new(4,4, 0);
        public static readonly Block rockOreGold = new(5,5, 0);
        public static readonly Block coconut = new(6,6, 0);
        public static readonly Block snow = new(7,7, 0);

        // Row 1
        public static readonly Block dirt = new(8,0, 1);
        public static readonly Block logSideNormal = new(9,1, 1);
        public static readonly Block logTop = new(10,2, 1);
        public static readonly Block logSideBirch = new(11,3, 1);
        public static readonly Block rockOreSaphire = new(12,4, 1);
        public static readonly Block rockOreEmerald = new(13,5, 1);
        public static readonly Block logSidePalm = new(14,6, 1);
        public static readonly Block oreBlockIron = new(15,7, 1);

        // Row 2
        public static readonly Block rockQuartz = new(16,0, 2);
        public static readonly Block craftingStationFurnaceFront = new(17,1, 2);
        public static readonly Block rockOreRuby = new(18,2, 2);
        public static readonly Block rockMagma = new(19,3, 2);
        public static readonly Block bricksSnow = new(20,4, 2);
        public static readonly Block rockOreCoal = new(21,5, 2);
        public static readonly Block planksNormal = new(22,6, 2);
        public static readonly Block bricksRed = new(23,7, 2);

        // Row 3
        public static readonly Block rockMagmaHot = new(24,0, 3);
        public static readonly Block craftingStationCauldronFront = new(25,1, 3);
        public static readonly Block bricksMagma = new(26,2, 3);
        public static readonly Block tntSide = new(27,3, 3);
        public static readonly Block tntTop = new(28,4, 3);
        public static readonly Block lampSide = new(29,5, 3);
        public static readonly Block craftingStationWorkbenchBottom = new(30,6, 3);
        public static readonly Block rockOreSulfur = new(31,7, 3);

        // Row 4
        public static readonly Block sandGravel = new(32,0, 4);
        public static readonly Block rockOreIron = new(33,1, 4);
        public static readonly Block bricksStones = new(34,2, 4);
        public static readonly Block bricksStonesMossy = new(35,3, 4);
        public static readonly Block bookcase = new(36,4, 4);
        public static readonly Block oreBlockGold = new(37,5, 4);
        public static readonly Block bricksSandstone = new(38,6, 4);
        public static readonly Block rockOreUnobtainium = new(39,7, 4);

        // Row 5
        public static readonly Block rockOreKlorium = new(40,0, 5);
        public static readonly Block rockOreCryptonite = new(41,1, 5);
        public static readonly Block craftingStationPaintingTableTop = new(42,2, 5);
        public static readonly Block pumkinFront = new(43,3, 5);
        public static readonly Block pumkinTop = new(44,4, 5);
        public static readonly Block pumkinSide = new(45,5, 5);
        public static readonly Block bricksForgottenStoneMossy = new(46,6, 5);
        public static readonly Block bricksForgottenStoneRune = new(47,7, 5);

        // Row 6
        public static readonly Block bricksForgottenStone = new(48,0, 6);
        public static readonly Block rockSmooth = new(49,1, 6);
        public static readonly Block mobSpawner = new(50,2, 6);
        public static readonly Block sponge = new(51,3, 6);
        public static readonly Block carpet = new(52,4, 6);
        public static readonly Block rockPath = new(53,5, 6);
        public static readonly Block oreBlockPlating = new(54,6, 6);
        public static readonly Block craftingStationWorkbenchTop = new(55,7, 6);

        // Row 7
        public static readonly Block craftingStationWorkbenchSide = new(56,0, 7);
        public static readonly Block craftingStationAnvil = new(57,1, 7);
        public static readonly Block planksWaxed = new(58,2, 7);
        public static readonly Block melonSide = new(59,3, 7);
        public static readonly Block melonTop = new(60,4, 7);
        public static readonly Block planksBirch = new(61,5, 7);
        public static readonly Block planksPalm = new(62,6, 7);
        public static readonly Block electricitySource = new(63,7, 7);

        // Row 8
        public static readonly Block rockQuartzSoft = new(64,0, 8);
        public static readonly Block rockQuartzSmooth = new(65,1, 8);
        public static readonly Block magicStone = new(66,2, 8); // No idea wtf this is supposed to be in the texture so i just set it to this lol - Zephyros1938
        public static readonly Block magicStoneSide = new(67,3, 8);
        public static readonly Block magicStoneTop = new(68,4, 8);
        public static readonly Block magicMoss = new(69,5, 8);
        public static readonly Block magicGold = new(70,6, 8);
        public static readonly Block pillarBase = new(71,7, 8);

        // Row 9
        public static readonly Block pillarMid = new(72,0, 9);
        public static readonly Block pillarTop = new(73,1, 9);
        public static readonly Block lampTop = new(74,2, 9);
        public static readonly Block alienMoss1 = new(75,3, 9); // I also have no idea what this is supposed to be either help - Zephyros1938
        public static readonly Block alienMoss2 = new(76,4, 9);
        public static readonly Block alienMoss3 = new(77,5, 9);
        public static readonly Block alienMoss4 = new(78,6, 9);
        public static readonly Block alienMoss5 = new(79,7, 9);

        // Transparent Blocks
        // Row 0
        public static readonly Block leavesOak = new(80,0, 0);
        public static readonly Block cactiSide = new(81,1, 0);
        public static readonly Block bushBerry = new(82,2, 0);
        public static readonly Block thatchFlowers = new(83,3, 0);
        public static readonly Block leavesOakApples = new(84,4, 0);
        public static readonly Block leavesOakBirdnest = new(85,5, 0);
        public static readonly Block bones = new(86,6, 0);
        public static readonly Block cobwebs = new(87,7, 0);

        // Row 1
        public static readonly Block foliage = new(88,0, 1);
        public static readonly Block cactiCenter = new(89,1, 1);
        public static readonly Block gemblockEmerald = new(90,2, 1);
        public static readonly Block gemblockSaphire = new(91,3, 1);
        public static readonly Block gemblockRuby = new(92,4, 1);
        public static readonly Block thatchIron = new(93,5, 1);
        public static readonly Block leavesSpruce = new(94,6, 1);
        public static readonly Block leavesAcacia = new(95,7, 1);

        // Row 2
        public static readonly Block liquidAcid = new(96,0, 2);
        public static readonly Block leavesPine = new(97,1, 2);
        public static readonly Block leavesTropical = new(98,2, 2);
        public static readonly Block liquidWater = new(99,3, 2);
        public static readonly Block ice = new(100,4, 2);
        public static readonly Block liquidWaterLillypad = new(101,5, 2);
        public static readonly Block bushIvy = new(102,6, 2);
        public static readonly Block bushBlueberries = new(103,7, 2);

        // Row 3
        public static readonly Block liquidMagma = new(104,0, 3);
        public static readonly Block thatchOvergrown = new(105,1, 3);
        public static readonly Block leavesJungleThick = new(106,2, 3);
        public static readonly Block sapplingRedwood = new(107,3, 3);
        public static readonly Block thatch = new(108,4, 3);
        public static readonly Block flowerYellow = new(109,5, 3);
        public static readonly Block flowerRed = new(110,6, 3);
        public static readonly Block flowerWhite = new(111,7, 3);

        // Row 4
        public static readonly Block glass = new(112,0, 4);
        public static readonly Block leavesAcaciaBirdnest = new(113,1, 4);
        public static readonly Block leavesJungleSparse = new(114,2, 4);
        public static readonly Block leavesJungle = new(115,3, 4);
        public static readonly Block leavesJungleFlowering = new(116,4, 4);
        public static readonly Block leavesBirch = new(117,5, 4);
        public static readonly Block cloud = new(118,6, 4);
        public static readonly Block cloudStormy = new(119,7, 4);

        // Row 5
        public static readonly Block glassPane = new(120,0, 5);
        public static readonly Block demonWeeds = new(121,1, 5);
        public static readonly Block gemblockCoal = new(122,2, 5);
        public static readonly Block beehive = new(123,3, 5);
        public static readonly Block shrubTwiggy = new(124,4, 5);
        public static readonly Block shrubFan = new(125,5, 5);
        public static readonly Block shrub = new(126,6, 5);
        public static readonly Block candle = new(127,7, 5);

        // Row 6
        public static readonly Block wheat1 = new(128,0, 6);
        public static readonly Block wheat2 = new(129,1, 6);
        public static readonly Block wheat3 = new(130,2, 6);
        public static readonly Block wheat4 = new(131,3, 6);
        public static readonly Block shrubCacti = new(132,4, 6);
        public static readonly Block sapplingTreeApples = new(133,5, 6);
        public static readonly Block sapplingTreeTropical = new(134,6, 6);
        public static readonly Block grass1 = new(135,7, 6);

        // Row 7
        public static readonly Block sapplingTreeAcaciaLong = new(136,0, 7);
        public static readonly Block sapplingTreeSwamp = new(137,1, 7);
        public static readonly Block sapplingTreePalm = new(138,2, 7);
        public static readonly Block sapplingTreeBirch = new(139,3, 7);
        public static readonly Block sapplingTreeAcacia = new(140,4, 7);
        public static readonly Block sapplingTreeTwiggy = new(141,5, 7);
        public static readonly Block sapplingTreePine = new(142,6, 7);
        public static readonly Block sapplingTreeOak = new(143,7, 7);

        // Row 8
        public static readonly Block alienFlowers = new(144,0, 8);
        public static readonly Block cloudDroplets = new(145,1, 8);
        public static readonly Block alienGrass = new(146,2, 8);
        public static readonly Block alienShrubFlower = new(147,3, 8);
        public static readonly Block grass2 = new(148,4, 8);
        public static readonly Block moss = new(149,5, 8);
        public static readonly Block alienMoss = new(150,6, 8);
        public static readonly Block vines1 = new(151,7, 8);

        // Row 9
        public static readonly Block chain = new(152,0, 9);
        public static readonly Block vines2 = new(153,1, 9);
        public static readonly Block grass3 = new(154,2, 9);
        public static readonly Block coral = new(155,3, 9);

        public TileIDs()
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

    public readonly struct Block(int X, int Y, int Z)
    {
        // public int ID { get; } = ID;
        // public TexCoord TexCoordStart { get; } = TexCoordStart;

        public Vector3 BlockVec3 { get; } = new(X,Y,Z);
    }

    public readonly struct TexCoord(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
    }
}