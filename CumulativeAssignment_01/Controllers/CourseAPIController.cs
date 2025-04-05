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
        /// <summary>
        /// This method is used to add new course details received from the user to the database courses table.
        /// </summary>
        /// <param name="addStudent">It takes the json course object as an input from the user.</param>
        /// <returns>
        ///  The method returns the inserted row number or last inserted course id from courses table.
        /// </returns>
        /// <example>
        /// POST: "https://localhost:7074/api/CourseAPI/addCourse" 
        /// Header : Content-Type:"application/json"
        /// FORM DATA :
        /// {\"courseid\":\"13\",\"coursecode\":\"IXD5106\",\"teacherid\":\"11\",\"coursename\":\"Interaction Design\"}
        /// 13
        /// FORM DATA :
        /// {\"courseid\":\"14\",\"coursecode\":\"IXD5206\",\"teacherid\":\"12\",\"coursename\":\"Product Management\"}
        /// 14
        /// </example>
        [HttpPost(template: "addCourse")]
        public int addCourse([FromBody] Course newCourse)
        {
            //creating Student object to store values of database
            Course course = new Course();

            //Initialize the database connection object.
            SchoolDbContext schooldb = new SchoolDbContext();

            //create connection to access the database
            MySqlConnection connection = schooldb.AccessDatabase();

            //open the connection
            connection.Open();

            //create SQL command
            MySqlCommand command = connection.CreateCommand();

            //writing the queries to add the desired data
            command.CommandText = "INSERT INTO courses(courseid,coursecode,teacherid,startdate,finishdate,coursename) VALUES(@courseid,@coursecode,@teacherid,CURRENT_DATE(), NOW() + INTERVAL 60 DAY,@coursename)";

            //Creating sql command 
            MySqlCommand getCourseId = connection.CreateCommand();

            //writing query to get the last course id in the table
            getCourseId.CommandText = "SELECT IFNULL(MAX(courseid), 0) + 1 FROM courses";

            //executing the raeder to read the sql query and storing to result
            //MySqlDataReader lastCourseId = getCourseId.ExecuteReader();

            int nextCourseId = Convert.ToInt32(getCourseId.ExecuteScalar());

        //    if (lastCourseId.Read()) { 
        //        if (lastCourseId.IsDBNull(0)) {
        //            nextCourseId = lastCourseId.GetInt32(0) + 1;
        //        }
        //}
        //    lastCourseId.Close();

            //int courseId = 0; // initializing course id as 0

            command.Parameters.AddWithValue("@courseid", nextCourseId);
            command.Parameters.AddWithValue("@coursecode", newCourse.CourseCode);
            command.Parameters.AddWithValue("@teacherid", newCourse.TeacherId);
            command.Parameters.AddWithValue("@coursename", newCourse.CourseName);


            //command.Prepare();

            //writing command to insert into database
            command.ExecuteNonQuery();

            //assigning latest row number to the course id from courses table.
            //courseId = Convert.ToInt32(command.LastInsertedId);

            return nextCourseId;
        }
        /// <summary>
        /// This method is used to delete existing course details from the courses table from the database.
        /// </summary>
        /// <param name=" courseId">It takes the course id as input from the user</param>
        /// <returns>
        ///  The method returns the course id that deleted from the table.
        /// </returns>
        /// <example>
        /// DELETE: "https://localhost:7074/api/CourseAPI/deleteCourse/14" -> 14
        /// </example>
        /// /// <example>
        /// DELETE: "https://localhost:7074/api/CourseAPI/deleteCourse/15" -> 15
        /// </example>
        [HttpDelete(template: "deleteCourse/{courseId}")]
        public int deleteCourse(int courseId)
        {
            //creating Student object to store values of database
            Course course = new Course();

            //Initialize the database connection object.
            SchoolDbContext schooldb = new SchoolDbContext();

            //create connection to access the database
            MySqlConnection connection = schooldb.AccessDatabase();

            //open the connection
            connection.Open();

            //create SQL command
            MySqlCommand command = connection.CreateCommand();

            //writing the queries to delete the existing data
            command.CommandText = "DELETE FROM courses WHERE courseid=@courseId";

            command.Parameters.AddWithValue("@courseid", courseId);

            command.Prepare();

            //writing command to execute delete query
            command.ExecuteNonQuery();

            //returning command to delete the data
            return courseId;



        }
    }
}
