using CumulativeAssignment_01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using System;

namespace CumulativeAssignment_01.Controllers
{
    [Route("api/StudentAPI")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        /// <summary>
        /// This is a GET request used to access all the students information present in the student table.
        /// </summary>
        /// <returns>
        /// This returns all the students information who present in the student table.
        /// </returns>
        /// <example>
        /// GET: "https://localhost:7074/api/StudentAPI/studentsList" -> [{"studentId":1,"studentFName":"Sarah","studentLName":"Valdez","studentNumber":"N1678","enrolDate":"2018-06-18T00:00:00"},{"studentId":2,"studentFName":"Jennifer","studentLName":"Faulkner","studentNumber":"N1679","enrolDate":"2018-08-02T00:00:00"},{"studentId":3,"studentFName":"Austin","studentLName":"Simon","studentNumber":"N1682","enrolDate":"2018-06-14T00:00:00"},{"studentId":4,"studentFName":"Mario","studentLName":"English","studentNumber":"N1686","enrolDate":"2018-07-03T00:00:00"},{"studentId":5,"studentFName":"Elizabeth","studentLName":"Murray","studentNumber":"N1690","enrolDate":"2018-07-12T00:00:00"},{"studentId":6,"studentFName":"Kevin","studentLName":"Williams","studentNumber":"N1691","enrolDate":"2018-08-04T00:00:00"},{"studentId":7,"studentFName":"Jason","studentLName":"Freeman","studentNumber":"N1694","enrolDate":"2018-08-16T00:00:00"},{"studentId":8,"studentFName":"Nicole","studentLName":"Armstrong","studentNumber":"N1698","enrolDate":"2018-07-10T00:00:00"},{"studentId":9,"studentFName":"Colleen","studentLName":"Riley","studentNumber":"N1702","enrolDate":"2018-07-15T00:00:00"},{"studentId":10,"studentFName":"Julie","studentLName":"Salazar","studentNumber":"N1705","enrolDate":"2018-07-10T00:00:00"},{"studentId":11,"studentFName":"Dr.","studentLName":"Bridges","studentNumber":"N1709","enrolDate":"2018-08-22T00:00:00"},{"studentId":12,"studentFName":"Vanessa","studentLName":"Cox","studentNumber":"N1712","enrolDate":"2018-08-17T00:00:00"},{"studentId":13,"studentFName":"Denise","studentLName":"Jackson","studentNumber":"N1714","enrolDate":"2018-07-26T00:00:00"},{"studentId":14,"studentFName":"Roy","studentLName":"Davidson","studentNumber":"N1715","enrolDate":"2018-08-11T00:00:00"},{"studentId":15,"studentFName":"Ryan","studentLName":"Walters","studentNumber":"N1717","enrolDate":"2018-07-25T00:00:00"},{"studentId":16,"studentFName":"Patricia","studentLName":"Sweeney","studentNumber":"N1719","enrolDate":"2018-08-08T00:00:00"},{"studentId":18,"studentFName":"Melissa","studentLName":"Morales","studentNumber":"N1723","enrolDate":"2018-08-10T00:00:00"},{"studentId":19,"studentFName":"Kimberly","studentLName":"Johnson","studentNumber":"N1727","enrolDate":"2018-08-02T00:00:00"},{"studentId":20,"studentFName":"Andrea","studentLName":"Flores","studentNumber":"N1731","enrolDate":"2018-07-09T00:00:00"},{"studentId":21,"studentFName":"Jason","studentLName":"II","studentNumber":"N1732","enrolDate":"2018-06-05T00:00:00"},{"studentId":22,"studentFName":"David","studentLName":"Dunlap","studentNumber":"N1734","enrolDate":"2018-08-27T00:00:00"},{"studentId":23,"studentFName":"Elizabeth","studentLName":"Thompson","studentNumber":"N1736","enrolDate":"2018-08-07T00:00:00"},{"studentId":24,"studentFName":"Becky","studentLName":"Medina","studentNumber":"N1737","enrolDate":"2018-07-02T00:00:00"},{"studentId":25,"studentFName":"Wayne","studentLName":"Collins","studentNumber":"N1740","enrolDate":"2018-07-20T00:00:00"},{"studentId":26,"studentFName":"Nicole","studentLName":"Henderson","studentNumber":"N1742","enrolDate":"2018-06-07T00:00:00"},{"studentId":27,"studentFName":"David","studentLName":"Larson","studentNumber":"N1744","enrolDate":"2018-07-19T00:00:00"},{"studentId":28,"studentFName":"John","studentLName":"Reed","studentNumber":"N1748","enrolDate":"2018-08-15T00:00:00"},{"studentId":29,"studentFName":"Richard","studentLName":"King","studentNumber":"N1751","enrolDate":"2018-08-17T00:00:00"},{"studentId":30,"studentFName":"Alexander","studentLName":"Bennett","studentNumber":"N1752","enrolDate":"2018-07-29T00:00:00"},{"studentId":31,"studentFName":"Caitlin","studentLName":"Cummings","studentNumber":"N1756","enrolDate":"2018-08-02T00:00:00"},{"studentId":32,"studentFName":"Christine","studentLName":"Bittle","studentNumber":"N0001","enrolDate":"2020-10-05T00:00:00"}]
        /// </example>
        [HttpGet(template:"studentsList")]
        public List<Student> StudentsList()
        {
            //creating Student object to store values of database
            List<Student> studentsInfo = new List<Student>();

            //Initialize the database connection object.
            SchoolDbContext school = new SchoolDbContext();

            //create connection to access the database
            MySqlConnection connection = school.AccessDatabase();

            //open the connection
            connection.Open();

            //create SQL command
            MySqlCommand command = connection.CreateCommand();

            //writing the queries to get the desired data
            command.CommandText = "SELECT * FROM students";

            //executing the raeder to read the sql query and storing to result
            MySqlDataReader resultSet = command.ExecuteReader();

            //looping the data in database using Read() method
            while (resultSet.Read())
            {
                //Getting and storing values of rows in variables
                int id = Convert.ToInt32(resultSet["studentid"]);
                string fName = resultSet["studentfname"].ToString();
                string lName = resultSet["studentlname"].ToString();
                string number = resultSet["studentnumber"].ToString();
                DateTime dateOfEnrol = Convert.ToDateTime(resultSet["enroldate"]);

                //Create a mock Teachers object to store the values
                Student student = new Student()
                {
                    //Adding Values to the list
                    StudentId = id,
                    StudentFName = fName,
                    StudentLName = lName,
                    StudentNumber = number,
                    EnrolDate = dateOfEnrol,
                };
                studentsInfo.Add(student);

            }
            //close the connection
            resultSet.Close();
            //retuen the result set
            return studentsInfo;
        }
        /// <summary>
        /// This is a GET request used to access a particular teacher information present in the teachers table using a particular id provided by the user.
        /// </summary>
        /// <param name="id">This takes teacher id as a input from the user.</param>
        /// <returns>
        /// This returns a particular teacher information who present in the teachers table.
        /// </returns>
        /// <example>
        /// GET: "https://localhost:7074/api/StudentAPI/studentDetails/20" -> {"studentId":20,"studentFName":"Andrea","studentLName":"Flores","studentNumber":"N1731","enrolDate":"2018-07-09T00:00:00"}
        /// </example>

        [HttpGet(template: "studentDetails/{id}")]
        public Student StudentInfo(int id)
        {
            //creating Student object to store values of database
            Student student = new Student();

            //Initialize the database connection object.
            SchoolDbContext school = new SchoolDbContext();

            //create connection to access the database
            MySqlConnection connection = school.AccessDatabase();

            //open the connection
            connection.Open();

            //create SQL command
            MySqlCommand command = connection.CreateCommand();

            //writing the queries to get the desired data
            command.CommandText = "SELECT * FROM students WHERE studentid=" + id;

            //executing the raeder to read the sql query and storing to result
            MySqlDataReader output = command.ExecuteReader();

            //looping the data in database using Read() method
            while (output.Read())
            {
                //Getting and storing values of rows in variables
                int studentid = Convert.ToInt32(output["studentid"]);
                string fName = output["studentfname"].ToString();
                string lName = output["studentlname"].ToString();
                string number = output["studentnumber"].ToString();
                DateTime dateOfEnrol = Convert.ToDateTime(output["enroldate"]);

                //Adding values to the object
                student.StudentId = studentid;
                student.StudentFName = fName;
                student.StudentLName = lName;
                student.StudentNumber = number;
                student.EnrolDate = dateOfEnrol;

            }
            //close the connection
            connection.Close();
            //retuen the result set
            return student;
        }
        /// <summary>
        /// This method is used to add new student details received from the user to the database students table.
        /// </summary>
        /// <param name="addStudent">It takes the json student object as an input from the user.</param>
        /// <returns>
        ///  The method returns the inserted row number or last inserted student id from students table.
        /// </returns>
        /// <example>
        /// POST: "https://localhost:7074/api/StudentAPI/addStudent" 
        /// Header : Content-Type:"application/json"
        /// FORM DATA :
        /// {\"studentfname\":\"Ramya\",\"studentlname\":\"Shetty\",\"studentnumber\":\"N1750\"}
        /// 36
        /// FORM DATA :
        /// {\"studentfname\":\"Pallavi\",\"studentlname\":\"Kandikanti\",\"studentnumber\":\"N1751\"}
        /// 37
        /// </example>
        [HttpPost(template: "addStudent")]
        public int addStudent([FromBody] Student newStudent)
        {
            //creating Student object to store values of database
            Student student = new Student();

            //Initialize the database connection object.
            SchoolDbContext schooldb = new SchoolDbContext();

            //create connection to access the database
            MySqlConnection connection = schooldb.AccessDatabase();

            //open the connection
            connection.Open();

            //create SQL command
            MySqlCommand command = connection.CreateCommand();

            //writing the queries to add the desired data
            command.CommandText = "INSERT INTO students(studentfname,studentlname,studentnumber,enroldate) VALUES(@studentfname,@studentlname,@studentnumber,CURRENT_DATE())";

            int studentId = 0; // initializing course id as 0

            command.Parameters.AddWithValue("@studentfname", newStudent.StudentFName);
            command.Parameters.AddWithValue("@studentlname", newStudent.StudentLName);
            command.Parameters.AddWithValue("@studentnumber", newStudent.StudentNumber);
           

            command.Prepare();

            //writing command to insert into database
            command.ExecuteNonQuery();

            //assigning latest row number to the teacher id from teachers table.
            studentId = Convert.ToInt32(command.LastInsertedId);

            return studentId;
        }
        /// <summary>
        /// This method is used to delete existing student details from the student table from the database.
        /// </summary>
        /// <param name="studentId">It takes the student id as input from the user</param>
        /// <returns>
        ///  The method returns the student id that deleted from the table.
        /// </returns>
        /// <example>
        /// DELETE: "https://localhost:7074/api/StudentAPI/deleteStudent/37" -> 37
        /// </example>
        /// <example>
        /// DELETE: "https://localhost:7074/api/StudentAPI/deleteStudent/38" -> 38
        /// </example>
        [HttpDelete(template: "deleteStudent/{studentId}")]
        public int deleteStudent(int studentId)
        {
            //creating Student object to store values of database
            Student student = new Student();

            //Initialize the database connection object.
            SchoolDbContext schooldb = new SchoolDbContext();

            //create connection to access the database
            MySqlConnection connection = schooldb.AccessDatabase();

            //open the connection
            connection.Open();

            //create SQL command
            MySqlCommand command = connection.CreateCommand();

            //writing the queries to delete the existing data
            command.CommandText = "DELETE FROM students WHERE studentid=@studentId";

            command.Parameters.AddWithValue("@studentid", studentId);

            command.Prepare();

            //writing command to execute delete query
            command.ExecuteNonQuery();

            //returning command to delete the data
            return studentId;
        }
    }
}
