namespace TransformLib.Transforms
{
    /// <summary>
    /// represents a concrete pseudo string component.
    /// </summary>
    public class PseudoString : IPseudoTransform
    {
        private readonly string _pseudoString;

        /// <summary>
        /// Initializes a new instance of the <see cref="PseudoString"/> class.
        /// </summary>
        /// <param name="pseudoString">The pseudo string.</param>
        public PseudoString(string pseudoString)
        {
            this._pseudoString = pseudoString;
        }

        /// <inheritdoc/>
        public IPseudoTransform BaseTransform => null;

        /// <inheritdoc/>
        public void RemoveDecoration(IPseudoTransform transformToRemove)
        {
            return;
        }

        /// <inheritdoc/>
        public string Transform()
        {
            return _pseudoString;
        }
    }
}