using Microsoft.AspNetCore.Mvc;
using WB5.Models;

namespace WB5.ViewComponents
{
    public class ProductListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Price = 1250000, ImageUrl = "/img/13.png" },
                new Product { Id = 2, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Price = 1250000, ImageUrl = "/img/04.png" },
                new Product { Id = 3, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Price = 1250000, ImageUrl = "/img/07.png" }
            };

            return View("~/Views/Shared/Components/ProductList/Default.cshtml", products);
        }
    }
}