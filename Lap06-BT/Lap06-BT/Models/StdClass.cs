using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class StdClass
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên lớp không được để trống")]
    [StringLength(100)]
    public string CLassName { get; set; }

    // Navigation property
    [ValidateNever]
    public virtual ICollection<Student>? Students { get; set; } = new List<Student>();
}