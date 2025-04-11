using System;

namespace IFBOptLib.IFBGraphics
{
    public static class IFBUtils
    {
        public static float RandomFloat(System.Random random, float min, float max)
        {
            return Convert.ToSingle(random.NextDouble()) * (max - min) + min;
        }
    }
}
