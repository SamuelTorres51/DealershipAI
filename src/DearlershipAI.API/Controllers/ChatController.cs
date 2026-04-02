using DearlershipAI.API.Models.DTOs.Requests;
using DearlershipAI.API.Models.DTOs.Responses;
using DearlershipAI.API.Services.UseCases.Cars.Search;
using Microsoft.AspNetCore.Mvc;

namespace DearlershipAI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase {
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCarsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendMessage(
        [FromBody] RequestSearchCarJson request,
        [FromServices] ISearchCarUseCase useCase
    ) {
        if (request is null || string.IsNullOrWhiteSpace(request.Search))
            return BadRequest("Message cannot be empty.");

        var response = await useCase.Execute(request);
        if (response == null)
            return BadRequest("Failed to process message.");

        return Ok(response);
    }
}
