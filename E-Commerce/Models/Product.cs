

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Product
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [DisplayName("Image")]
        [ValidateNever]
        
        public string Img { get; set; }
        
        public string Price { get; set; }

        
        [DisplayName("Category")]
        
        
        
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }      



       

       }
    }


