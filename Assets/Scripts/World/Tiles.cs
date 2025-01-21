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
    /// Each block is structured as follows: ID, bottom left x, bottom left y.
    /// 
    /// Reference /Assets/Images/textures.png for the solid texture map.
    /// Reference /Assets/Images/textures_tr.png for the transparent texture map.
    /// 
    /// </summary>
    public readonly struct TileIDs
    {
        // Nontransparent Blocks
        // Row 0
        public static readonly BlockFace grassTop = new(0f, 0f, 0f);
        public static readonly BlockFace grassSide = new(1f, 1f, 0f);
        public static readonly BlockFace sandSoft = new(2f, 2f, 0f);
        public static readonly BlockFace sandRough = new(3f, 3f, 0f);
        public static readonly BlockFace rock = new(4f, 4f, 0f);
        public static readonly BlockFace rockOreGold = new(5f, 5f, 0f);
        public static readonly BlockFace coconut = new(6f, 6f, 0f);
        public static readonly BlockFace snow = new(7f, 7f, 0f);

        // Row 1
        public static readonly BlockFace dirt = new(8f, 0f, 1f);
        public static readonly BlockFace logSideNormal = new(9, 1, 1);
        public static readonly BlockFace logTop = new(10f, 2f, 1f);
        public static readonly BlockFace logSideBirch = new(11f, 3f, 1f);
        public static readonly BlockFace rockOreSaphire = new(12f, 4f, 1f);
        public static readonly BlockFace rockOreEmerald = new(13f, 5f, 1f);
        public static readonly BlockFace logSidePalm = new(14f, 6f, 1f);
        public static readonly BlockFace oreBlockIron = new(15f, 7f, 1f);

        // Row 2
        public static readonly BlockFace rockQuartz = new(16f, 0f, 2f);
        public static readonly BlockFace craftingStationFurnaceFront = new(17f, 1f, 2f);
        public static readonly BlockFace rockOreRuby = new(18f, 2f, 2f);
        public static readonly BlockFace rockMagma = new(19f, 3f, 2f);
        public static readonly BlockFace bricksSnow = new(20f, 4f, 2f);
        public static readonly BlockFace rockOreCoal = new(21f, 5f, 2f);
        public static readonly BlockFace planksNormal = new(22f, 6f, 2f);
        public static readonly BlockFace bricksRed = new(23f, 7f, 2f);

        // Row 3
        public static readonly BlockFace rockMagmaHot = new(24f, 0f, 3f);
        public static readonly BlockFace craftingStationCauldronFront = new(25f, 1f, 3f);
        public static readonly BlockFace bricksMagma = new(26f, 2f, 3f);
        public static readonly BlockFace tntSide = new(27f, 3f, 3f);
        public static readonly BlockFace tntTop = new(28f, 4f, 3f);
        public static readonly BlockFace lampSide = new(29f, 5f, 3f);
        public static readonly BlockFace craftingStationWorkbenchBottom = new(30f, 6f, 3f);
        public static readonly BlockFace rockOreSulfur = new(31f, 7f, 3f);

        // Row 4
        public static readonly BlockFace sandGravel = new(32f, 0f, 4f);
        public static readonly BlockFace rockOreIron = new(33f, 1f, 4f);
        public static readonly BlockFace bricksStones = new(34f, 2f, 4f);
        public static readonly BlockFace bricksStonesMossy = new(35f, 3f, 4f);
        public static readonly BlockFace bookcase = new(36f, 4f, 4f);
        public static readonly BlockFace oreBlockGold = new(37f, 5f, 4f);
        public static readonly BlockFace bricksSandstone = new(38f, 6f, 4f);
        public static readonly BlockFace rockOreUnobtainium = new(39f, 7f, 4f);

        // Row 5
        public static readonly BlockFace rockOreKlorium = new(40f, 0f, 5f);
        public static readonly BlockFace rockOreCryptonite = new(41f, 1f, 5f);
        public static readonly BlockFace craftingStationPaintingTableTop = new(42f, 2f, 5f);
        public static readonly BlockFace pumkinFront = new(43f, 3f, 5f);
        public static readonly BlockFace pumkinTop = new(44f, 4f, 5f);
        public static readonly BlockFace pumkinSide = new(45f, 5f, 5f);
        public static readonly BlockFace bricksForgottenStoneMossy = new(46f, 6f, 5f);
        public static readonly BlockFace bricksForgottenStoneRune = new(47f, 7f, 5f);

        // Row 6
        public static readonly BlockFace bricksForgottenStone = new(48f, 0f, 6f);
        public static readonly BlockFace rockSmooth = new(49f, 1f, 6f);
        public static readonly BlockFace mobSpawner = new(50f, 2f, 6f);
        public static readonly BlockFace sponge = new(51f, 3f, 6f);
        public static readonly BlockFace carpet = new(52f, 4f, 6f);
        public static readonly BlockFace rockPath = new(53f, 5f, 6f);
        public static readonly BlockFace oreBlockPlating = new(54f, 6f, 6f);
        public static readonly BlockFace craftingStationWorkbenchTop = new(55f, 7f, 6f);

        // Row 7
        public static readonly BlockFace craftingStationWorkbenchSide = new(56f, 0f, 7f);
        public static readonly BlockFace craftingStationAnvil = new(57f, 1f, 7f);
        public static readonly BlockFace planksWaxed = new(58f, 2f, 7f);
        public static readonly BlockFace melonSide = new(59f, 3f, 7f);
        public static readonly BlockFace melonTop = new(60f, 4f, 7f);
        public static readonly BlockFace planksBirch = new(61f, 5f, 7f);
        public static readonly BlockFace planksPalm = new(62f, 6f, 7f);
        public static readonly BlockFace electricitySource = new(63f, 7f, 7f);

        // Row 8
        public static readonly BlockFace rockQuartzSoft = new(64f, 0f, 8f);
        public static readonly BlockFace rockQuartzSmooth = new(65f, 1f, 8f);
        public static readonly BlockFace magicStone = new(66f, 2f, 8f); // No idea wtf this is supposed to be in the texture so i just set it to this lol - Zephyros1938
        public static readonly BlockFace magicStoneSide = new(67f, 3f, 8f);
        public static readonly BlockFace magicStoneTop = new(68f, 4f, 8f);
        public static readonly BlockFace magicMoss = new(69f, 5f, 8f);
        public static readonly BlockFace magicGold = new(70f, 6f, 8f);
        public static readonly BlockFace pillarBase = new(71f, 7f, 8f);

        // Row 9
        public static readonly BlockFace pillarMid = new(72f, 0f, 9f);
        public static readonly BlockFace pillarTop = new(73f, 1f, 9f);
        public static readonly BlockFace lampTop = new(74f, 2f, 9f);
        public static readonly BlockFace alienMoss1 = new(75f, 3f, 9f); // I also have no idea what this is supposed to be either help - Zephyros1938
        public static readonly BlockFace alienMoss2 = new(76f, 4f, 9f);
        public static readonly BlockFace alienMoss3 = new(77f, 5f, 9f);
        public static readonly BlockFace alienMoss4 = new(78f, 6f, 9f);
        public static readonly BlockFace alienMoss5 = new(79f, 7f, 9f);

        // Transparent Blocks
        // Row 0
        public static readonly BlockFace leavesOak = new(80f, 0f, 0f);
        public static readonly BlockFace cactiSide = new(81f, 1f, 0f);
        public static readonly BlockFace bushBerry = new(82f, 2f, 0f);
        public static readonly BlockFace thatchFlowers = new(83f, 3f, 0f);
        public static readonly BlockFace leavesOakApples = new(84f, 4f, 0f);
        public static readonly BlockFace leavesOakBirdnest = new(85f, 5f, 0f);
        public static readonly BlockFace bones = new(86f, 6f, 0f);
        public static readonly BlockFace cobwebs = new(87f, 7f, 0f);

        // Row 1
        public static readonly BlockFace foliage = new(88f, 0f, 1f);
        public static readonly BlockFace cactiCenter = new(89f, 1f, 1f);
        public static readonly BlockFace gemblockEmerald = new(90f, 2f, 1f);
        public static readonly BlockFace gemblockSaphire = new(91f, 3f, 1f);
        public static readonly BlockFace gemblockRuby = new(92f, 4f, 1f);
        public static readonly BlockFace thatchIron = new(93f, 5f, 1f);
        public static readonly BlockFace leavesSpruce = new(94f, 6f, 1f);
        public static readonly BlockFace leavesAcacia = new(95f, 7f, 1f);

        // Row 2
        public static readonly BlockFace liquidAcid = new(96f, 0f, 2f);
        public static readonly BlockFace leavesPine = new(97f, 1f, 2f);
        public static readonly BlockFace leavesTropical = new(98f, 2f, 2f);
        public static readonly BlockFace liquidWater = new(99f, 3f, 2f);
        public static readonly BlockFace ice = new(100f, 4f, 2f);
        public static readonly BlockFace liquidWaterLillypad = new(101f, 5f, 2f);
        public static readonly BlockFace bushIvy = new(102f, 6f, 2f);
        public static readonly BlockFace bushBlueberries = new(103f, 7f, 2f);

        // Row 3
        public static readonly BlockFace liquidMagma = new(104f, 0f, 3f);
        public static readonly BlockFace thatchOvergrown = new(105f, 1f, 3f);
        public static readonly BlockFace leavesJungleThick = new(106f, 2f, 3f);
        public static readonly BlockFace sapplingRedwood = new(107f, 3f, 3f);
        public static readonly BlockFace thatch = new(108f, 4f, 3f);
        public static readonly BlockFace flowerYellow = new(109f, 5f, 3f);
        public static readonly BlockFace flowerRed = new(110f, 6f, 3f);
        public static readonly BlockFace flowerWhite = new(111f, 7f, 3f);

        // Row 4
        public static readonly BlockFace glass = new(112f, 0f, 4f);
        public static readonly BlockFace leavesAcaciaBirdnest = new(113f, 1f, 4f);
        public static readonly BlockFace leavesJungleSparse = new(114f, 2f, 4f);
        public static readonly BlockFace leavesJungle = new(115f, 3f, 4f);
        public static readonly BlockFace leavesJungleFlowering = new(116f, 4f, 4f);
        public static readonly BlockFace leavesBirch = new(117f, 5f, 4f);
        public static readonly BlockFace cloud = new(118f, 6f, 4f);
        public static readonly BlockFace cloudStormy = new(119f, 7f, 4f);

        // Row 5
        public static readonly BlockFace glassPane = new(120f, 0f, 5f);
        public static readonly BlockFace demonWeeds = new(121f, 1f, 5f);
        public static readonly BlockFace gemblockCoal = new(122f, 2f, 5f);
        public static readonly BlockFace beehive = new(123f, 3f, 5f);
        public static readonly BlockFace shrubTwiggy = new(124f, 4f, 5f);
        public static readonly BlockFace shrubFan = new(125f, 5f, 5f);
        public static readonly BlockFace shrub = new(126f, 6f, 5f);
        public static readonly BlockFace candle = new(127f, 7f, 5f);

        // Row 6
        public static readonly BlockFace wheat1 = new(128f, 0f, 6f);
        public static readonly BlockFace wheat2 = new(129f, 1f, 6f);
        public static readonly BlockFace wheat3 = new(130f, 2f, 6f);
        public static readonly BlockFace wheat4 = new(131f, 3f, 6f);
        public static readonly BlockFace shrubCacti = new(132f, 4f, 6f);
        public static readonly BlockFace sapplingTreeApples = new(133f, 5f, 6f);
        public static readonly BlockFace sapplingTreeTropical = new(134f, 6f, 6f);
        public static readonly BlockFace grass1 = new(135f, 7f, 6f);

        // Row 7
        public static readonly BlockFace sapplingTreeAcaciaLong = new(136f, 0f, 7f);
        public static readonly BlockFace sapplingTreeSwamp = new(137f, 1f, 7f);
        public static readonly BlockFace sapplingTreePalm = new(138f, 2f, 7f);
        public static readonly BlockFace sapplingTreeBirch = new(139f, 3f, 7f);
        public static readonly BlockFace sapplingTreeAcacia = new(140f, 4f, 7f);
        public static readonly BlockFace sapplingTreeTwiggy = new(141f, 5f, 7f);
        public static readonly BlockFace sapplingTreePine = new(142f, 6f, 7f);
        public static readonly BlockFace sapplingTreeOak = new(143f, 7f, 7f);

        // Row 8
        public static readonly BlockFace alienFlowers = new(144f, 0f, 8f);
        public static readonly BlockFace cloudDroplets = new(145f, 1f, 8f);
        public static readonly BlockFace alienGrass = new(146f, 2f, 8f);
        public static readonly BlockFace alienShrubFlower = new(147f, 3f, 8f);
        public static readonly BlockFace grass2 = new(148f, 4f, 8f);
        public static readonly BlockFace moss = new(149f, 5f, 8f);
        public static readonly BlockFace alienMoss = new(150f, 6f, 8f);
        public static readonly BlockFace vines1 = new(151f, 7f, 8f);

        // Row 9
        public static readonly BlockFace chain = new(152f, 0f, 9f);
        public static readonly BlockFace vines2 = new(153f, 1f, 9f);
        public static readonly BlockFace grass3 = new(154f, 2f, 9f);
        public static readonly BlockFace coral = new(155f, 3f, 9f);

        public TileIDs()
        {
        }
    }
    public readonly struct BlockFaceDirections
    {
        public static readonly float[] Forward = 
        [
            1f,1f,1f,
            1f,-1f,1f,
            -1f,1f,1f,
            1f,-1f,1f,
            -1f,-1f,1f,
            -1f,1f,1f
        ];
    }
}