using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace drapapp.Model
{
    public class Product
    {
        public int PRODUCTID { get; set; }
        public int CATEGORYID { get; set; }

        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal PRICE { get; set; }
        public DateTime CREATED { get; set; }
        public DateTime MODIFIED { get; set; }

        public Category Category { get; set; }



        //PRODUCTID = '2', CATEGORYID = '2', PRICE = '1444', NAME = 'Apple Macbook Air', DESCRIPTION = '2.3GHZ 8GB RAM', CREATED = '3.05.2003 21:02:44', MODIFIED = '3.05.2003 21:02:44'}
    }
}
