using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lesson08.Lab.Models
{
    [Table("Banner")]
    public class Banner
    {
        [Key]
        [Display(Name = "Mã banner")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên banner không được để trống")]
        [StringLength(100)]
        [Display(Name = "Tên banner")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Trạng thái")]
        public byte Status { get; set; } = 1;

        [Display(Name = "Độ ưu tiên")]
        public int Prioty { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }
    }
}