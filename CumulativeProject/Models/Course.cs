namespace CumulativeProject.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? CourseCode { get; set; }
        public int? TeacherId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? FinishDate { get; set; }
    }
}
