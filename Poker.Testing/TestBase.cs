using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PokerTest
{

    public interface IAdd
    {
        int Add(int a, int b);
    }
    public class Add : IAdd
    {
        int IAdd.Add(int a, int b)
        {
            return a + b;
        }
    }

    [TestFixture]
    public class TestBase
    {
        [Test]
        public void ShouldAddTwoNumbers()
        {
            IAdd something = new Add();

            var expectedResult = something.Add(1, 1);

            Assert.AreEqual(expectedResult, 2);
        }
    }
}
