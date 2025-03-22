using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CumulativeAssignment_01.Models;
using MySql.Data.MySqlClient;

namespace CumulativeAssignment_01.Controllers
{
    [Route("api/CourseAPI")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        /// <summary>
        /// This is a GET request used to access all the courses information present in the courses table.
        /// </summary>
        /// <returns>
        /// This returns all the courses information who present in the courses table.
        /// </returns>
        /// <example>
        /// GET: "https://localhost:7074/api/CourseAPI/CoursesList" -> [{"courseId":1,"courseCode":"http5101","teacherId":1,"startDate":"2018-09-04T00:00:00","finishDate":"2018-12-14T00:00:00","courseName":"Web Application Development"},{"courseId":2,"courseCode":"http5102","teacherId":2,"startDate":"2018-09-04T00:00:00","finishDate":"2018-12-14T00:00:00","courseName":"Project Management"},{"courseId":3,"courseCode":"http5103","teacherId":5,"startDate":"2018-09-04T00:00:00","finishDate":"2018-12-14T00:00:00","courseName":"Web Programming"},{"courseId":4,"courseCode":"http5104","teacherId":7,"startDate":"2018-09-04T00:00:00","finishDate":"2018-12-14T00:00:00","courseName":"Digital Design"},{"courseId":5,"courseCode":"http5105","teacherId":8,"startDate":"2018-09-04T00:00:00","finishDate":"2018-12-14T00:00:00","courseName":"Database Development"},{"courseId":6,"courseCode":"http5201","teacherId":2,"startDate":"2019-01-08T00:00:00","finishDate":"2019-04-27T00:00:00","courseName":"Security & Quality Assurance"},{"courseId":7,"courseCode":"http5202","teacherId":3,"startDate":"2019-01-08T00:00:00","finishDate":"2019-04-27T00:00:00","courseName":"Web Application Development 2"},{"courseId":8,"courseCode":"http5203","teacherId":4,"startDate":"2019-01-08T00:00:00","finishDate":"2019-04-27T00:00:00","courseName":"XML and Web Services"},{"courseId":9,"courseCode":"http5204","teacherId":5,"startDate":"2019-01-08T00:00:00","finishDate":"2019-04-27T00:00:00","courseName":"Mobile Development"},{"courseId":10,"courseCode":"http5205","teacherId":6,"startDate":"2019-01-08T00:00:00","finishDate":"2019-04-27T00:00:00","courseName":"Career Connections"},{"courseId":11,"courseCode":"http5206","teacherId":8,"startDate":"2019-01-08T00:00:00","finishDate":"2019-04-27T00:00:00","courseName":"Web Information Architecture"},{"courseId":12,"courseCode":"PHYS2203","teacherId":10,"startDate":"2019-09-04T00:00:00","finishDate":"2019-12-14T00:00:00","courseName":"Massage Therapy"}]
        /// </example>
        [HttpGet(template:"CoursesList")]
        public List<Course> CoursesList()
        {
            //create Course list to store result sets of database.
            List<Course> courseList = new List<Course>();

            //Initialize the database connection object.
            SchoolDbContext school = new SchoolDbContext();

            //create connection to access the database
            MySqlConnection connection = school.AccessDatabase();

            //open the connection
            connection.Open();

            //create SQL command 
            MySqlCommand command = connection.CreateCommand();

            //writing the queries to get the desired data
            command.CommandText = "SELECT * FROM courses";

            //executing the raeder to read the sql query and storing to result
            MySqlDataReader results = command.ExecuteReader();

            //looping the data in database using Read() method
            while (results.Read())
            {
                //Getting and storing values of rows in variables
                int id = Convert.ToInt32(results["courseid"]);
                string code = results["coursecode"].ToString();
                int teacherid = Convert.ToInt32(results["teacherid"]);
                DateTime startDate = Convert.ToDateTime(results["startdate"]);
                DateTime endDate = Convert.ToDateTime(results["finishdate"]);
                string name = results["coursename"].ToString();

                //Create a mock Teachers object to store the values
                Course courses = new Course()
                {


                    //Adding Values to the list
                    CourseId = id,
                    CourseCode = code,
                    TeacherId = teacherid,
                    StartDate = startDate,
                    FinishDate = endDate,
                    CourseName = name

                };
                //Adding mock object to original Teacher object
                courseList.Add(courses);
            }
            //close the connection
            connection.Close();
            //retuen the result set
            return courseList;
        }
        /// <summary>
        /// This is a GET request used to access a particular course information present in the courses table using a particular id provided by the user.
        /// </summary>
        /// <param name="id">This takes course id as a input from the user.</param>
        /// <returns>
        /// This returns a particular course information who present in the courses table.
        /// </returns>
        /// <example>
        /// GET: "https://localhost:7074/api/CourseAPI/CourseInfo/10" -> {"courseId":10,"courseCode":"http5205","teacherId":6,"startDate":"2019-01-08T00:00:00","finishDate":"2019-04-27T00:00:00","courseName":"Career Connections"}
        /// </example>

        [HttpGet(template: "CourseInfo/{id}")]
        public Course CourseInfo(int id)
        {
            //creating Course object to store values of database
            Course courses = new Course();

            //Initialize the database connection object.
            SchoolDbContext school = new SchoolDbContext();

            //create connection to access the database
            MySqlConnection connection = school.AccessDatabase();

            //open the connection
            connection.Open();

            //create SQL command
            MySqlCommand command = connection.CreateCommand();

            //writing the queries to get the desired data
            command.CommandText = "SELECT * FROM courses WHERE courseid=" + id;

            //executing the raeder to read the sql query and storing to result
            MySqlDataReader output = command.ExecuteReader();

            //looping the data in database using Read() method
            while (output.Read())
            {
                //Getting and storing values of rows in variables
                int courseid = Convert.ToInt32(output["courseid"]);
                string code = output["coursecode"].ToString();
                int teacherid = Convert.ToInt32(output["teacherid"]);
                DateTime startDate = Convert.ToDateTime(output["startdate"]);
                DateTime endDate = Convert.ToDateTime(output["finishdate"]);
                string name = output["coursename"].ToString();

                //Adding values to the object
                courses.CourseId = courseid;
                courses.CourseCode = code;
                courses.TeacherId = teacherid;
                courses.StartDate = startDate;
                courses.FinishDate = endDate;
                courses.CourseName = name;

            }
            //close the connection
            connection.Close();
            //retuen the result set
            return courses;
        }
    }
}
