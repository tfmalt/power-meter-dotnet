using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PowerMeterApi.Tests
{
    public class PowerControllerTest
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, 4);
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(1, 5);
        }
    }
}
