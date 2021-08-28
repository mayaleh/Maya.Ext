using System;
using Xunit;

namespace Maya.Ext.Tests
{
    public class UnitTests
    {
        [Fact]
        public void CompareTwoInstances_ShouldAlwaysTheSame()
        {
            //Arrange
            var expected = Unit.Default;

            //Act
            var act = new Unit();

            //Assert
            Assert.Equal(expected, act);
            Assert.True(act.Equals(expected));
            Assert.True(act == expected);
            Assert.Equal(expected.GetHashCode(), act.GetHashCode());
        }
    }
}