using Microsoft.AspNetCore.Mvc;
using CumulativeAssignment_01.Models;
using System.Diagnostics;

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
    }
}
