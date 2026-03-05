using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentPerformanceSystem.Features.Courses.Models;
using StudentPerformanceSystem.Features.Enrollments.Models;
using StudentPerformanceSystem.Features.Grades.Models;
using StudentPerformanceSystem.Features.Students.Models;
using StudentPerformanceSystem.Features.Teachers.Models;
using StudentPerformanceSystem.Features.Users.Models;

namespace StudentPerformanceSystem.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<Enrollment> Enrollments { get; set; }

    public DbSet<Grade> Grades { get; set; }
}