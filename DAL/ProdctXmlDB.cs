using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using zad9.Models;

namespace zad9.DAL
{
    public class ProductXmlDB : IProductDB
    {
        XmlDocument db = new XmlDocument();
        string xmlDB_path;
        private XmlDocument doc = new XmlDocument();
        public ProductXmlDB(IConfiguration _configuration)
        {
            xmlDB_path = _configuration.GetValue<string>("AppSettings:XmlDB_path"); // odczytanie pliku konfiguracyjnego
            LoadDB();
        }
        private void LoadDB()
        {
            db.Load(xmlDB_path);
        }

        public List<Product> List()
        {
            List<Product> productList = new List<Product>();
            XmlNodeList productXmlNodeList = db.SelectNodes("/store/product");

            foreach (XmlNode productXmlNode in productXmlNodeList)
            {
                productList.Add(XmlNodeProduct2Product(productXmlNode));
            }
            return productList;
        }
        public Product Get(int _id) // Funkcja zwracająca Produkt o danym id
        {
            List<Product> productList = List();
            Product p = new Product();

            foreach (var product in productList)
            {
                if(product.id==_id)
                    p=product;
            }
            return p;

           
                //Product p = new Product();
                //OpenXmlBase();
                //XmlNode node = XmlNodeProductGet(_id);
                //return XmlNodeProduct2Product(node);
            
        }

        private Product XmlNodeProduct2Product(XmlNode node)
        {
            Product p = new Product();
            p.id = int.Parse(node.Attributes["id"].Value);
            p.name = node["name"].InnerText;
            p.price = decimal.Parse(node["price"].InnerText);
            return p;
        }
        public int Update(Product _product)
        {
            
            OpenXmlBase();
            XmlNode node = XmlNodeProductGet(_product.id);
            node["name"].InnerText = _product.name;
            node["price"].InnerText = _product.price.ToString();
            SaveXmlBase();

            return 0;
        }
        private XmlNode XmlNodeProductGet(int _id)
        {
            XmlNode node = null;
            XmlNodeList list = doc.SelectNodes("/store/product[@id=" + _id.ToString() +
           "]");
            node = list[0];
            return node;
        }
        private void OpenXmlBase()
        {
            doc.Load("DATA/store.xml");
        }
        private void SaveXmlBase()
        {
            doc.Save("DATA/store.xml");
        }
        public int Delete(int _id)
        { return 0; }
        public int Add(Product _product)
        { return 0; }
    }
}