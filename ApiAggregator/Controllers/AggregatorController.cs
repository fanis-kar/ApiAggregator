using ApiAggregator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAggregator.Controllers;

[ApiController]
[Route("api/[controller]")]
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