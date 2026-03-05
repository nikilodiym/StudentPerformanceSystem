using StudentPerformanceSystem.Features.Courses.Models;
using StudentPerformanceSystem.Features.Grades.Models;
using StudentPerformanceSystem.Features.Students.Models;

namespace StudentPerformanceSystem.Features.Enrollments.Models;

public class Enrollment
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public Student? Student { get; set; }

    public int CourseId { get; set; }

    public Course? Course { get; set; }

    public Grade? Grade { get; set; }

    public override string ToString()
    {
        return $"Student {StudentId} -> Course {CourseId}";
    }
}