using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ez.Assets
{
    /// <summary>
    /// Represents a source from which assets can be read and written.
    /// </summary>
    public interface IAssetSource
    {
        /// <summary>
        /// Writes an <paramref name="asset"/> in the <see cref="IAssetSource"/>.
        /// </summary>
        /// <typeparam name="T">The type of asset to be written.</typeparam>
        /// <param name="asset">The asset to be written.</param>
        /// <param name="assetName">The name of asset to be written.</param>
        void WriteAsset<T>(in T asset, in string assetName);

        /// <summary>
        /// Reads an asset from the <see cref="IAssetSource"/>.
        /// </summary>
        /// <typeparam name="T">The type of asset to be read.</typeparam>
        /// <param name="assetName">The name of asset to be read.</param>
        /// <param name="asset">The asset read from the source, otherwise, <see langword="default"/>.</param>
        /// <returns><see langword="true"/>, if the asset was read correctly, otherwise <see langword="false"/>.</returns>
        bool ReadAsset<T>(string assetName, out T asset);
    }
}
