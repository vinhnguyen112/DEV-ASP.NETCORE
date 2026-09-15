using Microsoft.AspNetCore.Mvc;
using modelControl.Models;

namespace modelControl.Controllers
{

    [Route("/sanpham")]
    public class ProductController : Controller
    {
  
        public IActionResult Index()
        {
            ViewBag.MessageVB = "Product DetailsWB";
            ViewData["MessageVD"] = "Product DetailsWD";
            TempData["MessageTD"] = "Product DetailsTD";
            return View();
        }


        [Route("chitietsanpham")]
        public IActionResult GetAllProduct()
        {

                List<Product> products = new List<Product>
                {
                    new Product { Id = 1, Name = "Product 1", YearRelease = 2023, Price = 99.99f },
                    new Product { Id = 2, Name = "Product 2", YearRelease = 2022, Price = 149.99f },
                    new Product { Id = 3, Name = "Product 3", YearRelease = 2021, Price = 199.99f }
                };
            ViewBag.Product = products;
            // Logic to retrieve product details based on the provided id
            return View("GetAllProduct");
        }

        [Route("seach")]
        public IActionResult GetSeachProduct(string Name)
        {

            List<Product> products = new List<Product>
                {
                    new Product { Id = 1, Name = "iphone 1", YearRelease = 2023, Price = 99.99f },
                    new Product { Id = 2, Name = "Product 2", YearRelease = 2022, Price = 149.99f },
                    new Product { Id = 3, Name = "Product 3", YearRelease = 2021, Price = 199.99f }

                };

            List<Product> searchResults = products.Where(p => p.Name.ToLower().Contains(Name.ToLower())).ToList();
            ViewBag.Product = searchResults;
            // Logic to retrieve product details based on the provided id
            return View();
        }
    }
}
