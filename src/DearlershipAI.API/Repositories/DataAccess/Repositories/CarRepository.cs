using DearlershipAI.API.Models.DTOs.Responses;
using DearlershipAI.API.Models.Entities;
using DearlershipAI.API.Models.Repositories.Cars;
using DearlershipAI.API.Services.UseCases.Cars.Search;
using Microsoft.EntityFrameworkCore;

namespace DearlershipAI.API.Repositories.DataAccess.Repositories;

public class CarRepository : ICarWriteOnlyRepository, ICarReadOnlyRepository{ 
    private readonly DearlershipDbContext _dbContext;

    public CarRepository(DearlershipDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task Add(Car car) {
        await _dbContext.AddAsync(car);
    }

    public async Task<List<Car>> Search(SearchCarFilters filters) {
        var query = _dbContext.Cars.AsQueryable();

        var builder = new SearchQueryBuilder(filters);
        query = builder.BuildQuery(query, filters);

        return await query.ToListAsync();
    }
}
