using OpenTK.Mathematics;

namespace ConsoleApp1.World
{
    public class World
    {
        (float, float, float) renderDistance = (2f, 2f, 2f);
        (float, float, float) chunkSize = (10f, 10f, 10f);
        (int, Vector3) chunkList;
        float cubeSize = 0.5f;

        Vector3[] pointOffsets =
        {
            new(1,1,1),
            new(1,-1,1),
            new(-1,-1,1),
            new(-1,1,1),
            new(1,1,-1),
            new(1,-1,-1),
            new(-1,-1,-1),
            new(-1,1,-1),
        };

        public World((float, float, float)? renderDistance = null, (float, float, float)? chunkSize = null, float? cubeSize = null)
        {
            if (renderDistance.HasValue)
            {
                this.renderDistance = renderDistance.Value;
            }
            if (chunkSize.HasValue)
            {
                this.chunkSize = chunkSize.Value;
            }
            if (cubeSize.HasValue)
            {
                this.cubeSize = cubeSize.Value;
            }

            for (int n = 0; n < pointOffsets.Length; n++)
            {
                pointOffsets[n] *= this.cubeSize;
            }
        }

        public void Generate()
        {
            Console.WriteLine("Generating..");
            Vector3 point = new(0f, 0f, 0f);
            foreach (Vector3 p in pointOffsets)
            {
                Console.WriteLine($"{point + p}");
            }
        }
    }
}