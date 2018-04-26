using Moq;
using TransformLib.Transforms;
using Xunit;

namespace TransformLib.Tests.Transforms
{
    public class TransformDecoratorBaseUnitTest
    {
        [Fact]
        public void BaseTransform_RemoveDecorator_Successful()
        {
            // Arrange
            var componentMock = new Mock<IPseudoTransform>();
            var decorator1 = new TransformDecoratorBaseTestObject(componentMock.Object);
            var decorator2 = new TransformDecoratorBaseTestObject(decorator1);
            var decorator3 = new TransformDecoratorBaseTestObject(decorator2);
            var decorator4 = new TransformDecoratorBaseTestObject(decorator3);

            // Act
            decorator4.RemoveDecoration(decorator2);
            var result = decorator4.BaseTransform.BaseTransform;

            // Assert
            Assert.Equal(decorator1, result);
        }

        [Fact]
        public void BaseTransform_Set_Correctly()
        {
            // Arrange
            var pseudoTransformMock = new Mock<IPseudoTransform>();
            var testObject = new TransformDecoratorBaseTestObject(pseudoTransformMock.Object);

            // Act
            var result = testObject.BaseTransform;

            // Assert
            Assert.Equal(pseudoTransformMock.Object, result);
        }
    }
}