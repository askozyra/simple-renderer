namespace CommonStructures.Misc
{
    public class Color
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }

        public Color(float r = 0.0f, float g = 0.0f, float b = 0.0f, float a = 1.0f)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static Color GrayColor { get; } = new Color(0.3f, 0.3f, 0.3f);
    }
}
