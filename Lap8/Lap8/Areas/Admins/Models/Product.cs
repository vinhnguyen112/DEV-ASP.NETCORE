using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lesson08.Lab.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150)]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Upload)]
        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [Required]
        [Display(Name = "Giá gốc")]
        public float Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public float SalePrice { get; set; }

        [Display(Name = "Trạng thái")]
        public byte Status { get; set; } = 1;

        [DataType(DataType.Text)]
        [StringLength(1000)]
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // khóa ngoại tới bảng Category
        public Category? Category { get; set; }
    }
}