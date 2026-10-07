using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lap09.Areas.Admins.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Image { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // danh sách sản phẩm theo danh mục
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
