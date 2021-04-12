namespace Ez
{
    /// <summary>
    /// Describes an object that can be reset.
    /// </summary>
    public interface IResettable
    {
        /// <summary>
        /// Resets the object to a state that can be reused or destroyed.
        /// </summary>
        void Reset();

        /// <summary>
        /// Set an object just before being used.
        /// </summary>
        void Set();
    }
}
