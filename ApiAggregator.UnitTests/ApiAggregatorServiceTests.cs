using ApiAggregator.Services.Interfaces;
using Moq;

namespace ApiAggregator.UnitTests;

public class ApiAggregatorServiceTests
{
    private readonly Mock<IApiAggregationService> _mockApiAggregationApiService;

    public ApiAggregatorServiceTests()
    {
        _mockApiAggregationApiService = new Mock<IApiAggregationService>();
    }

    [Fact]
    public async Task GetAggregatedData_ShouldReturnNotEmptyData()
    {
        // Arrange
        _mockApiAggregationApiService.Setup(service => service.GetAggregatedData()).ReturnsAsync(PrepareData.GetAggregatedApiData());

        // Act
        var result = await _mockApiAggregationApiService.Object.GetAggregatedData();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.NewsApiResponse.Articles);
        Assert.Equal(200, result.OpenWeatherMapApiResponse.Cod);
        Assert.NotEmpty(result.RestCountriesApiResponse.Countries);
    }

    [Fact]
    public async Task GetAggregatedData_ShouldReturnManyData()
    {
        // Arrange
        _mockApiAggregationApiService.Setup(service => service.GetAggregatedData()).ReturnsAsync(PrepareData.GetAggregatedApiData());

        // Act
        var result = await _mockApiAggregationApiService.Object.GetAggregatedData();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.NewsApiResponse.Articles.Count);
        Assert.Equal(2, result.RestCountriesApiResponse.Countries.Count);
    }
}