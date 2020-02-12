using drapapp.Contracts;
using drapapp.Data;
using drapapp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace drapapp.Business
{
    public class ProductBusiness: IProductBusiness
    {
        private readonly IProductRepository _productRepository;

        public ProductBusiness(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<ProductResponse> GetAsync(int id)
        {
            ProductResponse productResponse = new ProductResponse();
            var product = await _productRepository.GetAsync(id);

            if (product == null)
            {
                productResponse.Message = "Product not found.";
            }
            else
            {
                productResponse.Products.Add(product);
            }

            return productResponse;
        }

        public async Task<ProductResponse> GetAllAsync()
        {
            ProductResponse productResponse = new ProductResponse();
            IEnumerable<Product> products = await _productRepository.GetAllAsync();

            if (products.ToList().Count == 0)
            {
                productResponse.Message = "Products not found.";
            }
            else
            {
                productResponse.Products.AddRange(products);
            }

            return productResponse;
        }

        public async Task<ProductResponse> GetWithCategoryAllAsync( )
        {
            ProductResponse productResponse = new ProductResponse();
            IEnumerable<Product> products = await _productRepository.GetAllWithCategoryAsync();

            if (products.ToList().Count == 0)
            {
                productResponse.Message = "Products not found.";
            }
            else
            {
                productResponse.Products.AddRange(products);
            }

            return productResponse;
        }
        public ProductResponse GetProductResponses()
        {
            ProductResponse productResponse = new ProductResponse();
            List<Product> products = _productRepository.GetProducts();

            if (products.ToList().Count == 0)
            {
                productResponse.Message = "Products not found.";
            }
            else
            {
                productResponse.Products.AddRange(products);
            }

            return productResponse;
        }
        public async Task<ProductResponse> GetCategoryIdAllAsync(int id)
        {
            ProductResponse productResponse = new ProductResponse();
            IEnumerable<Product> products = await _productRepository.GetProductCategoryIdAsync(id);

            if (products.ToList().Count == 0)
            {
                productResponse.Message = "Products not found.";
            }
            else
            {
                productResponse.Products.AddRange(products);
            }

            return productResponse;
        }

        public async Task AddAsync(ProductRequest productRequest)
        {
            Product product = new Product()
            {
                PRODUCTID = productRequest.PRODUCTID,
                CATEGORYID = productRequest.CategoryId,
                NAME = productRequest.Name,
                DESCRIPTION = productRequest.Description,
                PRICE = productRequest.Price,
                CREATED = productRequest.Created,
                MODIFIED = productRequest.Modifed
            };

            await _productRepository.AddAsync(product);
        }
    }
}

