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
    public async Task<IActionResult> GetData([FromQuery] string query1 = "everything", [FromQuery] string query2 = "Galatsi,GR", [FromQuery] string query3 = "euro")
    {
        var data = await _apiAggregationService.GetAggregatedData(query1, query2, query3);
        return Ok(data);
    }
}