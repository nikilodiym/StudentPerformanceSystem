using StudentPerformanceSystem.Features.Courses.Models;

namespace StudentPerformanceSystem.Features.Teachers.Models;

public class Teacher
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public List<Course> Courses { get; set; } = new();

    public override string ToString()
    {
        return $"{Name} ({Department})";
    }
}