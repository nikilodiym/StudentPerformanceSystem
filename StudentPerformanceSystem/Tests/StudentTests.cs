using StudentPerformanceSystem.Features.Students.Models;

namespace StudentPerformanceSystem.Tests;

public class StudentTests
{
    [Fact]
    public void CreateStudent()
    {
        var student = new Student
        {
            FirstName = "John",
            LastName = "Smith"
        };

        Assert.Equal("John", student.FirstName);
    }
}

public class Assert
{
    public static void Equal(string john, string studentFirstName)
    {
        throw new NotImplementedException();
    }
}

public class FactAttribute : Attribute
{
}