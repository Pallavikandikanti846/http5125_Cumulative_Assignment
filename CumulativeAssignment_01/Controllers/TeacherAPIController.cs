using CumulativeAssignment_01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using System;

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
    }
}
