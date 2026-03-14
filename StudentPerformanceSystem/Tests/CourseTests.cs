using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPerformanceSystem.Data;
using StudentPerformanceSystem.Features.Courses.Models;
using StudentPerformanceSystem.Features.Teachers.Models;

namespace StudentPerformanceSystem.Tests;

public class CourseTests
{
    [Xunit.Fact]
    public async Task OnGetAsync_LoadsCoursesWithTeachers()
    {
        await using var dbContext = CreateDbContext();
        var teacher = new Teacher { Name = "Olena Marchenko", Department = "Math" };
        dbContext.Teachers.Add(teacher);
        await dbContext.SaveChangesAsync();

        dbContext.Courses.Add(new Course { Title = "Algebra", TeacherId = teacher.Id });
        await dbContext.SaveChangesAsync();

        var pageModel = new StudentPerformanceSystem.Features.Courses.Index(dbContext);

        await pageModel.OnGetAsync();

        Xunit.Assert.True(pageModel.HasTeachers);
        Xunit.Assert.Single(pageModel.CourseList);
        Xunit.Assert.Equal("Algebra", pageModel.CourseList[0].Title);
        Xunit.Assert.Equal("Olena Marchenko", pageModel.CourseList[0].Teacher.Name);
    }

    [Xunit.Fact]
    public async Task OnPostAsync_WithTeacher_SavesCourseAndRedirects()
    {
        await using var dbContext = CreateDbContext();
        var teacher = new Teacher { Name = "Petro Shevchenko", Department = "Science" };
        dbContext.Teachers.Add(teacher);
        await dbContext.SaveChangesAsync();

        var pageModel = new StudentPerformanceSystem.Features.Courses.Index(dbContext)
        {
            NewCourse = new Course
            {
                Title = "Physics",
                TeacherId = teacher.Id
            }
        };

        var result = await pageModel.OnPostAsync();

        var redirect = Xunit.Assert.IsType<RedirectToPageResult>(result);
        Xunit.Assert.Null(redirect.PageName);

        var savedCourse = await dbContext.Courses.SingleAsync();
        Xunit.Assert.Equal("Physics", savedCourse.Title);
        Xunit.Assert.Equal(teacher.Id, savedCourse.TeacherId);
    }

    [Xunit.Fact]
    public async Task OnPostAsync_WithoutTeachers_ReturnsPageAndDoesNotSave()
    {
        await using var dbContext = CreateDbContext();
        var pageModel = new StudentPerformanceSystem.Features.Courses.Index(dbContext)
        {
            NewCourse = new Course
            {
                Title = "Chemistry"
            }
        };

        var result = await pageModel.OnPostAsync();

        Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.RazorPages.PageResult>(result);
        Xunit.Assert.False(pageModel.HasTeachers);
        Xunit.Assert.Empty(await dbContext.Courses.ToListAsync());
        Xunit.Assert.True(pageModel.ModelState.ErrorCount > 0);
    }

    private static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}

