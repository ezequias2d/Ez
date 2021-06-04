using System;
using System.Collections.Concurrent;
using Ez.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Ez.Assets
{
    /// <summary>
    /// Represents that implementation a cached <see cref="IAssetManager"/>.
    /// </summary>
    public sealed class AssetManager : IAssetManager
    {
        #region fields
        private readonly MultiValueDicionary<string, object> _loadedAssets;
        private readonly ConcurrentList<IDisposable> _disposableAssets;
        private readonly IAssetSource _assetSource;
        private bool _disposed;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetManager"/> class 
        /// with a <see cref="IAssetSource"/>.
        /// </summary>
        /// <param name="assetSource"></param>
        public AssetManager(IAssetSource assetSource)
        {
            if (assetSource == null)
                throw new NullReferenceException(nameof(assetSource));

            _assetSource = assetSource;
            _loadedAssets = new MultiValueDicionary<string, object>(
                new ConcurrentDictionary<string, ICollection<object>>(StringComparer.OrdinalIgnoreCase), 
                () => new ConcurrentList<object>());
            _disposableAssets = new ConcurrentList<IDisposable>();
            _disposed = false;
        }

        /// <summary>
        /// Destroys a instance of <see cref="AssetManager"/> class.
        /// </summary>
        ~AssetManager() => Dispose(false);

        #region public methods
        /// <summary>
        /// Releases all resources used by the <see cref="AssetManager"/> class.
        /// </summary>
        public void Dispose() => Dispose(true);

        /// <summary>
        /// Disposes all data that was loaded by this <see cref="AssetManager"/>.
        /// </summary>
        public void Unload()
        {
            using(var operation = _disposableAssets.GetOperationList())
            {
                foreach(var disposable in operation)
                {
                    disposable.Dispose();
                }
                operation.Clear();
            }
            _loadedAssets.Clear();
        }

        /// <summary>
        /// Get the first loaded asset, if it is already loaded, otherwise load the asset from physical memory 
        /// </summary>
        /// <typeparam name="T">Type of asset.</typeparam>
        /// <param name="assetName">Asset name.</param>
        /// <returns>The first asset from asset name.</returns>
        public T GetAsset<T>(string assetName)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(AssetManager));

            if (string.IsNullOrEmpty(assetName))
                throw new ArgumentNullException(nameof(assetName));

            // Check for a previously loaded asset first
            if (TryGetAsset(assetName, out T asset))
                return asset;

            // Read the asset.
            if (_assetSource.ReadAsset<T>(assetName, out asset))
                LoadAsset(assetName, asset);
            else
                throw new ArgumentException($"The asset {assetName} was not found");

            return asset;
        }

        /// <summary>
        /// Loads an asset to the content manager.
        /// </summary>
        /// <typeparam name="T">The type of asset to be loaded.</typeparam>
        /// <param name="assetName">The name of the asset to be loaded.</param>
        /// <param name="asset">The asset to be loaded.</param>
        public void LoadAsset<T>(string assetName, in T asset)
        {
            _loadedAssets.Add(assetName, asset);
            if (asset is IDisposable disposable)
                _disposableAssets.Add(disposable);
        }

        /// <summary>
        /// Unloads an asset by type and name in a <see cref="IAssetManager"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of asset to unload.</typeparam>
        /// <param name="assetName">The name of the asset to be unloaded.</param>
        public void UnloadAsset<T>(string assetName)
        {
            if (TryGetAsset(assetName, out T asset))
            {
                _loadedAssets.Remove(assetName, asset);

                if (asset is IDisposable disposable)
                {
                    _disposableAssets.Remove(disposable);
                    disposable.Dispose();
                }
            }
        }
        #endregion

        #region private methods
        private void Dispose(bool disposing)
        {

            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                    Unload();
            }
        }

        private bool TryGetAsset<T>(string assetName, out T asset)
        {
            if (_loadedAssets.TryGetValue(assetName, out IReadOnlyCollection<object> objs))
            {
                asset = (T)objs
                    .First((element) => element is T);
                return true;
            }
            asset = default;
            return false;
        }
        #endregion
    }

}
