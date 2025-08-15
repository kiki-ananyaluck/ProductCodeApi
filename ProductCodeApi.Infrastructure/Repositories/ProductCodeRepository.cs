using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCodeApi.Application.Interfaces;
using ProductCodeApi.Domain.Entities;
using ProductCodeApi.Infrastructure.Data;

namespace ProductCodeApi.Infrastructure.Repositories;

public class ProductCodeRepository : IProductCodeRepository
{
    private readonly AppDbContext _db;

    public ProductCodeRepository(AppDbContext db) => _db = db;

    public async Task<bool> ExistsAsync(string normalizedCode)
    {
        return await _db.ProductCodes.AnyAsync(x => x.Code == normalizedCode && x.DeletedAt == null);
    }

    public async Task<ProductCode> AddAsync(ProductCode entity)
    {
        _db.ProductCodes.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<ProductCode>> GetAllAsync()
    {
        return await _db.ProductCodes
            .Where(x => x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<ProductCode?> GetByIdAsync(int id)
    {
        return await _db.ProductCodes.FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);
    }

    public async Task UpdateAsync(ProductCode entity)
    {
        _db.ProductCodes.Update(entity);
        await _db.SaveChangesAsync();
    }


}
