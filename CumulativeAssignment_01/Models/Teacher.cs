namespace CumulativeAssignment_01.Models
{
    //Creating class Teacher 
    public class Teacher
    {
        //Taking each column in table as a methos and using getters and setters to acceess the values
        public int TeacherId { get; set; }
        public string? TeacherFName { get; set; }
        public string? TeacherLName { get; set; }
        public string? EmployeeNo { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
    }
}
