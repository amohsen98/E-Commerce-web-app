
using E_Commerce.Entites.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class ProductVM
    {
        
        public Product Product { get; set; }
        [ValidateNever]
        
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
