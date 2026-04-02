using DearlershipAI.API.Models.DTOs.Responses;
using DearlershipAI.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DearlershipAI.API.Services.UseCases.Cars.Search;

public class SearchQueryBuilder {
    private readonly SearchCarFilters _filters;

    public SearchQueryBuilder(SearchCarFilters filters) {
        _filters = filters;
    }

    public IQueryable<Car> BuildQuery(IQueryable<Car> query, SearchCarFilters filters) {
        if (!string.IsNullOrEmpty(filters.Brand)) {
            query = query.Where(c => EF.Functions.Like(c.Brand, $"%{filters.Brand}%"));
        }

        if (!string.IsNullOrEmpty(filters.Model)) {
            query = query.Where(c => EF.Functions.Like(c.Model, $"%{filters.Model}%"));
        }

        if (!string.IsNullOrEmpty(filters.Version)) {
            query = query.Where(c => EF.Functions.Like(c.Version, $"%{filters.Version}%"));
        }

        return query;
    }
}
