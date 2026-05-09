using MediatR;
using Moq;
using mss.ims.api.Features.Customers.mss.ims.api.Controllers;

namespace mss.ims.unittests
{
    public class CustomersControllerTests
    {
        [Fact]
        public void GetStatus_ShouldReturnHealthy()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();

            var controller = new CustomersController(mediatorMock.Object);

            // Act
            var result = controller.GetStatus();

            // Assert
            Assert.Equal("Healthy", result);
        }
    }
}
