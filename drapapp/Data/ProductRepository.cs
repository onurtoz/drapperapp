using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using drapapp.Model;
using Dapper.Oracle;

namespace drapapp.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        private readonly string _connectionOracleString;

        private IDbConnection _sqlconnection { get { return new SqlConnection(_connectionString); } }
        private IDbConnection _oracleconnection { get { return new OracleConnection(_connectionOracleString); } }

        public ProductRepository()
        {
            // TODO: It will be refactored...
            _connectionString = "Server=N108388;Database=TTGrapQL;Trusted_Connection=True;MultipleActiveResultSets=true";
            _connectionOracleString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User Id = hwadmin; Password = Asdf1234; ";
            //string con = "Data Source=(DESCRIPTION =(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST = 000.00.0.00)(PORT = 0000)))(CONNECT_DATA =(SERVICE_NAME = database)));User ID=User/Schema;Password=password;Unicode=True";
        }

        public async Task<Product> GetAsync(long id)
        {
            if (_oracleconnection.State == ConnectionState.Closed)
            {
                _oracleconnection.Open();
            }
            using (IDbConnection dbConnection = _oracleconnection)
            {
                string query = @"SELECT ProductId,CategoryId,Name
                                ,[Description]
                                ,[Price]
                                ,[Created]
                                ,[Modified]
                                FROM [Product]
                                WHERE [ProductId] = @Id";

                var product = await dbConnection.QueryFirstOrDefaultAsync<Product>(query, new { @Id = id });

                return product;
            }
        }

        public List<Product> GetProducts()
        {

            string query = @"SELECT * FROM Product p INNER JOIN Category a ON p.CATEGORYID = a.CATGEGORYID";
            List<Product> products = new List<Product>();
            Product product;
            if (_oracleconnection.State == ConnectionState.Closed)
            {
                _oracleconnection.Open();
            }
            using (IDbConnection dbConnection = _oracleconnection)
            {
                IEnumerable<IDictionary<string, object>> rows;
                rows = dbConnection.Query(query).Cast<IDictionary<string, object>>();
                foreach (var row in rows)
                {
                    product = new Product();
                    product.PRODUCTID = (int)row["PRODUCTID"];
                    product.CATEGORYID = (int)row["CATEGORYID"];
                    product.PRICE = (decimal)row["PRICE"];
                    product.NAME = row["NAME"].ToString();
                    product.DESCRIPTION = row["DESCRIPTION"].ToString();
                    product.CREATED = (DateTime)row["CREATED"];
                    product.MODIFIED = (DateTime)row["MODIFIED"];
                    products.Add(product);
                   
                }
               
            }
            return products;
        }

        //use
        public async Task<IEnumerable<Product>> GetAllAsync()
        {

            if (_oracleconnection.State == ConnectionState.Closed)
            {
                _oracleconnection.Open();
            }
            //TODO: Paging...
            using (IDbConnection dbConnection = _oracleconnection)
            {

                string query = @"Select * FROM Product";


                var product = await dbConnection.QueryAsync<Product>(query);

                return product;
            }
        }
         // use
        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
            using (IDbConnection dbConnection = _oracleconnection)
            {
                var product = await dbConnection.QueryAsync<Product, Category, Product>(

                    @"SELECT *
                    FROM Product p 
                    INNER JOIN Category a ON p.CATEGORYID = a.CATGEGORYID",

                     (u, c) =>
                    {
                        u.Category = c;
                        return u;
                    },
                       splitOn: "CATGEGORYID"
                       );
               
             
                return product;
            }
        }


        public async Task<IEnumerable<Product>> GetProductCategoryIdAsync(int id)
        {
            using (IDbConnection dbConnection = _oracleconnection)
            {
                var product = await dbConnection.QueryAsync<Product, Category, Product>(

                    @"SELECT *
                    FROM Product p 
                    INNER JOIN Category a ON p.CATEGORYID = a.CATGEGORYID 
                    where PRODUCTID = :ProductId",

                    map: (u, c) =>
                    {
                        u.Category = c;

                        return u;
                    },
                       splitOn: "CATGEGORYID",
                       param: new { @ProductId = id }
                       );

                return product;
            }
        }

        public async Task AddAsync(Product product)
        {
            using (IDbConnection dbConnection = _oracleconnection)
            {

                var param = new DynamicParameters();

                param.Add(name: "PRODUCTID", value: product.PRODUCTID, direction: ParameterDirection.Input);
                param.Add(name: "CATEGORYID", value: product.CATEGORYID, direction: ParameterDirection.Input);
                param.Add(name: "NAME", value: product.NAME, direction: ParameterDirection.Input);
                param.Add(name: "DESCRIPTION", value: product.DESCRIPTION, direction: ParameterDirection.Input);
                param.Add(name: "PRICE", value: product.DESCRIPTION, direction: ParameterDirection.Input);
                param.Add(name: "CREATED", value: product.DESCRIPTION, direction: ParameterDirection.Input);
                param.Add(name: "MODIFIED", value: product.DESCRIPTION, direction: ParameterDirection.Input);


                //param.Add(name: "DESCRIPTION", dbType: DbType.Int32, direction: ParameterDirection.Output);
                string query = @"INSERT INTO Product (
                                PRODUCTID,
                                CATEGORYID,
                                NAME,
                                DESCRIPTION,
                                PRICE,
                                CREATED,
                                MODIFIED) VALUES (
                                :PRODUCTID,
                                :CATEGORYID,
                                :NAME,
                                :DESCRIPTION,
                                :PRICE,
                                :CREATED,
                                :MODIFIED)";

                try
                {
                    await dbConnection.ExecuteAsync(query, product);
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    throw;
                }
                
            }
        }
    }
}
