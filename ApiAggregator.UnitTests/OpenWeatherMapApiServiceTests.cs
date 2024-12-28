using ApiAggregator.Services.Interfaces;
using Moq;

namespace ApiAggregator.UnitTests;

public class OpenWeatherMapApiServiceTests
{
    private readonly Mock<IOpenWeatherMapApiService> _mockOpenWeatherMapApiService;

    public OpenWeatherMapApiServiceTests()
    {
        _mockOpenWeatherMapApiService = new Mock<IOpenWeatherMapApiService>();
    }

    [Fact]
    public async Task GetData_ShouldReturnNotEmptyData()
    {
        // Arrange
        var query = "London,uk";
        var x = 5;
        _mockOpenWeatherMapApiService.Setup(service => service.GetData(query)).ReturnsAsync(PrepareData.GetOpenWeatherMapApiData());

        // Act
        var result = await _mockOpenWeatherMapApiService.Object.GetData(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Cod);
        Assert.NotEqual(default, result);
    }
}