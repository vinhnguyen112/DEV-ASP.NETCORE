using System.ComponentModel.DataAnnotations;

namespace Lap04.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
    }
}
