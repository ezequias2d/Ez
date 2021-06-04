using Ez.Magics;
using System.IO;

namespace Ez.Assets
{
    /// <summary>
    /// Provides an interface to deserialize.
    /// </summary>
    public interface IAssetReader : IMagiced
    {
        /// <summary>
        /// Attempts to read an <paramref name="value"/> from the stream.
        /// </summary>
        /// <typeparam name="T">The type of instance to be readed.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="value">When this method returns, contains the readed instance,
        /// if it is available, otherwise <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the value was read; otherwise,
        /// <see langword="false"/>.</returns>
        bool TryRead<T>(Stream stream, out T value);
    }
}
