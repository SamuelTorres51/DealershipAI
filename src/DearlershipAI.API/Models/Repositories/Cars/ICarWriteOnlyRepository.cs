using DearlershipAI.API.Models.Entities;

namespace DearlershipAI.API.Models.Repositories.Cars;

public interface ICarWriteOnlyRepository {
    Task Add(Car car);
}
