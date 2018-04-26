using TransformLib.Transforms;
using Xunit;

namespace TransformLib.Tests.Transforms
{
    public class PesudoStringUnitTest
    {
        private const string PSEUDO_STRING = "hello there";

        [Fact]
        public void Transform_BaseTransform_Null()
        {
            // Arrange
            var testObject = new PseudoString(PSEUDO_STRING);

            // Act
            var result = testObject.BaseTransform;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Transform_ReturnsOriginalString()
        {
            // Arrange
            var testObject = new PseudoString(PSEUDO_STRING);

            // Act
            var result = testObject.Transform();

            // Assert
            Assert.Equal(PSEUDO_STRING, result);
        }
    }
}