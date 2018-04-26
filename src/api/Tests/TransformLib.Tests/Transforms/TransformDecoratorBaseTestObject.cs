using TransformLib.Transforms;
using System;

namespace TransformLib.Tests.Transforms
{
    /// <summary>
    /// Object used to test the <see cref="TransformDecoratorBase"/> class.
    /// </summary>
    public class TransformDecoratorBaseTestObject : TransformDecoratorBase
    {
        public TransformDecoratorBaseTestObject(IPseudoTransform baseTransform)
            : base(baseTransform)
        {
        }

        public override string Transform()
        {
            throw new NotImplementedException();
        }
    }
}