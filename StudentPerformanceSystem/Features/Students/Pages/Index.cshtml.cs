using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentPerformanceSystem.Data;
using StudentPerformanceSystem.Features.Students.Models;

namespace StudentPerformanceSystem.Features.Students.Pages;

public class Index : PageModel
{
    private readonly ApplicationDbContext _dbContext;

    public Index(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Properties used by the Razor page
    public List<Student> StudentList { get; private set; } = new();

    [BindProperty]
    public Student NewStudent { get; set; } = new()
    {
        BirthDate = DateTime.Today.AddYears(-18)
    };

    public async Task OnGetAsync()
    {
        await LoadStudentsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadStudentsAsync();
            return Page();
        }

        _dbContext.Students.Add(NewStudent);
        await _dbContext.SaveChangesAsync();

        return RedirectToPage();
    }

    private async Task LoadStudentsAsync()
    {
        StudentList = await _dbContext.Students
            .OrderBy(student => student.LastName)
            .ThenBy(student => student.FirstName)
            .ToListAsync();
    }
}