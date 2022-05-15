using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using zad9.Models;

namespace zad9.DAL
{
    public class ProductXmlDB : IProductDB
    {
        XmlDocument db = new XmlDocument();
        string xmlDB_path;
        
        public ProductXmlDB(IConfiguration _configuration)
        {
            xmlDB_path = _configuration.GetValue<string>("AppSettings:XmlDB_path"); // odczytanie pliku konfiguracyjnego, ścieżka do DATA/store.xml
            LoadDB();
        }

        /// <summary>
        ///  Funkcja zwracająca listę produktów
        /// </summary>
        /// <returns> Lista produktów</returns>
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
        /// <summary>
        /// Funkcja zwracająca produkt o danym id
        /// </summary>
        public Product Get(int _id) 
        {
            List<Product> productList = List();
            Product p = new Product(); 

            foreach (var product in productList)        //Product p = new Product();                                                                 
            {                                           //OpenXmlBase();
                if (product.id==_id)                    //XmlNode node = XmlNodeProductGet(_id);
                    p =product;                         //return XmlNodeProduct2Product(node);
            }
            return p;    
         
        }

        /// <summary>
        /// Funkcja modyfikująca własności produktu
        /// </summary>
        public int Update(Product _product)
        {
            OpenXmlBase();
            XmlNode node = XmlNodeProductGet(_product.id);
            node["name"].InnerText = _product.name;
            node["price"].InnerText = _product.price.ToString();
            SaveXmlBase();

            return 0;
        }
        public int Delete(int _id)
        {
            int xd = getID();

            db.Load("DATA/store.xml");

            var root = db.SelectSingleNode("store");
            //"product[@id=5]
            string nodeID = "product[@id=" +_id.ToString() +"]";

            var deleteNode = root.SelectSingleNode(nodeID);
            root.RemoveChild(deleteNode);

            db.Save("DATA/store.xml");
            return 0;
        }
        public int Add(Product _product)
        {
            db.Load("DATA/store.xml");

            string newId = getID().ToString();

            XmlNode root = db.SelectSingleNode("store");
            XmlElement product = db.CreateElement("product");
            root.AppendChild(product);


            XmlAttribute id = db.CreateAttribute("id");
            //id.Value = getID().ToString();
            //id.Value = db.SelectNodes("store/product").Count.ToString();
            id.Value = newId;
            product.Attributes.Append(id);

            XmlElement name = db.CreateElement("name");
            name.InnerText = _product.name;
            product.AppendChild(name);      

            XmlElement price = db.CreateElement("price");
            price.InnerText = _product.price.ToString();
            product.AppendChild(price);

            db.Save("DATA/store.xml");

            return 0; 
        }

        /// <summary>
        /// Funkcja rzutująca Xml -> Product
        /// </summary>
        private Product XmlNodeProduct2Product(XmlNode node) 
        {
            Product p = new Product();
            p.id = int.Parse(node.Attributes["id"].Value);
            p.name = node["name"].InnerText;
            p.price = decimal.Parse(node["price"].InnerText);
            return p;
        }


        /// <summary>
        /// Funkcja zwracająca produkt w formacie XML o danym ID
        /// </summary>
        /// <returns>XmlNode </returns>
        private XmlNode XmlNodeProductGet(int _id) 
        {
            XmlNode node = null;
            XmlNodeList list = db.SelectNodes("/store/product[@id=" + _id.ToString() + "]");
            node = list[0];

            return node;
        }
        private void OpenXmlBase() /// Otwarcie bazy Xml
        {
            db.Load("DATA/store.xml");
        }
        private void SaveXmlBase() /// Zapisanie bazy Xml
        {
            db.Save("DATA/store.xml");
        }

        private void LoadDB() ///Załadowanie bazy
        {
            db.Load(xmlDB_path);
        }
        
        private int getID()
        {
            List<Product> productList = List();

            int newID = productList.Last().id + 1;
            return newID;
        }
    }
}