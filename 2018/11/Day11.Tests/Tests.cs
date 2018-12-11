using NUnit.Framework;

namespace Day11.Tests
{
    [TestFixture]
    public class Tests
    {
        [TestCase(3, 5, 8, 4)]
        [TestCase(122, 79, 57, -5)]
        [TestCase(217, 196, 39, 0)]
        [TestCase(101, 153, 71, 4)]
        public void TestPowerLevel(int x, int y, int serialNumber, int expected)
        {
            Assert.That(Day11.PowerGrid.PowerLevel(x, y, serialNumber), Is.EqualTo(expected));
        }
    }
}
