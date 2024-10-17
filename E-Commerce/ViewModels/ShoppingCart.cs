using E_Commerce.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class ShoppingCart
    {
        public Product Product { get; set; }

        [Range(1,100, ErrorMessage ="you must enter value between 1 to 100  ")]
        public int Count { get; set; }  
    }
}
