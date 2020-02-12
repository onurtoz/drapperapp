using drapapp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace drapapp.Data
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(long id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetProductCategoryIdAsync(int id);
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        Task AddAsync(Product product);
        List<Product> GetProducts();
    }
}
