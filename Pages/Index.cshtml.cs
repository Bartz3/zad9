using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;
using zad9.Models;
using zad9.DAL;
namespace zad9.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Product> productList;
        IProductDB productDB;
        public IndexModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }
        
        public void OnGet()
        {
            productList = productDB.List();
        }


        //private Product XmlNode2Product(XmlNode node) // Zamienia produkt z XML do obiektu
        //{
        //    Product p = new Product();
        //    p.id = int.Parse(node.Attributes["id"].Value);
        //    p.name = node["name"].InnerText;
        //    p.price = decimal.Parse(node["price"].InnerText);
        //    return p;
        //}
    }
}