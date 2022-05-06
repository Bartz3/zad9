using System.ComponentModel.DataAnnotations;

namespace zad9.Models
{
    public class Product
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Nazwa produktu")]
        public string name { get; set; }
        [Display(Name = "Cena")]
        public decimal price { get; set; }

    }
}
