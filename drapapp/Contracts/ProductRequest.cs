using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace drapapp.Contracts
{
    public class ProductRequest
    {
        public int CategoryId { get; set; }
        public int PRODUCTID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modifed { get; set; }
    }
}
