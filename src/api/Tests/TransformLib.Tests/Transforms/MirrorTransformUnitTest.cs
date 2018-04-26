using Moq;
using TransformLib.Transforms;
using Xunit;

namespace TransformLib.Tests.Transforms
{
    public class MirrorTransformUnitTest
    {
        private const string PSEUDO_STRING = "hello there";

        [Fact]
        public void MirrorTransform_Transform_Successful()
        {
            // Arrange
            var pseudoStringMock = new Mock<IPseudoTransform>();
            pseudoStringMock
                .Setup(x => x.Transform())
                .Returns(PSEUDO_STRING);
            var testObject = new MirrorTransform(pseudoStringMock.Object);

            // Act
            var result = testObject.Transform();

            // Assert
            Assert.Equal("ereht olleh", result);
        }
    }
}