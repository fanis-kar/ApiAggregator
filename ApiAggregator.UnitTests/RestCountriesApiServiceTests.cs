using ApiAggregator.Services.Interfaces;
using Moq;

namespace ApiAggregator.UnitTests;

public class RestCountriesApiServiceTests
{
    private readonly Mock<IRestCountriesApiService> _mockRestCountriesApiService;

    public RestCountriesApiServiceTests()
    {
        _mockRestCountriesApiService = new Mock<IRestCountriesApiService>();
    }

    [Fact]
    public async Task GetData_ShouldReturnNotEmptyData()
    {
        // Arrange
        var query = "euro";
        _mockRestCountriesApiService.Setup(service => service.GetData(query)).ReturnsAsync(PrepareData.GetRestCountriesApiData());

        // Act
        var result = await _mockRestCountriesApiService.Object.GetData(query);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Countries);
    }

    [Fact]
    public async Task GetData_ShouldReturnTwoCountries()
    {
        // Arrange
        var query = "euro";
        _mockRestCountriesApiService.Setup(service => service.GetData(query)).ReturnsAsync(PrepareData.GetRestCountriesApiData());

        // Act
        var result = await _mockRestCountriesApiService.Object.GetData(query);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Countries);
        Assert.Equal(2, result.Countries.Count);
    }
}