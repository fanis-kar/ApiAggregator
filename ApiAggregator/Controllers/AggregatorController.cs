using ApiAggregator.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiAggregator.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]

public class AggregatorController : ControllerBase
{
    private readonly IApiAggregationService _apiAggregationService;

    public AggregatorController(IApiAggregationService apiAggregationService)
    {
        _apiAggregationService = apiAggregationService;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetData()
    {
        var data = await _apiAggregationService.GetAggregatedData();
        return Ok(data);
    }
}