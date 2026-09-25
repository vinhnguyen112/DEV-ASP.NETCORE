using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lap06.Models.DBModel
{
    [Table("Publishers")]
    public partial class Publisher
    {
        [DisplayName("Mã nhà xuất bản")]
        [Key]
        public int PublisherId { get; set; }

        [DisplayName("Tên nhà xuất bản")]
        [StringLength(100)]
        [Required(ErrorMessage = "Tên nhà xuất bản không được để trống")]
        public string PublisherName { get; set; } = null!;

        [DisplayName("Điện thoại")]
        [StringLength(10)]
        public string? Phone { get; set; }

        [DisplayName("Địa chỉ")]
        [StringLength(200)]
        public string? Address { get; set; }

        // Thuộc tính quan hệ
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
