using System;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;
using zad9.Models;
using zad9.DAL;

namespace zad9
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Product product { get; set; }

        public IProductDB productDB;
        public EditModel(IProductDB _productDB)
        {
            productDB=_productDB;
        }
        public void OnGet(int _id)
        {     
            product = productDB.Get(_id);
        }

        public IActionResult OnPost()
        {
            productDB.Update(product);

            return RedirectToPage("Index");
        }

    }
}