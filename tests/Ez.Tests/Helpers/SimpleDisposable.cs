namespace Ez.Tests.Helpers
{
    internal class SimpleDisposable : Disposable
    {
        protected override void ManagedDispose()
        {
            // test implementation not required
        }

        protected override void UnmanagedDispose()
        {
            // test implementation not required
        }
    }
}
