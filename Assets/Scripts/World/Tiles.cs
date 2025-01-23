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
        public static readonly uint grassTop = 0;
        public static readonly uint grassSide = 1;
        public static readonly uint sandSoft = 2;
        public static readonly uint sandRough = 3;
        public static readonly uint rock = 4;
        public static readonly uint rockOreGold = 5;
        public static readonly uint coconut = 6;
        public static readonly uint snow = 7;

        // Row 1
        public static readonly uint dirt = 8;
        public static readonly uint logSideNormal = 9;
        public static readonly uint logTop = 10;
        public static readonly uint logSideBirch = 11;
        public static readonly uint rockOreSaphire = 12;
        public static readonly uint rockOreEmerald = 13;
        public static readonly uint logSidePalm = 14;
        public static readonly uint oreBlockIron = 15;

        // Row 2
        public static readonly uint rockQuartz = 16;
        public static readonly uint craftingStationFurnaceFront = 17;
        public static readonly uint rockOreRuby = 18;
        public static readonly uint rockMagma = 19;
        public static readonly uint bricksSnow = 20;
        public static readonly uint rockOreCoal = 21;
        public static readonly uint planksNormal = 22;
        public static readonly uint bricksRed = 23;

        // Row 3
        public static readonly uint rockMagmaHot = 24;
        public static readonly uint craftingStationCauldronFront = 25;
        public static readonly uint bricksMagma = 26;
        public static readonly uint tntSide = 27;
        public static readonly uint tntTop = 28;
        public static readonly uint lampSide = 29;
        public static readonly uint craftingStationWorkbenchBottom = 30;
        public static readonly uint rockOreSulfur = 31;

        // Row 4
        public static readonly uint sandGravel = 32;
        public static readonly uint rockOreIron = 33;
        public static readonly uint bricksStones = 34;
        public static readonly uint bricksStonesMossy = 35;
        public static readonly uint bookcase = 36;
        public static readonly uint oreBlockGold = 37;
        public static readonly uint bricksSandstone = 38;
        public static readonly uint rockOreUnobtainium = 39;

        // Row 5
        public static readonly uint rockOreKlorium = 40;
        public static readonly uint rockOreCryptonite = 41;
        public static readonly uint craftingStationPaintingTableTop = 42;
        public static readonly uint pumkinFront = 43;
        public static readonly uint pumkinTop = 44;
        public static readonly uint pumkinSide = 45;
        public static readonly uint bricksForgottenStoneMossy = 46;
        public static readonly uint bricksForgottenStoneRune = 47;

        // Row 6
        public static readonly uint bricksForgottenStone = 48;
        public static readonly uint rockSmooth = 49;
        public static readonly uint mobSpawner = 50;
        public static readonly uint sponge = 51;
        public static readonly uint carpet = 52;
        public static readonly uint rockPath = 53;
        public static readonly uint oreBlockPlating = 54;
        public static readonly uint craftingStationWorkbenchTop = 55;

        // Row 7
        public static readonly uint craftingStationWorkbenchSide = 56;
        public static readonly uint craftingStationAnvil = 57;
        public static readonly uint planksWaxed = 58;
        public static readonly uint melonSide = 59;
        public static readonly uint melonTop = 60;
        public static readonly uint planksBirch = 61;
        public static readonly uint planksPalm = 62;
        public static readonly uint electricitySource = 63;

        // Row 8
        public static readonly uint rockQuartzSoft = 64;
        public static readonly uint rockQuartzSmooth = 65;
        public static readonly uint magicStone = 66; // No idea wtf this is supposed to be in the texture so i just set it to this lol - Zephyros1938
        public static readonly uint magicStoneSide = 67;
        public static readonly uint magicStoneTop = 68;
        public static readonly uint magicMoss = 69;
        public static readonly uint magicGold = 70;
        public static readonly uint pillarBase = 71;

        // Row 9
        public static readonly uint pillarMid = 72;
        public static readonly uint pillarTop = 73;
        public static readonly uint lampTop = 74;
        public static readonly uint alienMoss1 = 75; // I also have no idea what this is supposed to be either help - Zephyros1938
        public static readonly uint alienMoss2 = 76;
        public static readonly uint alienMoss3 = 77;
        public static readonly uint alienMoss4 = 78;
        public static readonly uint alienMoss5 = 79;

        // Transparent Blocks
        // Row 0
        public static readonly uint leavesOak = 80;
        public static readonly uint cactiSide = 81;
        public static readonly uint bushBerry = 82;
        public static readonly uint thatchFlowers = 83;
        public static readonly uint leavesOakApples = 84;
        public static readonly uint leavesOakBirdnest = 85;
        public static readonly uint bones = 86;
        public static readonly uint cobwebs = 87;

        // Row 1
        public static readonly uint foliage = 88;
        public static readonly uint cactiCenter = 89;
        public static readonly uint gemblockEmerald = 90;
        public static readonly uint gemblockSaphire = 91;
        public static readonly uint gemblockRuby = 92;
        public static readonly uint thatchIron = 93;
        public static readonly uint leavesSpruce = 94;
        public static readonly uint leavesAcacia = 95;

        // Row 2
        public static readonly uint liquidAcid = 96;
        public static readonly uint leavesPine = 97;
        public static readonly uint leavesTropical = 98;
        public static readonly uint liquidWater = 99;
        public static readonly uint ice = 100;
        public static readonly uint liquidWaterLillypad = 101;
        public static readonly uint bushIvy = 102;
        public static readonly uint bushBlueberries = 103;

        // Row 3
        public static readonly uint liquidMagma = 104;
        public static readonly uint thatchOvergrown = 105;
        public static readonly uint leavesJungleThick = 106;
        public static readonly uint sapplingRedwood = 107;
        public static readonly uint thatch = 108;
        public static readonly uint flowerYellow = 109;
        public static readonly uint flowerRed = 110;
        public static readonly uint flowerWhite = 111;

        // Row 4
        public static readonly uint glass = 112;
        public static readonly uint leavesAcaciaBirdnest = 113;
        public static readonly uint leavesJungleSparse = 114;
        public static readonly uint leavesJungle = 115;
        public static readonly uint leavesJungleFlowering = 116;
        public static readonly uint leavesBirch = 117;
        public static readonly uint cloud = 118;
        public static readonly uint cloudStormy = 119;

        // Row 5
        public static readonly uint glassPane = 120;
        public static readonly uint demonWeeds = 121;
        public static readonly uint gemblockCoal = 122;
        public static readonly uint beehive = 123;
        public static readonly uint shrubTwiggy = 124;
        public static readonly uint shrubFan = 125;
        public static readonly uint shrub = 126;
        public static readonly uint candle = 127;

        // Row 6
        public static readonly uint wheat1 = 128;
        public static readonly uint wheat2 = 129;
        public static readonly uint wheat3 = 130;
        public static readonly uint wheat4 = 131;
        public static readonly uint shrubCacti = 132;
        public static readonly uint sapplingTreeApples = 133;
        public static readonly uint sapplingTreeTropical = 134;
        public static readonly uint grass1 = 135;

        // Row 7
        public static readonly uint sapplingTreeAcaciaLong = 136;
        public static readonly uint sapplingTreeSwamp = 137;
        public static readonly uint sapplingTreePalm = 138;
        public static readonly uint sapplingTreeBirch = 139;
        public static readonly uint sapplingTreeAcacia = 140;
        public static readonly uint sapplingTreeTwiggy = 141;
        public static readonly uint sapplingTreePine = 142;
        public static readonly uint sapplingTreeOak = 143;

        // Row 8
        public static readonly uint alienFlowers = 144;
        public static readonly uint cloudDroplets = 145;
        public static readonly uint alienGrass = 146;
        public static readonly uint alienShrubFlower = 147;
        public static readonly uint grass2 = 148;
        public static readonly uint moss = 149;
        public static readonly uint alienMoss = 150;
        public static readonly uint vines1 = 151;

        // Row 9
        public static readonly uint chain = 152;
        public static readonly uint vines2 = 153;
        public static readonly uint grass3 = 154;
        public static readonly uint coral = 155;

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