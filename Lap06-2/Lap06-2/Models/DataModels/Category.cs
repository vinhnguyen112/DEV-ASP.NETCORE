using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lap06.Models.DBModel
{
    [Table("Categories")]
    public partial class Category
    {
        [DisplayName("Mã loại")]
        [Key]
        public int CategoryId { get; set; }

        [DisplayName("Tên loại")]
        [StringLength(100)]
        [Required(ErrorMessage = "Tên loại không được để trống")]
        public string CategoryName { get; set; } = null!; // Sửa lại theo chuẩn tên thuộc tính thực tế trong DB của bạn

        // Thuộc tính quan hệ
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

