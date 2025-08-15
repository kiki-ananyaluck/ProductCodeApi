using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProductCodeApi.Domain.Entities;

namespace ProductCodeApi.Application.Interfaces;

public interface IProductCodeRepository
{
    Task<bool> ExistsAsync(string normalizedCode);
    Task<ProductCode> AddAsync(ProductCode entity);
    Task<IEnumerable<ProductCode>> GetAllAsync();
    Task<ProductCode?> GetByIdAsync(int id);
    Task UpdateAsync(ProductCode entity);

}
