using Ez.Magics;
using System.IO;

namespace Ez.Assets
{
    /// <summary>
    /// Provides an interface to serialize.
    /// </summary>
    public interface IAssetWriter : IMagiced
    {
        /// <summary>
        /// Attempts to write an <paramref name="value"/> to the stream.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
        /// <param name="value">The instance to be written.</param>
        /// <param name="stream">The destination stream.</param>
        /// <returns></returns>
        bool TryWrite<T>(in T value, Stream stream);
    }
}
