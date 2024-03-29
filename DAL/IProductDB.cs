﻿using zad9.Models;

namespace zad9.DAL
{
    public interface IProductDB
    {
        public List<Product> List();
        public Product Get(int _id);
        public int Update(Product _product);
        public int Delete(int _id);
        public int Add(Product _product);
    }

}
