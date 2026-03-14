using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPerformanceSystem.Data;
using StudentPerformanceSystem.Features.Students.Models;

namespace StudentPerformanceSystem.Tests;

public class StudentTests
{
    [Xunit.Fact]
    public async Task OnGetAsync_LoadsStudentsFromDatabase()
    {
        await using var dbContext = CreateDbContext();
        dbContext.Students.AddRange(
            new Student { FirstName = "Ira", LastName = "Bondar", BirthDate = new DateTime(2005, 1, 10) },
            new Student { FirstName = "Andrii", LastName = "Antonov", BirthDate = new DateTime(2004, 2, 20) });
        await dbContext.SaveChangesAsync();

        var pageModel = new StudentPerformanceSystem.Features.Students.Pages.Index(dbContext);

        await pageModel.OnGetAsync();

        Xunit.Assert.Equal(2, pageModel.StudentList.Count);
        Xunit.Assert.Equal("Antonov", pageModel.StudentList[0].LastName);
    }

    [Xunit.Fact]
    public async Task OnPostAsync_WithValidStudent_SavesStudentAndRedirects()
    {
        await using var dbContext = CreateDbContext();
        var pageModel = new StudentPerformanceSystem.Features.Students.Pages.Index(dbContext)
        {
            NewStudent = new Student
            {
                FirstName = "Marta",
                LastName = "Koval",
                BirthDate = new DateTime(2006, 5, 15)
            }
        };

        var result = await pageModel.OnPostAsync();

        var redirect = Xunit.Assert.IsType<RedirectToPageResult>(result);
        Xunit.Assert.Null(redirect.PageName);

        var savedStudent = await dbContext.Students.SingleAsync();
        Xunit.Assert.Equal("Marta", savedStudent.FirstName);
        Xunit.Assert.Equal("Koval", savedStudent.LastName);
    }

    [Xunit.Fact]
    public async Task OnPostAsync_WithInvalidModelState_ReturnsPageAndDoesNotSave()
    {
        await using var dbContext = CreateDbContext();
        dbContext.Students.Add(new Student
        {
            FirstName = "Existing",
            LastName = "Student",
            BirthDate = new DateTime(2003, 3, 3)
        });
        await dbContext.SaveChangesAsync();

        var pageModel = new StudentPerformanceSystem.Features.Students.Pages.Index(dbContext)
        {
            NewStudent = new Student()
        };
        pageModel.ModelState.AddModelError("NewStudent.FirstName", "Required");

        var result = await pageModel.OnPostAsync();

        Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.RazorPages.PageResult>(result);
        Xunit.Assert.Equal(1, await dbContext.Students.CountAsync());
        Xunit.Assert.Single(pageModel.StudentList);
    }

    private static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}