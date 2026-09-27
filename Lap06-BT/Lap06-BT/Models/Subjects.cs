using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class Subjects
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên môn học không được để trống")]
    [StringLength(100)]
    public string SubjectName { get; set; }

    // Navigation property
    [ValidateNever]
    public virtual ICollection<Marks>? Marks { get; set; } = new List<Marks>();
}