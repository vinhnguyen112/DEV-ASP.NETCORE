using System.ComponentModel.DataAnnotations;

namespace Lap04.Models
{
    public class People
    {
        public int Id { get; set; }
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime Brithday { get; set; }
        [Display(Name = "Tiểu sử")]
        public string Bio { get; set; }
        [Display(Name = "Giới tính")]
        public byte Gender { get; set; }
    }
}
