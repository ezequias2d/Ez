using System.Threading;

namespace Ez.Threading
{
    public class ReaderWriterPropertyWrapper<T>
    {
        private readonly ReaderWriterLockSlim _locker;
        private T _value;

        public ReaderWriterPropertyWrapper(T value)
        {
            _value = value;
            _locker = new ReaderWriterLockSlim();
        }

        public T Value 
        {
            get
            {
                try
                {
                    _locker.EnterReadLock();
                    return _value;
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            set 
            {
                try
                {
                    _locker.EnterWriteLock();
                    _value = value;
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }
        }

        public static implicit operator T(ReaderWriterPropertyWrapper<T> property) =>
            property.Value;
    }
}
