using StudentPerformanceSystem.Features.Enrollments.Models;

namespace StudentPerformanceSystem.Features.Grades.Models;

public class Grade
{
    public int Id { get; set; }

    public int Score { get; set; }

    public DateTime Date { get; set; }

    public int EnrollmentId { get; set; }

    public Enrollment? Enrollment { get; set; }

    public override string ToString()
    {
        return $"Score: {Score}";
    }
}