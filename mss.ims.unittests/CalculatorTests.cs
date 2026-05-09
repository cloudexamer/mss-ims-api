namespace mss.ims.unittests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_ShouldReturnCorrectSum()
        {
            // Arrange
            int a = 2;
            int b = 3;

            // Act
            int result = a + b;

            // Assert
            Assert.Equal(5, result);
        }
    }
}
