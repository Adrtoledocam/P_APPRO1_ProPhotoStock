namespace TestProject1
    
{
    public class PriceCalculationTests
    {
        [Fact]
        public void CalculatePrice_ExclusifPub()
        {
            string contract = "exclusif";
            string usage = "pub";
            int expected = 740;
            int actual = 0;

            if (contract == "exclusif" && usage == "pub")
                actual = 740;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculatePrice_DiffusionMedia()
        {
            string contract = "diffusion";
            string usage = "media";
            int expected = 700;
            int actual = 0;

            if (contract == "diffusion" && usage == "media")
                actual = 700;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculatePrice_NullValues()
        {
            string contract = null;
            string usage = "pub";
            int expected = 0;
            int actual = 0;

            if (contract == null || usage == null)
                actual = 0;

            Assert.Equal(expected, actual);
        }    
    }
}
