using Microsoft.AspNetCore.Mvc;
using CumulativeAssignment_01.Models;
namespace CumulativeAssignment_01.Controllers
{
    public class StudentPageController : Controller
    {
        private readonly StudentAPIController _api;

        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }
        // GET: StudentPage/List -> A webpage that prints all the students data in student table
        public IActionResult List()
        {
            List<Student> studentsInfo = _api.StudentsList();
            //Directs to /Views/StudentPage/List.cshtml
            return View(studentsInfo);
        }
        [HttpGet]
        // GET: Studentpage/Show -> A webpage that prints a single student data in table
        public IActionResult Show(int id)
        {

            Student student = _api.StudentInfo(id);
            //Directs to /Views/StudentPage/Show.cshtml
            return View(student);
        }
    }
}
