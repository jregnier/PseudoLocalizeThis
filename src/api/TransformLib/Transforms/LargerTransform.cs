using System;
using System.Collections.Generic;
using System.Linq;

namespace TransformLib.Transforms
{
    /// <summary>
    /// Represents a concrete decorator object used with the <see cref="PseudoString" /> class.
    /// </summary>
    public class LargerTransform : TransformDecoratorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LargerTransform" /> class.
        /// </summary>
        /// <param name="transform">The base transform.</param>
        public LargerTransform(IPseudoTransform transform)
            : base(transform)
        {
        }

        /// <inheritdoc />
        public override string Transform()
        {
            List<string> result = new List<string>();

            var sentence = BaseTransform.Transform();
            var words = sentence.Split(' ').ToList();

            foreach(var word in words)
            {
                var count = (int)(word.Length * 0.3f);
                var bufferString = new string('x', count);
                result.Add($"{bufferString}{word}{bufferString}");
            }

            return string.Join(" ", result);
        }
    }
}