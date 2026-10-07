using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson08.Lab.Models
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        [Display(Name = "Mã bài viết")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(100)]
        [Display(Name = "Tiêu đề")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Trạng thái")]
        public byte Status { get; set; } = 1;

        [Display(Name = "Lượt xem")]
        public int ViewCount { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [StringLength(1500)]
        [DataType(DataType.Text)]
        [Display(Name = "Nội dung")]
        public string? Description { get; set; }
    }
}