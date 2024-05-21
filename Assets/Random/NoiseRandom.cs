using Noise;

namespace RandomNumberGeneration
{
    public class NoiseRandom
    {
        public uint seed;
        public int position;

        public NoiseRandom(uint seed = 0, int position = 0)
        {
            this.seed = seed;
            this.position = position;
        }

        public void SetSeedAndPosition(uint seed, int position)
        {
            this.seed = seed;
            this.position = position;
        }

        public override string ToString()
        {
            return $"seed {seed.ToString()}, position {position.ToString()}";
        }

        public uint Next()
        {
            var ret = SquirrelNoise.SquirrelNoise5(position, seed);
            position += 1;
            return ret;
        }

        public int NextInt()
        {
            return Range(int.MinValue, int.MaxValue);
        }

        public float NextFloat()
        {
            return Range(float.MinValue, float.MaxValue);
        }

        public float NextFloat01()
        {
            var returnVal = SquirrelNoise.Get1dNoiseZeroToOne(position, seed);
            position += 1;
            return returnVal;
        }

        public float Range(float min, float max)
        {
            float perc = NextFloat01();
            var offset = perc * (max - min);
            return min + offset;
        }

        public int Range(int min, int max)
        {
            float perc = NextFloat01();
            var offset = (int) (perc * (max - min));
            return min + offset;
        }
    }
}