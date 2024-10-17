using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.IO;
using System;
using E_Commerce.Data; // Make sure you have the correct namespace for your DbContext
using E_Commerce.Models; // For the Product model

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_Commerce.ViewModels;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public IActionResult Index()
        {

            return View();
            
        }
        public IActionResult GetData()
        {

            var products = _context.products.Include(p=>p.Category).ToList();
                       
            return Json(new { data = products });


        }


        // GET: Product/Index (Read All)


        // GET: Product/Create (Display Create Form)
        [HttpGet]
        public IActionResult Create()
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                //focus on this line might be problem
                CategoryList = _context.categories.Select(x => new SelectListItem
                {

                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList()



            };


            return View(productVM);
        }

        // POST: Product/Create (Create a New Product)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var ext = Path.GetExtension(file.FileName);
                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    productVM.Product.Img = @"Images\Products\" + filename + ext;
                }
                
                
                _context.products.Add(productVM.Product);
                _context.SaveChanges();
                TempData["Create"] = "Item Has Been Created Succesfully";
                return RedirectToAction("Index");   
            }
            return View(productVM);

        }
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null | Id == 0)
            {
                NotFound();
            }
            ProductVM productVM = new ProductVM()
            {
                Product = _context.products.Find(Id),
                //focus on this line might be problem
                CategoryList = _context.categories.Select(x => new SelectListItem
                {

                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList()

            }; 
            return View(productVM); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath, @"Images\Products");
                    var ext = Path.GetExtension(file.FileName);
                    if(productVM.Product.Img!= null)
                    {

                        var oldimg = Path.Combine(RootPath, productVM.Product.Img.TrimStart('\\'));
                        if (System.IO.File.Exists(oldimg)) { 
                        
                            System.IO.File.Delete(oldimg);
                        }
                    }   
                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    productVM.Product.Img = @"Images\Products\" + filename + ext;
                }


                _context.products.Update(productVM.Product);
                _context.SaveChanges();
                TempData["Update"] = "Item Has Been Created Succesfully";
                return RedirectToAction("Index");
            }
            return View(productVM.Product);

        }

        [HttpDelete]
        public IActionResult Delete(int? Id)
        {

            // Find the category in the database by Id
            var productIndb = _context.products.Find(Id);

            // Check if the category was not found
            if (productIndb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" }); // Return NotFound if category is not in the database
            }

            // Remove the found category from the database
            _context.products.Remove(productIndb);
            //remove photo from database
            var oldimg = Path.Combine(_webHostEnvironment.WebRootPath, productIndb.Img.TrimStart('\\'));
            if (System.IO.File.Exists(oldimg))
            {

                System.IO.File.Delete(oldimg);
            }

            // Save changes to persist the removal
            _context.SaveChanges();
           return  Json(new { success = true, message = "File Has Been Deleted" });
            

            // Redirect to the Index action after successful deletion
           
        }

    }


    }

