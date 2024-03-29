using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace zad9.Pages
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public Product product { get; set; }

        IProductDB productDB;
        public DeleteModel(IProductDB _productDB)
        {
            productDB = _productDB;
        }
        public void OnGet(int _id)
        {
            product = productDB.Get(_id);
        }

        public IActionResult OnPost()
        {
            productDB.Delete(product.id);

            return RedirectToPage("Index");
        }
    }
}
