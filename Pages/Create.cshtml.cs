using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace zad9.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Product product { get; set; }

        IProductDB productDB;

        public CreateModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            
            productDB.Add(product);

            //return Page();
            return RedirectToPage("Index");
        }
    }
}
