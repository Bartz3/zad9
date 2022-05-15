using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace zad9.DAL
{
    public class ProductSqlDB : IProductDB
    {
        public IConfiguration configuration { get; }
        private string StoreDBcs;
        public ProductSqlDB(IConfiguration _configuration)
        {
            configuration = _configuration;
            StoreDBcs = configuration.GetConnectionString("StoreDB");
        }

        public List<Product> List()
        {
            List<Product> productList = new List<Product>();

            SqlConnection con = new SqlConnection(StoreDBcs);
            SqlCommand cmd = new SqlCommand("sp_productDisplay", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Product _product;
            while (reader.Read())
            {
                _product = new Product();
                _product.id = int.Parse(reader["Id"].ToString());
                _product.name = reader["Name"].ToString();
                _product.price = Decimal.Parse(reader["Price"].ToString());

                productList.Add(_product);
            }
            reader.Close(); con.Close();
     
            return productList;
        }
        public Product Get(int _id)
        {

            SqlConnection con = new SqlConnection(StoreDBcs);
            SqlCommand cmd = new SqlCommand("sp_productGet", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Product _product =new Product();
            cmd.Parameters.AddWithValue("@Id", _product.id);

            while (reader.Read())
            {
                _product.id = int.Parse(reader["Id"].ToString());
                if (_product.id == _id)
                {
                    _product.name = reader["Name"].ToString();
                    _product.price = Decimal.Parse(reader["Price"].ToString());
                    _product.id = _id;
                    break;
                }

            }
            reader.Close(); con.Close();
            return _product;
        }
        public int Add(Product _product)
        {
            throw new NotImplementedException();
        }

        public int Delete(int _id)
        {
            throw new NotImplementedException();
        }

        public int Update(Product _product)
        {
            
            SqlConnection con = new SqlConnection(StoreDBcs);

            SqlCommand cmd = new SqlCommand("sp_productUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter name_SqlParam = new SqlParameter("@name", SqlDbType.VarChar,
            50);
            name_SqlParam.Value = _product.name;
            cmd.Parameters.Add(name_SqlParam);

            SqlParameter price_SqlParam = new SqlParameter("@price", SqlDbType.Money);
            price_SqlParam.Value = _product.price;
            cmd.Parameters.Add(price_SqlParam);

            SqlParameter productID_SqlParam = new SqlParameter("@productID",
            SqlDbType.Int);
            //productID_SqlParam.Direction = ParameterDirection.Input;
            productID_SqlParam.Value = _product.id;
            cmd.Parameters.Add(productID_SqlParam);

            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();

            return 1;
        }
    }
}
