using System;

namespace Tools.Helpers.Converters
{
    public static class AngleConverter
    {
        public static float RadiansToDegrees(float v)
        {
            return (float)(v * (Math.PI / 180));
        }

        public static float DegreesToRadians(float v)
        {
            return (float)(v * Math.PI / 180);
        }
    }
}
