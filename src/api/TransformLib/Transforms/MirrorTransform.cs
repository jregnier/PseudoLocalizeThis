using System;
using System.Linq;

namespace TransformLib.Transforms
{
    /// <summary>
    /// Represents a concrete decorator object used with the <see cref="PseudoString" /> class.
    /// </summary>
    public class MirrorTransform : TransformDecoratorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MirrorTransform" /> class.
        /// </summary>
        /// <param name="transform">The base transform.</param>
        public MirrorTransform(IPseudoTransform transform)
            : base(transform)
        {
        }

        /// <inheritdoc />
        public override string Transform()
        {
            var localizedString = BaseTransform.Transform();

            return new string(localizedString
                .ToCharArray()
                .Reverse()
                .ToArray());
        }
    }
}