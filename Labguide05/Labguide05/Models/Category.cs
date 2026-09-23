using System.ComponentModel.DataAnnotations;

namespace Labguide05.Models
{
    public class Category
    {
        [Key]

        public int Id { get; set; }

        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, MinimumLength =6, ErrorMessage ="Tên danh mục phải từ 6 - 100 ký tự")]
        public string Name { get; set; }

    }
}
