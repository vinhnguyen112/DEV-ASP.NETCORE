using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên sinh viên không được để trống")]
    [StringLength(100)]
    public string StudentName { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [StringLength(100)]
    public string StudentEmail { get; set; }

    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    [StringLength(50)]
    public string StudentPhone { get; set; }

    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    [StringLength(150)]
    public string StudentAddress { get; set; }

    [Required(ErrorMessage = "Ảnh đại diện không được để trống")]
    [StringLength(100)]
    public string StudentAvatar { get; set; }

    [Required(ErrorMessage = "Ngày sinh không được để trống")]
    [DataType(DataType.Date)]
    [Column(TypeName = "date")]
    public DateTime StudentBirthday { get; set; }

    [Required]
    public int ClassId { get; set; }

    [ForeignKey("ClassId")]
    [ValidateNever]
    public virtual StdClass? StdClass { get; set; }

    [ValidateNever]
    public virtual ICollection<Marks>? Marks { get; set; } = new List<Marks>();
}