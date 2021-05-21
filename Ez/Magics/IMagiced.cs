namespace Ez.Magics
{
    /// <summary>
    /// Suppots a <see cref="IMagicSupport"/> property.
    /// </summary>
    public interface IMagiced
    {
        /// <summary>
        /// Gets <see cref="MagicSupport"/> of a <see cref="IMagiced"/> instance.
        /// </summary>
        IMagicSupport MagicSupport { get; }
    }
}
