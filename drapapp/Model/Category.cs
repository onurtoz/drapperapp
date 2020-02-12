using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace drapapp.Model
{
    public class Category
    {
        public int CATGEGORYID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public DateTime CREATED { get; set; }
        public DateTime MODIFIED { get; set; }
        public List<Product> product { get; set; } 
    }
}
