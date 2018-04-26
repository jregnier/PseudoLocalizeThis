namespace TransformLib.Transforms
{
    /// <summary>
    /// Base class for a decorator.
    /// </summary>
    public abstract class TransformDecoratorBase : IPseudoTransform
    {
        private IPseudoTransform _baseTransform;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformDecoratorBase"/> class.
        /// </summary>
        /// <param name="transform">The base transform.</param>
        public TransformDecoratorBase(IPseudoTransform baseTransform)
        {
            this._baseTransform = baseTransform;
        }

        /// <inheritdoc/>
        public IPseudoTransform BaseTransform => _baseTransform;

        /// <inheritdoc/>
        public void RemoveDecoration(IPseudoTransform transformToRemove)
        {
            var toRemove = transformToRemove as TransformDecoratorBase;

            if (this._baseTransform == null ||
                toRemove == null)
            {
                return;
            }

            if (this._baseTransform.Equals(transformToRemove))
            {
                this._baseTransform = toRemove.BaseTransform;
            }
            else
            {
                this._baseTransform.RemoveDecoration(transformToRemove);
            }
        }

        /// <inheritdoc/>
        public abstract string Transform();
    }
}