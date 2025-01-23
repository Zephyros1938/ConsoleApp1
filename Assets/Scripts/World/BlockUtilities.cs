using OpenTK.Mathematics;

namespace ConsoleApp1.World
{
    public class BlockConstants
    {


        public static readonly float cubeSize = 0.5f;
        public static readonly Vector3[] offsets =
        [
            new( 1.0f,  1.0f,  1.0f), //+++ 0
            new( 1.0f,  1.0f, -1.0f), //++- 1
            new( 1.0f, -1.0f,  1.0f), //+-+ 2
            new( 1.0f, -1.0f, -1.0f), //+-- 3
            new(-1.0f,  1.0f,  1.0f), //-++ 4
            new(-1.0f,  1.0f, -1.0f), //-+- 5
            new(-1.0f, -1.0f,  1.0f), //--+ 6
            new(-1.0f, -1.0f, -1.0f)  //--- 7
        ];

        // Texture coordinates
        public static readonly Vector2[] texCoords =
        [
            new(1.0f, 1.0f), //++ 0
            new(1.0f, 0.0f), //+- 1
            new(0.0f, 0.0f), //-- 2
            new(0.0f, 1.0f)  //-+ 3
        ];

        // Face offset indexes
        public static readonly Matrix2x3[] faceOffsetIndexes =
        [
            // Top face
            new(0, 1, 4, 0, 1, 3),
            new(1, 5, 4, 1, 2, 3),
            
            // Bottom face
            new(2, 6, 3, 0, 3, 1),
            new(6, 7, 3, 3, 2, 1),

            // Front face
            new(0, 2, 1, 0, 1, 3),
            new(2, 3, 1, 1, 2, 3),

            // Back face
            new(4, 5, 6, 0, 3, 1),
            new(5, 7, 6, 3, 2, 1),

            // Right face
            new(0, 4, 2, 0, 1, 3),
            new(4, 6, 2, 1, 2, 3),

            // Left face
            new(1, 3, 5, 0, 3, 1),
            new(3, 7, 5, 3, 2, 1)
        ];
    }
    public class BlockUtilities
    {
        public static Vector3[] BlockCubeTransform(Vector3[] blocks)
        {
            List<Vector3> vectors = [];
            foreach (Vector3 block in blocks)
            {
                foreach (Matrix2x3 facedata in BlockConstants.faceOffsetIndexes)
                {
                    Vector3i offset = new((int)facedata[0,0], (int)facedata[0,1], (int)facedata[0,2]);
                    for (int j = 0; j < 3; j++)
                    {
                        vectors.Add(block + BlockConstants.offsets[offset[j]]);
                    }
                }
            }
            return [.. vectors];
        }

        public static Vector2[] BlockCubeTexCoord(Vector3[] blocks)
        {
            List<Vector2> TexCoords = [];
            foreach (Vector3 block in blocks)
            {
                foreach (Matrix2x3 facedata in BlockConstants.faceOffsetIndexes)
                {
                    Vector3i texCoordIndex = new((int)facedata[1,0], (int)facedata[1,1], (int)facedata[1,2]);
                    for (int j = 0; j < 3; j++)
                    {
                        TexCoords.Add(BlockConstants.texCoords[texCoordIndex[j]]);
                    }
                }
            }
            return [.. TexCoords];
        }

        public static (Vector3[], Vector2[]) BlockCube(Vector3[] blocks)
        {
            Console.WriteLine("Getting Block Transformations");
            (List<Vector3>, List<Vector2>) data = ([], []);
            for(int k = 0; k < blocks.Length; k++) {
                foreach (Matrix2x3 facedata in BlockConstants.faceOffsetIndexes)
                {
                    Vector3i offset = new((int)facedata[0,0], (int)facedata[0,1], (int)facedata[0,2]);
                    Vector3i texCoordIndex = new((int)facedata[1,0], (int)facedata[1,1], (int)facedata[1,2]);
                    for (int j = 0; j < 3; j++)
                    {
                        data.Item1.Add(blocks[k] + BlockConstants.offsets[offset[j]] * BlockConstants.cubeSize);
                        data.Item2.Add(BlockConstants.texCoords[texCoordIndex[j]]);
                    }
                }
            }
            return ([.. data.Item1], [.. data.Item2]);
        }
    }
}