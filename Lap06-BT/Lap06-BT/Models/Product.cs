using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lap06_BT.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150, ErrorMessage = "Tên sản phẩm giới hạn 150 ký tự")]
        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        public float Price { get; set; }

        public float SalePrice { get; set; }
        public byte Status { get; set; }

        [StringLength(1000, ErrorMessage = "Nội dung mô tả giới hạn 1000 ký tự")]
        [Column(TypeName = "ntext")]
        public string Descriptions { get; set; }

        [Required(ErrorMessage = "Danh mục sản phẩm không được để trống")]
        public int CategoryId { get; set; }

        public DateTime CreatedDate { get; set; }



        public Category? Category { get; set; }

    }

}
