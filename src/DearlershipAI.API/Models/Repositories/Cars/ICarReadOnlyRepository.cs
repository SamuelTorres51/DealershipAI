using DearlershipAI.API.Models.DTOs.Responses;
using DearlershipAI.API.Models.Entities;

namespace DearlershipAI.API.Models.Repositories.Cars;

public interface ICarReadOnlyRepository {
    Task<List<Car>> Search(SearchCarFilters filters);
}
