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
        public static readonly Block grassTop = new(0f, 0f, 0f);
        public static readonly Block grassSide = new(1f, 1f, 0f);
        public static readonly Block sandSoft = new(2f, 2f, 0f);
        public static readonly Block sandRough = new(3f, 3f, 0f);
        public static readonly Block rock = new(4f, 4f, 0f);
        public static readonly Block rockOreGold = new(5f, 5f, 0f);
        public static readonly Block coconut = new(6f, 6f, 0f);
        public static readonly Block snow = new(7f, 7f, 0f);

        // Row 1
        public static readonly Block dirt = new(8f, 0f, 1f);
        public static readonly Block logSideNormal = new(9, 1, 1);
        public static readonly Block logTop = new(10f, 2f, 1f);
        public static readonly Block logSideBirch = new(11f, 3f, 1f);
        public static readonly Block rockOreSaphire = new(12f, 4f, 1f);
        public static readonly Block rockOreEmerald = new(13f, 5f, 1f);
        public static readonly Block logSidePalm = new(14f, 6f, 1f);
        public static readonly Block oreBlockIron = new(15f, 7f, 1f);

        // Row 2
        public static readonly Block rockQuartz = new(16f, 0f, 2f);
        public static readonly Block craftingStationFurnaceFront = new(17f, 1f, 2f);
        public static readonly Block rockOreRuby = new(18f, 2f, 2f);
        public static readonly Block rockMagma = new(19f, 3f, 2f);
        public static readonly Block bricksSnow = new(20f, 4f, 2f);
        public static readonly Block rockOreCoal = new(21f, 5f, 2f);
        public static readonly Block planksNormal = new(22f, 6f, 2f);
        public static readonly Block bricksRed = new(23f, 7f, 2f);

        // Row 3
        public static readonly Block rockMagmaHot = new(24f, 0f, 3f);
        public static readonly Block craftingStationCauldronFront = new(25f, 1f, 3f);
        public static readonly Block bricksMagma = new(26f, 2f, 3f);
        public static readonly Block tntSide = new(27f, 3f, 3f);
        public static readonly Block tntTop = new(28f, 4f, 3f);
        public static readonly Block lampSide = new(29f, 5f, 3f);
        public static readonly Block craftingStationWorkbenchBottom = new(30f, 6f, 3f);
        public static readonly Block rockOreSulfur = new(31f, 7f, 3f);

        // Row 4
        public static readonly Block sandGravel = new(32f, 0f, 4f);
        public static readonly Block rockOreIron = new(33f, 1f, 4f);
        public static readonly Block bricksStones = new(34f, 2f, 4f);
        public static readonly Block bricksStonesMossy = new(35f, 3f, 4f);
        public static readonly Block bookcase = new(36f, 4f, 4f);
        public static readonly Block oreBlockGold = new(37f, 5f, 4f);
        public static readonly Block bricksSandstone = new(38f, 6f, 4f);
        public static readonly Block rockOreUnobtainium = new(39f, 7f, 4f);

        // Row 5
        public static readonly Block rockOreKlorium = new(40f, 0f, 5f);
        public static readonly Block rockOreCryptonite = new(41f, 1f, 5f);
        public static readonly Block craftingStationPaintingTableTop = new(42f, 2f, 5f);
        public static readonly Block pumkinFront = new(43f, 3f, 5f);
        public static readonly Block pumkinTop = new(44f, 4f, 5f);
        public static readonly Block pumkinSide = new(45f, 5f, 5f);
        public static readonly Block bricksForgottenStoneMossy = new(46f, 6f, 5f);
        public static readonly Block bricksForgottenStoneRune = new(47f, 7f, 5f);

        // Row 6
        public static readonly Block bricksForgottenStone = new(48f, 0f, 6f);
        public static readonly Block rockSmooth = new(49f, 1f, 6f);
        public static readonly Block mobSpawner = new(50f, 2f, 6f);
        public static readonly Block sponge = new(51f, 3f, 6f);
        public static readonly Block carpet = new(52f, 4f, 6f);
        public static readonly Block rockPath = new(53f, 5f, 6f);
        public static readonly Block oreBlockPlating = new(54f, 6f, 6f);
        public static readonly Block craftingStationWorkbenchTop = new(55f, 7f, 6f);

        // Row 7
        public static readonly Block craftingStationWorkbenchSide = new(56f, 0f, 7f);
        public static readonly Block craftingStationAnvil = new(57f, 1f, 7f);
        public static readonly Block planksWaxed = new(58f, 2f, 7f);
        public static readonly Block melonSide = new(59f, 3f, 7f);
        public static readonly Block melonTop = new(60f, 4f, 7f);
        public static readonly Block planksBirch = new(61f, 5f, 7f);
        public static readonly Block planksPalm = new(62f, 6f, 7f);
        public static readonly Block electricitySource = new(63f, 7f, 7f);

        // Row 8
        public static readonly Block rockQuartzSoft = new(64f, 0f, 8f);
        public static readonly Block rockQuartzSmooth = new(65f, 1f, 8f);
        public static readonly Block magicStone = new(66f, 2f, 8f); // No idea wtf this is supposed to be in the texture so i just set it to this lol - Zephyros1938
        public static readonly Block magicStoneSide = new(67f, 3f, 8f);
        public static readonly Block magicStoneTop = new(68f, 4f, 8f);
        public static readonly Block magicMoss = new(69f, 5f, 8f);
        public static readonly Block magicGold = new(70f, 6f, 8f);
        public static readonly Block pillarBase = new(71f, 7f, 8f);

        // Row 9
        public static readonly Block pillarMid = new(72f, 0f, 9f);
        public static readonly Block pillarTop = new(73f, 1f, 9f);
        public static readonly Block lampTop = new(74f, 2f, 9f);
        public static readonly Block alienMoss1 = new(75f, 3f, 9f); // I also have no idea what this is supposed to be either help - Zephyros1938
        public static readonly Block alienMoss2 = new(76f, 4f, 9f);
        public static readonly Block alienMoss3 = new(77f, 5f, 9f);
        public static readonly Block alienMoss4 = new(78f, 6f, 9f);
        public static readonly Block alienMoss5 = new(79f, 7f, 9f);

        // Transparent Blocks
        // Row 0
        public static readonly Block leavesOak = new(80f, 0f, 0f);
        public static readonly Block cactiSide = new(81f, 1f, 0f);
        public static readonly Block bushBerry = new(82f, 2f, 0f);
        public static readonly Block thatchFlowers = new(83f, 3f, 0f);
        public static readonly Block leavesOakApples = new(84f, 4f, 0f);
        public static readonly Block leavesOakBirdnest = new(85f, 5f, 0f);
        public static readonly Block bones = new(86f, 6f, 0f);
        public static readonly Block cobwebs = new(87f, 7f, 0f);

        // Row 1
        public static readonly Block foliage = new(88f, 0f, 1f);
        public static readonly Block cactiCenter = new(89f, 1f, 1f);
        public static readonly Block gemblockEmerald = new(90f, 2f, 1f);
        public static readonly Block gemblockSaphire = new(91f, 3f, 1f);
        public static readonly Block gemblockRuby = new(92f, 4f, 1f);
        public static readonly Block thatchIron = new(93f, 5f, 1f);
        public static readonly Block leavesSpruce = new(94f, 6f, 1f);
        public static readonly Block leavesAcacia = new(95f, 7f, 1f);

        // Row 2
        public static readonly Block liquidAcid = new(96f, 0f, 2f);
        public static readonly Block leavesPine = new(97f, 1f, 2f);
        public static readonly Block leavesTropical = new(98f, 2f, 2f);
        public static readonly Block liquidWater = new(99f, 3f, 2f);
        public static readonly Block ice = new(100f, 4f, 2f);
        public static readonly Block liquidWaterLillypad = new(101f, 5f, 2f);
        public static readonly Block bushIvy = new(102f, 6f, 2f);
        public static readonly Block bushBlueberries = new(103f, 7f, 2f);

        // Row 3
        public static readonly Block liquidMagma = new(104f, 0f, 3f);
        public static readonly Block thatchOvergrown = new(105f, 1f, 3f);
        public static readonly Block leavesJungleThick = new(106f, 2f, 3f);
        public static readonly Block sapplingRedwood = new(107f, 3f, 3f);
        public static readonly Block thatch = new(108f, 4f, 3f);
        public static readonly Block flowerYellow = new(109f, 5f, 3f);
        public static readonly Block flowerRed = new(110f, 6f, 3f);
        public static readonly Block flowerWhite = new(111f, 7f, 3f);

        // Row 4
        public static readonly Block glass = new(112f, 0f, 4f);
        public static readonly Block leavesAcaciaBirdnest = new(113f, 1f, 4f);
        public static readonly Block leavesJungleSparse = new(114f, 2f, 4f);
        public static readonly Block leavesJungle = new(115f, 3f, 4f);
        public static readonly Block leavesJungleFlowering = new(116f, 4f, 4f);
        public static readonly Block leavesBirch = new(117f, 5f, 4f);
        public static readonly Block cloud = new(118f, 6f, 4f);
        public static readonly Block cloudStormy = new(119f, 7f, 4f);

        // Row 5
        public static readonly Block glassPane = new(120f, 0f, 5f);
        public static readonly Block demonWeeds = new(121f, 1f, 5f);
        public static readonly Block gemblockCoal = new(122f, 2f, 5f);
        public static readonly Block beehive = new(123f, 3f, 5f);
        public static readonly Block shrubTwiggy = new(124f, 4f, 5f);
        public static readonly Block shrubFan = new(125f, 5f, 5f);
        public static readonly Block shrub = new(126f, 6f, 5f);
        public static readonly Block candle = new(127f, 7f, 5f);

        // Row 6
        public static readonly Block wheat1 = new(128f, 0f, 6f);
        public static readonly Block wheat2 = new(129f, 1f, 6f);
        public static readonly Block wheat3 = new(130f, 2f, 6f);
        public static readonly Block wheat4 = new(131f, 3f, 6f);
        public static readonly Block shrubCacti = new(132f, 4f, 6f);
        public static readonly Block sapplingTreeApples = new(133f, 5f, 6f);
        public static readonly Block sapplingTreeTropical = new(134f, 6f, 6f);
        public static readonly Block grass1 = new(135f, 7f, 6f);

        // Row 7
        public static readonly Block sapplingTreeAcaciaLong = new(136f, 0f, 7f);
        public static readonly Block sapplingTreeSwamp = new(137f, 1f, 7f);
        public static readonly Block sapplingTreePalm = new(138f, 2f, 7f);
        public static readonly Block sapplingTreeBirch = new(139f, 3f, 7f);
        public static readonly Block sapplingTreeAcacia = new(140f, 4f, 7f);
        public static readonly Block sapplingTreeTwiggy = new(141f, 5f, 7f);
        public static readonly Block sapplingTreePine = new(142f, 6f, 7f);
        public static readonly Block sapplingTreeOak = new(143f, 7f, 7f);

        // Row 8
        public static readonly Block alienFlowers = new(144f, 0f, 8f);
        public static readonly Block cloudDroplets = new(145f, 1f, 8f);
        public static readonly Block alienGrass = new(146f, 2f, 8f);
        public static readonly Block alienShrubFlower = new(147f, 3f, 8f);
        public static readonly Block grass2 = new(148f, 4f, 8f);
        public static readonly Block moss = new(149f, 5f, 8f);
        public static readonly Block alienMoss = new(150f, 6f, 8f);
        public static readonly Block vines1 = new(151f, 7f, 8f);

        // Row 9
        public static readonly Block chain = new(152f, 0f, 9f);
        public static readonly Block vines2 = new(153f, 1f, 9f);
        public static readonly Block grass3 = new(154f, 2f, 9f);
        public static readonly Block coral = new(155f, 3f, 9f);

        public TileIDs()
        {
        }
    }
}