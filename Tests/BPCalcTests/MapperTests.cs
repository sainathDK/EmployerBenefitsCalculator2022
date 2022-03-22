using NUnit.Framework;
using Shouldly;


namespace BPCalcTests
{
    [TestFixture]
    public class MapperTests
    {
        [Test]
        public void RequestToWorkflow()
        {

            int x = 10;

            x.ShouldBe(10);

        }

    }
}
