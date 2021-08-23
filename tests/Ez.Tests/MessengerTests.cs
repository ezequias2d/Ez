using Ez.Messenger;
using Ez.Tests.Helpers;
using System;
using Xunit;

namespace Ez.Tests
{
    public class MessengerTests
    {
        [Fact]
        public void SendMessengerTest1()
        {
            var ro = new ReceiverObject<EventArgs>();
            Assert.Null(ro.Sender);
            Assert.Null(ro.Args);

            var sender = new object();
            var args = new EventArgs();
            ro.SendMessenger(nameof(ro.OnEvent), sender, args);

            Assert.Same(sender, ro.Sender);
            Assert.Same(args, ro.Args);
        }

        [Fact]
        public void SendMessengerTest2()
        {
            var ro = new ReceiverObject<CustomEventArgs>();
            Assert.Null(ro.Sender);
            Assert.Null(ro.Args);

            var sender = new object();
            var cusomObject = new object();
            var args = new CustomEventArgs(cusomObject);
            ro.SendMessenger(nameof(ro.OnEvent), sender, args);

            Assert.Same(sender, ro.Sender);
            Assert.Same(cusomObject, ro.Args.CustomValue);
            Assert.Same(args, ro.Args);
        }
    }
}
