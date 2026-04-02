using DearlershipAI.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DearlershipAI.API.Repositories.DataAccess;

public class DearlershipDbContext : DbContext{
    public DearlershipDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Car> Cars { get; set; }
}
