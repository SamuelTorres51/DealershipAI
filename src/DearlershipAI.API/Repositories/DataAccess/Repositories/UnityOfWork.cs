using DearlershipAI.API.Models.Repositories;

namespace DearlershipAI.API.Repositories.DataAccess.Repositories;

public class UnityOfWork : IUnityOfWork{
    private readonly DearlershipDbContext _dbContext;

    public UnityOfWork(DearlershipDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
