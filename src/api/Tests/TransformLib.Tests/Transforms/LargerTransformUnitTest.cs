using Moq;
using TransformLib.Transforms;
using Xunit;

namespace TransformLib.Tests.Transforms
{
    public class LargerTransformUnitTest
    {
        private const string PSEUDO_STRING = "short and looonnnggg words";

        [Fact]
        public void LargerTransform_Transform_Successful()
        {
            // Arrange
            var pseudoStringMock = new Mock<IPseudoTransform>();
            pseudoStringMock
                .Setup(x => x.Transform())
                .Returns(PSEUDO_STRING);
            var testObject = new LargerTransform(pseudoStringMock.Object);

            // Act
            var result = testObject.Transform();

            // Assert
            Assert.Equal("xshortx and xxxlooonnngggxxx xwordsx", result);
        }
    }
}
