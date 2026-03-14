using System.ComponentModel.DataAnnotations;
using StudentPerformanceSystem.Features.Enrollments.Models;

namespace StudentPerformanceSystem.Features.Students.Models;

public class Student
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Birth Date")]
    public DateTime BirthDate { get; set; }
}