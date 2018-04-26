namespace TransformLib.Transforms
{
    /// <summary>
    /// Represents a pseudo transform.
    /// </summary>
    public interface IPseudoTransform
    {
        /// <summary>
        /// Gets the base transform.
        /// </summary>
        IPseudoTransform BaseTransform { get; }

        /// <summary>
        /// Performs a transformation.
        /// </summary>
        /// <returns>The new transformed string.</returns>
        string Transform();

        /// <summary>
        /// Remove a decoration from the stack.
        /// </summary>
        /// <param name="transformToRemove">The transform to remove.</param>
        void RemoveDecoration(IPseudoTransform transformToRemove);
    }
}