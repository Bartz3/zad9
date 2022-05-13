using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zad9.Models;

using zad9.DAL;
namespace zad9.Pages.Shared
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public Product product { get; set; }
        //public ProductXmlDB pxmldb = new ProductXmlDB(IConfiguration _configuration);

        public void OnGet(int _id)
        {
            //product = ProductXmlDB

        }
    }
}
