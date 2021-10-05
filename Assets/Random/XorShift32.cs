namespace RandomNumberGeneration
{
    public class XorShift32
    {
        private uint state;

        public XorShift32(uint seed)
        {
            UnityEngine.Assertions.Assert.AreNotEqual(seed, 0);
            state = seed;
        }

        public uint Next()
        {
            uint x = state;
            x ^= x << 13;
            x ^= x >> 17;
            x ^= x << 5;
            state = x;
            return state;
        }
    }
}
