using BPCalcAPI.Rules;
using NUnit.Framework;
using Shouldly;

namespace BPCalcTests
{
    [TestFixture]
    public class DiscountForNameStarsWithARRuleTests
    {
        [TestCase("Adam", 1000, 100)]
        [TestCase("John", 1000, 0)]
        [TestCase("Alice", 500, 50)]
        public void Test_Discount_Calculation(string memberName,decimal benefitCost,decimal expectedDiscount)
        {
            //Arrange
            var toTest = new DiscountForNameStarsWithARRule();

            //Act
            var resultFromRule = toTest.Compute(benefitCost, memberName);

            //Assert
            resultFromRule.ShouldBeEquivalentTo(expectedDiscount);
        }
    }
}
