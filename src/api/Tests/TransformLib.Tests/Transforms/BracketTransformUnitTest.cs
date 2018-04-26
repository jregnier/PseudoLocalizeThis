using Moq;
using Xunit;
using TransformLib.Transforms;

namespace TransformLib.Tests.Transforms
{
    public class BracketTransformUnitTest
    {
        private const string PSEUDO_STRING = "this is a test";

        [Fact]
        public void BracketTransform_Transform_Successful()
        {
            // Arrange
            var pseudoStringMock = new Mock<IPseudoTransform>();
            pseudoStringMock
                .Setup(x => x.Transform())
                .Returns(PSEUDO_STRING);
            var testObject = new BracketTransform(pseudoStringMock.Object);

            // Act
            var result = testObject.Transform();

            // Assert
            Assert.Equal("[this is a test]", result);
        }
    }
}