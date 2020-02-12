using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using drapapp.Business;
using drapapp.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace drapapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ProductsController : Controller
    {
        private readonly IProductBusiness _productBusiness;

        public ProductsController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        // GET api/v1/products/{id}
        [HttpPost]
        [Route("[action]")]
        public async Task<ProductResponse> GetAllCategoryId(int id)
        {
            return await _productBusiness.GetCategoryIdAllAsync(id);
        }

   
        [HttpGet]
        [Route("[action]")]
        public async Task<ProductResponse> GetAllCategoryWithProduct()
        {
            return await _productBusiness.GetWithCategoryAllAsync();
        }

        [HttpGet]
        [Route("[action]")]
        public ProductResponse GetProducts()
        {
            return _productBusiness.GetProductResponses();
        }

        [ProducesResponseType(201)]
        [HttpPost]
        [Route("[action]")]
        public async Task Post([FromBody]ProductRequest productRequest)
        {
            await _productBusiness.AddAsync(productRequest);
        }
    }
}
