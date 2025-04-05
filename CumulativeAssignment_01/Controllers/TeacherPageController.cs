using Microsoft.AspNetCore.Mvc;
using CumulativeAssignment_01.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CumulativeAssignment_01.Controllers
{
    public class TeacherPageController : Controller
    {

        private readonly TeacherAPIController _api;

        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        // GET: TeacherPage/List-> A webpage that prints all the teachers data in table based on search parameter given by the user on web page.
        public IActionResult List()
        {

            List<Teacher> teachersInfo = _api.ListOfTeachers();
            //Directs to /Views/TeacherPage/List.cshtml
            return View(teachersInfo);
        }
        [HttpGet]
        // GET: TeacherPage/Show -> A webpage that prints a single teacher data in table
        public IActionResult Show(int id)
        {

            Teacher teacherDetails = _api.GetATeacher(id);
            //Directs to /Views/TeacherPage/Show.cshtml
            return View(teacherDetails);
        }
        [HttpGet]
        // GET: TeacherPage/New -> A webpage that asks user to add new teacher information
        public IActionResult New()
        {
            //Directs to /Views/TeacherPage/New.cshtml
            return View();
        }
        [HttpPost]
        //POST: TecherPage/Create -> 
        //Header : Content-Type: application/x-www-url-encoded
        //FORM DATA : teacherfname={TeacherFName}&teacherlname={TeacherLName}&employeenumber={EmployeeNo}&salary={Salary}
        //Receives information about the teacher and goes to the list of teachers.
        public IActionResult Create(string TeacherFName, string TeacherLName, string EmployeeNo, decimal Salary,DateTime HireDate)
        {
            //creating Teacher object
            Teacher newTeacher = new Teacher();

            newTeacher.TeacherFName = TeacherFName;
            newTeacher.TeacherLName = TeacherLName;
            newTeacher.EmployeeNo = EmployeeNo;
            newTeacher.HireDate = HireDate;
            newTeacher.Salary = Salary;

            Debug.WriteLine(TeacherFName);
            Debug.WriteLine(TeacherLName);
            Debug.WriteLine(EmployeeNo);
            Debug.WriteLine(Salary);
            Debug.WriteLine(HireDate);

            int teacherId = _api.addTeacher(newTeacher);


            // redirects to /Views/TeacherPage/List.cshtml
            return RedirectToAction("List", new { id = teacherId });
        }
        //[HttpGet]
        //GET: TeacherPage/DeleteConfirm -> An article that asks a user if they want to delete teacher data in table
        public IActionResult DeleteConfirm(int id)
        {
            Teacher teacherSelected = _api.GetATeacher(id);
            // redirects to /Views/TeacherPage/DeleteConfirm.cshtml
            return View(teacherSelected);
        }
        [HttpPost]
        //POST: TeacherPage/Delete -> 
        //Header : Content-Type: application/x-www-url-encoded
        //deletes information about the teacher and goes to the list of teachers.
        public IActionResult Delete(int id)
        {
            int teacherId = _api.deleteTeacher(id);

            // redirects to /Views/TeacherPage/List.cshtml
            return RedirectToAction("List");
        }
    }
}
