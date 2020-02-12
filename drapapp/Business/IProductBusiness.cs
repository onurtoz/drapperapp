using drapapp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace drapapp.Business
{
    public interface IProductBusiness
    {
        Task<ProductResponse> GetAsync(int id);
        Task<ProductResponse> GetAllAsync();
        Task<ProductResponse> GetWithCategoryAllAsync();
        Task<ProductResponse> GetCategoryIdAllAsync(int id);
        Task AddAsync(ProductRequest productRequest);
        ProductResponse GetProductResponses();
    }
}
