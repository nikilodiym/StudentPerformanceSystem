using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentPerformanceSystem.Data;
using StudentPerformanceSystem.Features.Courses.Models;

namespace StudentPerformanceSystem.Features.Courses;

public class Index : PageModel
{
    private readonly ApplicationDbContext _dbContext;

    public Index(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Course> CourseList { get; private set; } = new();

    public List<SelectListItem> TeacherOptions { get; private set; } = new();

    [BindProperty]
    public Course NewCourse { get; set; } = new();

    public bool HasTeachers => TeacherOptions.Count > 0;

    public async Task OnGetAsync()
    {
        await LoadPageDataAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await LoadTeacherOptionsAsync();

        if (!HasTeachers)
        {
            ModelState.AddModelError(string.Empty, "Add at least one teacher before creating a course.");
        }

        if (!ModelState.IsValid)
        {
            await LoadCoursesAsync();
            return Page();
        }

        _dbContext.Courses.Add(NewCourse);
        await _dbContext.SaveChangesAsync();

        return RedirectToPage();
    }

    private async Task LoadPageDataAsync()
    {
        await LoadTeacherOptionsAsync();
        await LoadCoursesAsync();
    }

    private async Task LoadTeacherOptionsAsync()
    {
        TeacherOptions = await _dbContext.Teachers
            .OrderBy(teacher => teacher.Name)
            .Select(teacher => new SelectListItem
            {
                Value = teacher.Id.ToString(),
                Text = $"{teacher.Name} ({teacher.Department})"
            })
            .ToListAsync();
    }

    private async Task LoadCoursesAsync()
    {
        CourseList = await _dbContext.Courses
            .Include(course => course.Teacher)
            .OrderBy(course => course.Title)
            .ToListAsync();
    }
}