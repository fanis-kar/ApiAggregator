using ApiAggregator.Services.Interfaces;
using Moq;

namespace ApiAggregator.UnitTests;

public class NewsApiServiceTests
{
    private readonly Mock<INewsApiService> _mockNewsApiService;

    public NewsApiServiceTests()
    {
        _mockNewsApiService = new Mock<INewsApiService>();
    }

    [Fact]
    public async Task GetData_ShouldReturnNotEmptyData()
    {
        // Arrange
        _mockNewsApiService.Setup(service => service.GetData(string.Empty)).ReturnsAsync(PrepareData.GetNewsApiData());

        // Act
        var result = await _mockNewsApiService.Object.GetData(string.Empty);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Articles);
    }

    [Fact]
    public async Task GetData_ShouldReturnTwoArticles()
    {
        // Arrange
        _mockNewsApiService.Setup(service => service.GetData(string.Empty)).ReturnsAsync(PrepareData.GetNewsApiData());

        // Act
        var result = await _mockNewsApiService.Object.GetData(string.Empty);

        // Assert
        Assert.Equal(2, result.Articles.Count);
    }
}
