using CumulativeAssignment_01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace CumulativeAssignment_01.Controllers
{
    [Route("api/TeacherAPI")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        /// <summary>
        /// This is a GET request used to access all the teachers information present in the teachers table.
        /// </summary>
        /// <returns>
        /// This returns all the teachers information who present in the teachers table.
        /// </returns>
        /// <example>
        /// GET: "https://localhost:7074/api/TeacherAPI/ListOfTeachers" -> ["1","Alexander","Bennett","T378","05-08-2016 00:00:00","55.30","2","Caitlin","Cummings","T381","10-06-2014 00:00:00","62.77","3","Linda","Chan","T382","22-08-2015 00:00:00","60.22","4","Lauren","Smith","T385","22-06-2014 00:00:00","74.20","5","Jessica","Morris","T389","04-06-2012 00:00:00","48.62","6","Thomas","Hawkins","T393","10-08-2016 00:00:00","54.45","7","Shannon","Barton","T397","04-08-2013 00:00:00","64.70","8","Dana","Ford","T401","26-06-2014 00:00:00","71.15","9","Cody","Holland","T403","13-06-2016 00:00:00","43.20","10","John","Taram","T505","23-10-2015 00:00:00","79.63"]
        /// </example>
        [HttpGet(template: "ListOfTeachers")]
        public List<Teacher> ListOfTeachers()
        {
            //create Teacher list to store result sets of database.
            List<Teacher> teachersInfo = new List<Teacher>();

            //Initialize the database connection object.
            SchoolDbContext school=new SchoolDbContext();

            //create connection to access the database
            MySqlConnection connection = school.AccessDatabase();

            //open the connection
            connection.Open();

            //create SQL command 
            MySqlCommand command = connection.CreateCommand();

            //writing the queries to get the desired data
            command.CommandText = "SELECT * FROM teachers";

            //executing the raeder to read the sql query and storing to result
            MySqlDataReader results=command.ExecuteReader();

            //looping the data in database using Read() method
            while (results.Read())
            {
                //Getting and storing values of rows in variables
                int id = Convert.ToInt32(results["teacherid"]);
                string fName = results["teacherfname"].ToString();
                string lName = results["teacherlname"].ToString();
                string empNo = (results["employeenumber"]).ToString();
                DateTime dateOfHire = Convert.ToDateTime(results["hiredate"]);
                decimal sal =Convert.ToDecimal(results["salary"]);

                //Create a mock Teachers object to store the values
                Teacher allTeachers = new Teacher() {


                    //Adding Values to the list
                    TeacherId = id,
                    TeacherFName = fName,
                    TeacherLName = lName,
                    EmployeeNo = empNo,
                    HireDate = dateOfHire,
                    Salary = sal

                };
                //Adding mock object to original Teacher object
                teachersInfo.Add(allTeachers);
            }
            //close the connection
            connection.Close();
            //retuen the result set
            return teachersInfo;
        }
        /// <summary>
        /// This is a GET request used to access a particular teacher information present in the teachers table.
        /// </summary>
        /// <param name="id">This takes teacher id as a input from the user.</param>
        /// <returns>
        /// This returns a particular teacher information who present in the teachers table.
        /// </returns>
        /// <example>
        /// GET: "https://localhost:7074/api/TeacherAPI/ATeacher/7" -> {"teacherId":7,"teacherFName":"Shannon","teacherLName":"Barton","employeeNo":"T397","hireDate":"2013-08-04T00:00:00","salary":64.70}
        /// </example>

        [HttpGet(template:"ATeacher/{id}")]
        public Teacher GetATeacher(int id)
        {
            //creating Teacher object to store values of database
            Teacher teacherDetails=new Teacher() ;

            //Initialize the database connection object.
            SchoolDbContext school =new SchoolDbContext();

            //create connection to access the database
            MySqlConnection connection = school.AccessDatabase();

            //open the connection
            connection.Open();

            //create SQL command
            MySqlCommand command = connection.CreateCommand();

            //writing the queries to get the desired data
            command.CommandText = "SELECT * FROM teachers WHERE teacherid="+id;

            //executing the raeder to read the sql query and storing to result
            MySqlDataReader output = command.ExecuteReader();

            //looping the data in database using Read() method
            while (output.Read())
            {
                //Getting and storing values of rows in variables
                int teacherid = Convert.ToInt32(output["teacherid"]);
                string fName = output["teacherfname"].ToString();
                string lName = output["teacherlname"].ToString();
                string empNo = (output["employeenumber"]).ToString();
                DateTime dateOfHire = Convert.ToDateTime(output["hiredate"]);
                decimal sal = Convert.ToDecimal(output["salary"]);

                //Adding values to the object
                teacherDetails.TeacherId = teacherid;
                teacherDetails.TeacherFName = fName;
                teacherDetails.TeacherLName = lName;
                teacherDetails.EmployeeNo = empNo;
                teacherDetails.HireDate = dateOfHire;
                teacherDetails.Salary = sal;

            }
            //close the connection
            connection.Close();
            //retuen the result set
            return teacherDetails;
        }
        /// <summary>
        /// This method is used to add new teacher details received from the user to the database teachers table. and returns -1 when the teacher first name or last name is empty or null, hire date is future date.
        /// </summary>
        /// <param name="newTeacher">It takes the json teacher object as an input from the user.</param>
        /// <returns>
        ///  The method returns the inserted row number or last inserted teacher id from teachers table.
        /// </returns>
        /// <example>
        /// POST: "https://localhost:7074/api/TeacherAPI/addTeacher" 
        /// Header : Content-Type:"application/json"
        /// FORM DATA :
        /// {\"teacherfname\":\"John\",\"teacherlname\":\"Claus\",\"EmployeeNo\":\"T510\",\"salary\":\"45.67\"}
        /// 35
        /// FORM DATA :
        /// {\"teacherfname\":\"Mary\",\"teacherlname\":\"Claus\",\"EmployeeNo\":\"T345\",\"salary\":\"55.67\"}
        /// 36
        /// FORM DATA :
        /// {\"teacherfname\":\"\",\"teacherlname\":\"\",\"EmployeeNo\":\"T523\",\"salary\":\"87.67\"}
        /// -1
        /// {\"teacherfname\":\"Christine\",\"teacherlname\":\"Bittle\",\"EmployeeNo\":\"T453\",\"hiredate\":\"2025-04-15\",\"salary\":\"98.65\"}
        /// -1
        /// </example>
        [HttpPost(template:"addTeacher")]
        public int addTeacher([FromBody]Teacher newTeacher)
        {
            //creating Teacher object to store values of database
            Teacher teacher = new Teacher();

            int teacherId = 0; // initializing teacher id as 0

            // Checking when teacher first name and last name is null or empty and returns -1 when condition is true
            if (string.IsNullOrWhiteSpace(newTeacher.TeacherFName) || string.IsNullOrWhiteSpace(newTeacher.TeacherLName))
            {
                Debug.WriteLine("Teacher First Name and Last Name cannot be empty or null");
                return -1;
            }
            //Checking when hiredate is greater than current date nad returns -1 when condition is true
            if (newTeacher.HireDate>DateTime.Now)
            {
                Debug.WriteLine("Teacher Hire Date cannot be future date");
                return -1;
            }
            
            try
            {
             //Initialize the database connection object.
             SchoolDbContext schooldb = new SchoolDbContext();

            //create connection to access the database
            MySqlConnection connection = schooldb.AccessDatabase();

            //open the connection
            connection.Open();

            //create SQL command
            MySqlCommand command = connection.CreateCommand();

            //writing the queries to add the desired data
            command.CommandText = "INSERT INTO teachers(teacherfname,teacherlname,employeenumber,hiredate,salary) VALUES(@teacherfname,@teacherlname,@employeenumber,CURRENT_DATE(),@salary)";

            command.Parameters.AddWithValue("@teacherfname", newTeacher.TeacherFName);
            command.Parameters.AddWithValue("@teacherlname", newTeacher.TeacherLName);
            command.Parameters.AddWithValue("@employeenumber", newTeacher.EmployeeNo);
            //command.Parameters.AddWithValue("@hiredate", newTeacher.HireDate);
                command.Parameters.AddWithValue("@salary", newTeacher.Salary);

                Debug.WriteLine("EmployeeNo: " + newTeacher.EmployeeNo);
                Debug.WriteLine("FName: " + newTeacher.TeacherFName);
                Debug.WriteLine("LName: " + newTeacher.TeacherLName);
                Debug.WriteLine("HireDate: " + newTeacher.HireDate);
                Debug.WriteLine("Salary: " + newTeacher.Salary);

                command.Prepare();

            //writing command to insert into database
            command.ExecuteNonQuery();

            //assigning latest row number to the teacher id from teachers table.
            teacherId = Convert.ToInt32(command.LastInsertedId);
            }
            catch (SqlTypeException e)
            {
                Debug.WriteLine("Database run into an Error while adding the teacher"+e.Message);
            }
            catch(Exception e1)
            {
                Debug.WriteLine(e1.Message);
            }
            return teacherId;
        }
        /// <summary>
        /// This method is used to delete existing teacher details from the teachers table from the database, and returns -1 and error in the console if teacher id does not present in the teachers table.
        /// </summary>
        /// <param name="teacherId">It takes the teacher id as input from the user</param>
        /// <returns>
        ///  The method returns the teacher id that deleted from the table.
        /// </returns>
        /// <example>
        /// DELETE: "https://localhost:7074/api/TeacherAPI/deleteTeacher/39" -> 39
        /// </example>
        /// <example>
        /// DELETE: "https://localhost:7074/api/TeacherAPI/deleteTeacher/24" -> -1
        /// </example>
        [HttpDelete(template: "deleteTeacher/{teacherId}")]
        public int deleteTeacher(int teacherId)
        {
            //creating Teacher object to store values of database
            Teacher teacher = new Teacher();

            //Initialize the database connection object.
            SchoolDbContext schooldb = new SchoolDbContext();

            //create connection to access the database
            MySqlConnection connection = schooldb.AccessDatabase();

            //open the connection
            connection.Open();

            //create SQL command
            MySqlCommand command = connection.CreateCommand();

            MySqlCommand teacherIdQuery = connection.CreateCommand();


            //writing the queries to delete the existing data
            command.CommandText = "DELETE FROM teachers WHERE teacherid=@teacherid";

            //writing the queries to fetch a single teacher with teacher id 
            teacherIdQuery.CommandText = "SELECT * FROM teachers WHERE teacherid=@teacherid";
            teacherIdQuery.Parameters.AddWithValue("@teacherid", teacherId);


            //executing the raeder to read the sql query and storing to result
            MySqlDataReader results = teacherIdQuery.ExecuteReader();

            //Checking if the table has rows or not
              if (!results.HasRows)
                {
                   Debug.WriteLine("Teacher does not present, cannot delete the data");
                    results.Close();
                    return -1;
                }
            
            results.Close();
            
            command.Parameters.AddWithValue("@teacherid", teacherId);

            command.Prepare();
            
            //writing command to execute delete query
            command.ExecuteNonQuery();

            //returning command to delete the data
            return  teacherId;
        }
    }
}
