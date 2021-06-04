using NUnit.Framework;

namespace lab4
{
    [TestFixture]
    public class RpnTest
    {
        [Test]
        public static void CalculateTest()
        {
            Assert.AreEqual(-3, Rpn.Calculate("(3-6)"));
        }
        
        [Test]
        public static void GetExpressionTest()
        {
            Assert.AreEqual("3 6 - ", Rpn.GetExpression("3-6"));
        }
        
        [Test]
        public static void CountingTest()
        {
            Assert.AreEqual(-3, Rpn.Counting("3 6 - "));
        }

        [Test]
        public static void IsDelimeterTest()
        {
            Assert.True(Rpn.IsDelimeter('='));
        }
        
        [Test]
        public static void IsNotDelimeterTest()
        {
            Assert.False(Rpn.IsDelimeter('!'));
        }
        
        [Test]
        public static void IsOperatorTest()
        {
            Assert.True(Rpn.IsOperator('('));
        }
        
        [Test]
        public static void IsNotOperatorTest()
        {
            Assert.False(Rpn.IsOperator('0'));
        }
    }
}