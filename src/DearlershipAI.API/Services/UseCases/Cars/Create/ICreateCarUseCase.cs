using DearlershipAI.API.Models.DTOs.Requests;
using DearlershipAI.API.Models.Entities;

namespace DearlershipAI.API.Services.UseCases.Cars.Create;

public interface ICreateCarUseCase {
    Task<Car> Execute(RequestCarJson request);
}
