using StudentPerformanceSystem.Features.Enrollments.Models;

namespace StudentPerformanceSystem.Features.Students.Models;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}