using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ez.IO
{
    public sealed class ProgressStream : Stream
    {
        private readonly Stream _inner;
        private readonly long _basePosition;
        private readonly bool _leaveOpen;
        private long _current;
        private long _size;
        private double _invSize;
        public ProgressStream(Stream inner, bool leaveOpen)
        {
            _inner = inner;
            _current = 0;
            _leaveOpen = leaveOpen;

            _basePosition = inner.Position;
            UpdateSize();
        }

        public event EventHandler<ProgressEventArgs> Report;

        public override bool CanRead => _inner.CanRead;

        public override bool CanSeek => _inner.CanSeek;

        public override bool CanWrite => _inner.CanWrite;

        public override long Length => _inner.Length;

        public override long Position 
        { 
            get =>  _inner.Position;
            set 
            {
                _inner.Position = value;
                if (value > _basePosition + _size)
                    UpdateSize();
            }
        }

        public override void Flush() => _inner.Flush();

        public override int Read(byte[] buffer, int offset, int count)
        {
            var bytes = _inner.Read(buffer, offset, count);
            _current += bytes;
            InvokeReport();
            return bytes;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            var position = _inner.Seek(offset, origin);
            _current = position - _basePosition;
            InvokeReport();
            return position;
        }

        public override void SetLength(long value)
        {
            _inner.SetLength(value);
            UpdateSize();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _inner.Write(buffer, offset, count);
            _current = Position - _basePosition;
            UpdateSize();
            InvokeReport();
        }

        private void InvokeReport() => Report?.Invoke(this, new ProgressEventArgs((float)(_current * _invSize)));
        private void UpdateSize()
        {
            var aux = _inner.Position;
            _size = _inner.Length - _basePosition;
            if (_inner.Position != aux)
                _inner.Position = aux;
            _invSize = 1.0 / _size;
        }

        protected override void Dispose(bool disposing)
        {
            if(!_leaveOpen)
                _inner.Dispose();
        }
    }
}
