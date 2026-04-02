using DearlershipAI.API.Models.DTOs.Requests;
using DearlershipAI.API.Models.Entities;
using DearlershipAI.API.Services.UseCases.Cars.Create;
using Microsoft.AspNetCore.Mvc;

namespace DearlershipAI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase {
    [HttpPost]
    [ProducesResponseType(typeof(Car), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCar([FromBody] RequestCarJson request, [FromServices] ICreateCarUseCase useCase){
        var response = await useCase.Execute(request);
        if(response == null) 
            return BadRequest("Failed to create car.");
        return Created(string.Empty, response);
    }
}
