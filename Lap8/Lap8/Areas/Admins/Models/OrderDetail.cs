using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson08.Lab.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        // Composite primary key configured in AppDbContext
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public float Price { get; set; }

        // Khóa ngoại liên kết
        public Orders? Orders { get; set; }
        public Product? Product { get; set; }
    }
}