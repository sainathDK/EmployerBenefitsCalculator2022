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

            int x = 11;

            x.ShouldBe(10);

        }

    }
}
