using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCodeApi.Application.Interfaces;
using ProductCodeApi.Application.Validators;
using ProductCodeApi.Domain.Entities;

namespace ProductCodeApi.Application.Services;

public class ProductCodeService : IProductCodeService
{
    private readonly IProductCodeRepository _repo;

    public ProductCodeService(IProductCodeRepository repo) => _repo = repo;

    public async Task<ProductCode> CreateAsync(string code)
    {
        var normalized = code;
        if (await _repo.ExistsAsync(normalized))
            throw new InvalidOperationException("Code already exists");

        var entity = new ProductCode { Code = normalized };
        return await _repo.AddAsync(entity);
    }

    public async Task<IEnumerable<ProductCode>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity != null)
        {
            entity.DeletedAt = DateTimeOffset.UtcNow;
            entity.IsActive = false;
            await _repo.UpdateAsync(entity);
        }
    }

    public async Task<ProductCode?> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }



}
