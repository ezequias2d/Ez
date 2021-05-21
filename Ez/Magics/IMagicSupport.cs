using System;
using System.Collections.Generic;

namespace Ez.Magics
{
    /// <summary>
    /// Provides type support verification 
    /// </summary>
    public interface IMagicSupport
    {
        /// <summary>
        /// Gets the display name of a <see cref="IMagicSupport"/>.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Checks if a type is compatible.
        /// </summary>
        /// <typeparam name="T">The type to be checked.</typeparam>
        /// <returns><see langword="true"/> if suppoted, otherwise 
        /// <see langword="false"/>.</returns>
        bool Supports<T>();

        /// <summary>
        /// Gets supported types.
        /// </summary>
        IEnumerable<Type> Types { get; }
    }
}
