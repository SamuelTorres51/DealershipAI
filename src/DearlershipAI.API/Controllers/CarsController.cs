using DearlershipAI.API.Models.DTOs.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DearlershipAI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase {
    [HttpPost]
    public async Task<IActionResult> AddCar([FromBody] RequestCarJson request,) {
    
    
    }
}
