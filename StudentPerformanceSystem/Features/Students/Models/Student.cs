using StudentPerformanceSystem.Features.Enrollments.Models;

namespace StudentPerformanceSystem.Features.Students.Models;

public class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }

    public List<Enrollment> Enrollments { get; set; } = new();

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}