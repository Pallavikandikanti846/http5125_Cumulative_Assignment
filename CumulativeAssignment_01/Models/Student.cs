namespace CumulativeAssignment_01.Models
{
    //Creating class Student
    public class Student
    {
        //Taking each column in table as a methos and using getters and setters to acceess the values
        public int StudentId { get; set; }
        public string? StudentFName { get; set; }
        public string? StudentLName { get; set; }
        public string? StudentNumber { get; set; }
        public DateTime EnrolDate { get; set; } 
    }
}
