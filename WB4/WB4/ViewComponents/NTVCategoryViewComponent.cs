using Microsoft.AspNetCore.Mvc;

namespace WB4.ViewComponents
{
    public class NTVCategoryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<Models.NTVCategory> categories = new List<Models.NTVCategory>
            {
                new Models.NTVCategory { CategoryId = 1, CategoryName = "Điện tử" },
                new Models.NTVCategory { CategoryId = 2, CategoryName = "Điện lạnh" },
                new Models.NTVCategory { CategoryId = 3, CategoryName = "Đồ gia dụng" },
                new Models.NTVCategory { CategoryId = 4, CategoryName = "Tiện ích" },
            };

            return View("Default", categories);
        }
    }
}