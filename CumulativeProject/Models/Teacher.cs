namespace CumulativeProject.Models
{
    public class Teacher
    {
            public int TeacherId { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public DateOnly HireDate { get; set; }
            public string? EmployeeNumber { get; set; }
            public string? Salary { get; set; }
    }
}
