﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Entites.Models;

public class ApplicationUser : IdentityUser
{
    
    [Required]
    public string Name { get; set; }
    public string Address { get; set; }
    public string City {  get; set; }   


}
