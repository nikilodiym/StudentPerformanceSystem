using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentPerformanceSystem.Features.Students.Models;

namespace StudentPerformanceSystem.Features.Students.Pages;

public class Students : PageModel
{
    // Properties used by the Razor page
    public List<Student> StudentList { get; set; } = new();

    [BindProperty]
    public Student NewStudent { get; set; } = new();

    public void OnGet()
    {
        // Seed with some sample data for development/demo purposes
        if (!StudentList.Any())
        {
            StudentList.Add(new Student { Id = 1, FirstName = "Alice", LastName = "Johnson", BirthDate = DateTime.UtcNow.AddYears(-20) });
            StudentList.Add(new Student { Id = 2, FirstName = "Bob", LastName = "Williams", BirthDate = DateTime.UtcNow.AddYears(-22) });
        }
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // In a real app you'd persist NewStudent via DbContext. Here we just add to in-memory list and redirect.
        NewStudent.Id = StudentList.Count + 1;
        StudentList.Add(NewStudent);

        return RedirectToPage();
    }
}