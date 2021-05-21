using System;
using System.Collections.Generic;

namespace Ez.Magics
{
    /// <summary>
    /// A lightweight implementation of <see cref="IMagicSupport"/>.
    /// </summary>
    public sealed class MagicSupportSlim : IMagicSupport
    {
        private readonly HashSet<Type> _types;

        /// <summary>
        /// Creates a new instance of <see cref="MagicSupportSlim"/> class.
        /// </summary>
        /// <param name="displayName">The value of <see cref="DisplayName"/>.</param>
        /// <param name="types">Supported types in <see cref="Types"/> and <see cref="Supports{T}"/>.</param>
        public MagicSupportSlim(string displayName, params Type[] types)
        {
            DisplayName = displayName;
            _types = new HashSet<Type>(types);
        }

        /// <summary>
        /// Gets the display name of a <see cref="MagicSupportSlim"/>.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Gets supported types.
        /// </summary>
        public IEnumerable<Type> Types => _types;

        /// <summary>
        /// Checks if a type is compatible.
        /// </summary>
        /// <typeparam name="T">The type to be checked.</typeparam>
        /// <returns><see langword="true"/> if suppoted, otherwise 
        /// <see langword="false"/>.</returns>
        public bool Supports<T>() => _types.Contains(typeof(T));
    }
}
