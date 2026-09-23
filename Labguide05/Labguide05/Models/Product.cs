using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Labguide05.Models
{
    public class Product : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Tên sản phẩm phải từ 6 đến 150 ký tự")]
        public string Name { get; set; }

        [Display(Name = "Ảnh sản phẩm")]
        public string Image { get; set; } // Luu đuong dan anh

        [Display(Name = "Giá bán")]
        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(100000, double.MaxValue, ErrorMessage = "Giá sản phẩm phải tối thiểu từ 100,000 VNĐ")]
        public float Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        [Required(ErrorMessage = "Giá khuyến mãi không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá khuyến mãi không được nhỏ hơn 0")]
        public float SalePrice { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Mô tả sản phẩm không được để trống")]
        [MaxLength(1500, ErrorMessage = "Mô tả không được vượt quá 1500 ký tự")]
        public string Description { get; set; }

        [Display(Name = "Danh mục")]
        [Required(ErrorMessage = "Vui lòng chọn danh mục sản phẩm")]
        public int CategoryId { get; set; }

        // Logic Custom Validation cho SalePrice và Description
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 1. Kiểm tra SalePrice nhỏ hơn giá chuẩn 10% (SalePrice < Price * 0.9)
            if (SalePrice >= Price * 0.9f)
            {
                yield return new ValidationResult(
                    "Giá khuyến mãi phải nhỏ hơn giá chuẩn ít nhất 10% (nhỏ hơn " + (Price * 0.9f).ToString("N0") + " VNĐ)",
                    new[] { nameof(SalePrice) }
                );
            }

            // 2. Kiểm tra các từ nhạy cảm trong Description
            string[] badWords = { "die", "admin", "fack" };
            if (!string.IsNullOrEmpty(Description))
            {
                foreach (var word in badWords)
                {
                    if (Description.ToLower().Contains(word.ToLower()))
                    {
                        yield return new ValidationResult(
                            $"Mô tả không được chứa từ nhạy cảm: '{word}'",
                            new[] { nameof(Description) }
                        );
                    }
                }
            }
        }
    }
}