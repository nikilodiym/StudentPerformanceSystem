using StudentPerformanceSystem.Features.Enrollments.Models;

namespace StudentPerformanceSystem.Features.Students.Models;

public class Student
{
    public string FirstName { get; set; }
    public object LastName { get; set; }
    public object Id { get; set; }
    public DateTime BirthDate { get; set; }
}