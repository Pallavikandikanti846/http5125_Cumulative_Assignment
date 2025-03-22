namespace CumulativeAssignment_01.Models
{
    //Creating class Course
    public class Course
    {
        //Taking each column in table as a methos and using getters and setters to acceess the values
        public int CourseId { get; set; }
        public string? CourseCode { get; set; }
        public int TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string? CourseName { get; set; }
    }
}
