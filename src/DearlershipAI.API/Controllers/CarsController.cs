using DearlershipAI.API.Models.DTOs.Requests;
using DearlershipAI.API.Models.DTOs.Responses;
using DearlershipAI.API.Models.Entities;
using DearlershipAI.API.Services.UseCases.Cars.Create;
using DearlershipAI.API.Services.UseCases.Cars.Search;
using Microsoft.AspNetCore.Mvc;

namespace DearlershipAI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase {
    [HttpPost]
    [ProducesResponseType(typeof(Car), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCar([FromBody] RequestCarJson request, [FromServices] ICreateCarUseCase useCase) {
        var response = await useCase.Execute(request);
        if (response == null)
            return BadRequest("Failed to create car.");
        return Created(string.Empty, response);
    }

    [HttpPost("search")]
    [ProducesResponseType(typeof(ResponseCarsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchCars([FromBody] RequestSearchCarJson request, [FromServices] ISearchCarUseCase useCase) {
        var response = await useCase.Execute(request);
        if (response == null)
            return BadRequest("Failed to search cars.");
        return Ok(response);
    }
}
