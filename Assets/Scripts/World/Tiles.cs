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
        public static readonly int air = 512;
        public static readonly int grassTop = 0;
        public static readonly int grassSide = 1;
        public static readonly int sandSoft = 2;
        public static readonly int sandRough = 3;
        public static readonly int rock = 4;
        public static readonly int rockOreGold = 5;
        public static readonly int coconut = 6;
        public static readonly int snow = 7;

        // Row 1
        public static readonly int dirt = 8;
        public static readonly int logSideNormal = 9;
        public static readonly int logTop = 10;
        public static readonly int logSideBirch = 11;
        public static readonly int rockOreSaphire = 12;
        public static readonly int rockOreEmerald = 13;
        public static readonly int logSidePalm = 14;
        public static readonly int oreBlockIron = 15;

        // Row 2
        public static readonly int rockQuartz = 16;
        public static readonly int craftingStationFurnaceFront = 17;
        public static readonly int rockOreRuby = 18;
        public static readonly int rockMagma = 19;
        public static readonly int bricksSnow = 20;
        public static readonly int rockOreCoal = 21;
        public static readonly int planksNormal = 22;
        public static readonly int bricksRed = 23;

        // Row 3
        public static readonly int rockMagmaHot = 24;
        public static readonly int craftingStationCauldronFront = 25;
        public static readonly int bricksMagma = 26;
        public static readonly int tntSide = 27;
        public static readonly int tntTop = 28;
        public static readonly int lampSide = 29;
        public static readonly int craftingStationWorkbenchBottom = 30;
        public static readonly int rockOreSulfur = 31;

        // Row 4
        public static readonly int sandGravel = 32;
        public static readonly int rockOreIron = 33;
        public static readonly int bricksStones = 34;
        public static readonly int bricksStonesMossy = 35;
        public static readonly int bookcase = 36;
        public static readonly int oreBlockGold = 37;
        public static readonly int bricksSandstone = 38;
        public static readonly int rockOreUnobtainium = 39;

        // Row 5
        public static readonly int rockOreKlorium = 40;
        public static readonly int rockOreCryptonite = 41;
        public static readonly int craftingStationPaintingTableTop = 42;
        public static readonly int pumkinFront = 43;
        public static readonly int pumkintop = 44;
        public static readonly int pumkinSide = 45;
        public static readonly int bricksForgottenStoneMossy = 46;
        public static readonly int bricksForgottenStoneRune = 47;

        // Row 6
        public static readonly int bricksForgottenStone = 48;
        public static readonly int rockSmooth = 49;
        public static readonly int mobSpawner = 50;
        public static readonly int sponge = 51;
        public static readonly int carpet = 52;
        public static readonly int rockPath = 53;
        public static readonly int oreBlockPlating = 54;
        public static readonly int craftingStationWorkbenchTop = 55;

        // Row 7
        public static readonly int craftingStationWorkbenchSide = 56;
        public static readonly int craftingStationAnvil = 57;
        public static readonly int planksWaxed = 58;
        public static readonly int melonSide = 59;
        public static readonly int melonTop = 60;
        public static readonly int planksBirch = 61;
        public static readonly int planksPalm = 62;
        public static readonly int electricitySource = 63;

        // Row 8
        public static readonly int rockQuartzSoft = 64;
        public static readonly int rockQuartzSmooth = 65;
        public static readonly int magicStone = 66; // No idea wtf this is supposed to be in the texture so i just set it to this lol - Zephyros1938
        public static readonly int magicStoneSide = 67;
        public static readonly int magicStoneTop = 68;
        public static readonly int magicMoss = 69;
        public static readonly int magicGold = 70;
        public static readonly int pillarBase = 71;

        // Row 9
        public static readonly int pillarMid = 72;
        public static readonly int pillarTop = 73;
        public static readonly int lampTop = 74;
        public static readonly int alienMoss1 = 75; // I also have no idea what this is supposed to be either help - Zephyros1938
        public static readonly int alienMoss2 = 76;
        public static readonly int alienMoss3 = 77;
        public static readonly int alienMoss4 = 78;
        public static readonly int alienMoss5 = 79;

        // Transparent Blocks
        // Row 0
        public static readonly int leavesOak = 80;
        public static readonly int cactiSide = 81;
        public static readonly int bushBerry = 82;
        public static readonly int thatchFlowers = 83;
        public static readonly int leavesOakApples = 84;
        public static readonly int leavesOakBirdnest = 85;
        public static readonly int bones = 86;
        public static readonly int cobwebs = 87;

        // Row 1
        public static readonly int foliage = 88;
        public static readonly int cactiCenter = 89;
        public static readonly int gemblockEmerald = 90;
        public static readonly int gemblockSaphire = 91;
        public static readonly int gemblockRuby = 92;
        public static readonly int thatchIron = 93;
        public static readonly int leavesSpruce = 94;
        public static readonly int leavesAcacia = 95;

        // Row 2
        public static readonly int liquidAcid = 96;
        public static readonly int leavesPine = 97;
        public static readonly int leavesTropical = 98;
        public static readonly int liquidWater = 99;
        public static readonly int ice = 100;
        public static readonly int liquidWaterLillypad = 101;
        public static readonly int bushIvy = 102;
        public static readonly int bushBlueberries = 103;

        // Row 3
        public static readonly int liquidMagma = 104;
        public static readonly int thatchOvergrown = 105;
        public static readonly int leavesJungleThick = 106;
        public static readonly int sapplingRedwood = 107;
        public static readonly int thatch = 108;
        public static readonly int flowerYellow = 109;
        public static readonly int flowerRed = 110;
        public static readonly int flowerWhite = 111;

        // Row 4
        public static readonly int glass = 112;
        public static readonly int leavesAcaciaBirdnest = 113;
        public static readonly int leavesJungleSparse = 114;
        public static readonly int leavesJungle = 115;
        public static readonly int leavesJungleFlowering = 116;
        public static readonly int leavesBirch = 117;
        public static readonly int cloud = 118;
        public static readonly int cloudStormy = 119;

        // Row 5
        public static readonly int glassPane = 120;
        public static readonly int demonWeeds = 121;
        public static readonly int gemblockCoal = 122;
        public static readonly int beehive = 123;
        public static readonly int shrubTwiggy = 124;
        public static readonly int shrubFan = 125;
        public static readonly int shrub = 126;
        public static readonly int candle = 127;

        // Row 6
        public static readonly int wheat1 = 128;
        public static readonly int wheat2 = 129;
        public static readonly int wheat3 = 130;
        public static readonly int wheat4 = 131;
        public static readonly int shrubCacti = 132;
        public static readonly int sapplingTreeApples = 133;
        public static readonly int sapplingTreeTropical = 134;
        public static readonly int grass1 = 135;

        // Row 7
        public static readonly int sapplingTreeAcaciaLong = 136;
        public static readonly int sapplingTreeSwamp = 137;
        public static readonly int sapplingTreePalm = 138;
        public static readonly int sapplingTreeBirch = 139;
        public static readonly int sapplingTreeAcacia = 140;
        public static readonly int sapplingTreeTwiggy = 141;
        public static readonly int sapplingTreePine = 142;
        public static readonly int sapplingTreeOak = 143;

        // Row 8
        public static readonly int alienFlowers = 144;
        public static readonly int cloudDroplets = 145;
        public static readonly int alienGrass = 146;
        public static readonly int alienShrubFlower = 147;
        public static readonly int grass2 = 148;
        public static readonly int moss = 149;
        public static readonly int alienMoss = 150;
        public static readonly int vines1 = 151;

        // Row 9
        public static readonly int chain = 152;
        public static readonly int vines2 = 153;
        public static readonly int grass3 = 154;
        public static readonly int coral = 155;

        public TileIDs()
        {
        }
    }
}