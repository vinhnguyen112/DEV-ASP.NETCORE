using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class Marks
{
    [Key, Column(Order = 0)]
    public int SubjectId { get; set; }

    [ForeignKey("SubjectId")]
    [ValidateNever]
    public virtual Subjects? Subject { get; set; }

    [Key, Column(Order = 1)]
    public int StudentId { get; set; }

    [ForeignKey("StudentId")]
    [ValidateNever]
    public virtual Student? Student { get; set; }

    [Required]
    public float Score { get; set; }
}