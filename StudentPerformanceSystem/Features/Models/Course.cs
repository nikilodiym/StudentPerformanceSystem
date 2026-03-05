using StudentPerformanceSystem.Features.Enrollments.Models;
using StudentPerformanceSystem.Features.Teachers.Models;

namespace StudentPerformanceSystem.Features.Courses.Models;

public class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int TeacherId { get; set; }

    public Teacher Teacher { get; set; } = default!;

    public List<Enrollment> Enrollments { get; set; } = new();

    public override string ToString()
    {
        return Title;
    }
}