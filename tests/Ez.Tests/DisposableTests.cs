using Ez.Tests.Helpers;
using System;
using Xunit;

namespace Ez.Tests
{
    public class DisposableTests
    {

        [Fact]
        public void Test1()
        {
            var temp = new SimpleDisposable();
            Assert.False(temp.IsDisposed, "The Disposable class started disposed!!");

            temp.Dispose();
            Assert.True(temp.IsDisposed, "After calling Dispose, the object does not have the dispose flag enabled!");
        }
    }
}
