using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Progi.BidCalculator.API.Models.Request;
using Progi.BidCalculator.API.Models.Response;
using Progi.BidCalculator.Application.Interfaces;

namespace Progi.BidCalculator.API.Controllers;

[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
[ApiVersion("1.0")]
public sealed class CalculatorController(IBidCalculationService bidCalculationService,
    IMapper mapper) : ControllerBase
{

    [HttpPost("CalculateBid")]
    public async Task<ActionResult> CalculateBid([FromBody] CalculateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await bidCalculationService.CalculateBidAsync(request.VehicleType, request.Price);

        var mappedData = mapper.Map<CalculateResponse>(response);

        return Ok(mappedData);
    }
}