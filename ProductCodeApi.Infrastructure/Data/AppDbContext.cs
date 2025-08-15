using Microsoft.EntityFrameworkCore;
using ProductCodeApi.Domain.Entities;

namespace ProductCodeApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<ProductCode> ProductCodes => Set<ProductCode>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
