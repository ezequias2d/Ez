using System;
using System.Collections.Generic;
using System.Text;

namespace Ez.Threading
{
    public struct DisposeEventArgs
    {
        public DisposeEventArgs(bool disposing)
        {
            Disposing = disposing;
        }

        public bool Disposing { get; }
    }
}
