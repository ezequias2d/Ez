using System;

namespace Ez.Tests.Helpers
{
    public class ReceiverObject<TArgs> where TArgs : EventArgs
    {
        public ReceiverObject()
        {
            Sender = null;
            Args = null;
        }

        public object Sender { get; private set; }
        public TArgs Args { get; private set; }

        public void OnEvent(object sender, TArgs args)
        {
            Sender = sender;
            Args = args;
        }
    }
}
