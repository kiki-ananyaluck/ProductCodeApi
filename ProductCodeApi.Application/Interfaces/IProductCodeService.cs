using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCodeApi.Domain.Entities;
namespace ProductCodeApi.Application.Interfaces;

public interface IProductCodeService
{
    Task<ProductCode> CreateAsync(string code);
    Task<IEnumerable<ProductCode>> GetAllAsync();
    Task<ProductCode?> GetByIdAsync(int id); 
    Task DeleteAsync(int id);
}
