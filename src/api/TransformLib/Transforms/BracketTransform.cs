using System;

namespace TransformLib.Transforms
{
    /// <summary>
    /// Represents a concrete decorator object used with the <see cref="PseudoString" /> class.
    /// </summary>
    public class BracketTransform : TransformDecoratorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BracketTransform" /> class.
        /// </summary>
        /// <param name="transform">The base transform.</param>
        public BracketTransform(IPseudoTransform transform)
            : base(transform)
        {
        }

        /// <inheritdoc />
        public override string Transform()
        {
            var localizedString = BaseTransform.Transform();

            return $"[{localizedString}]";
        }
    }
}