using DearlershipAI.API.Models.DTOs.Requests;
using DearlershipAI.API.Models.DTOs.Responses;

namespace DearlershipAI.API.Services.UseCases.Cars.Search;

public interface ISearchCarUseCase {
    Task<ResponseCarsJson?> Execute(RequestSearchCarJson request);
}
