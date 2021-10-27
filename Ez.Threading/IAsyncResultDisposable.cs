using System;

namespace Ez.Threading
{
    public interface IAsyncResultDisposable : IAsyncResult, IDisposable
    {
    }
}
