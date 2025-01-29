using OpenTK.Mathematics;

namespace ConsoleApp1.Utilities
{
    public readonly struct CMath
    {
        public readonly float epsilon = 0.0001f;
        public readonly float epsilon_90 = 89.0f;
        public readonly float epsilon_n90 = -89.0f;
        public static readonly float epsilon_s = 0.0001f;
        public static readonly float epsilon_90_s = 89.0f;
        public static readonly float epsilon_n90_s = -89.0f;

        public CMath()
        {
        }
    }

    public class CMathFunctions
    {
        public static float Vec3Magnitude(Vector3 p1, Vector3 p2)
        {
            return Vector3.Distance(p1, p2);
        }

        public static float Vec3MagnitudeSquared(Vector3 p1, Vector3 p2)
        {
            return Vector3.DistanceSquared(p1, p2);
        }

        public static Vector3 Vec3SnapToIncrement(Vector3 p1, Vector3 incr)
        {
            return new(
                MathF.Round(p1.X / incr.X) * incr.X,
                MathF.Round(p1.Y / incr.Y) * incr.Y,
                MathF.Round(p1.Z / incr.Z) * incr.Z
            );
        }
    }
}