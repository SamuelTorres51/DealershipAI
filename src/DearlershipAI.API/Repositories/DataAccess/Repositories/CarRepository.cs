using DearlershipAI.API.Models.Entities;
using DearlershipAI.API.Models.Repositories.Cars;

namespace DearlershipAI.API.Repositories.DataAccess.Repositories;

public class CarRepository : ICarWriteOnlyRepository{ 
    private readonly DearlershipDbContext _dbContext;

    public CarRepository(DearlershipDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task Add(Car car) {
        await _dbContext.AddAsync(car);
    }
}
