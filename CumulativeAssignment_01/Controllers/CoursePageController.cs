using CumulativeAssignment_01.Models;
using Microsoft.AspNetCore.Mvc;

namespace CumulativeAssignment_01.Controllers
{
    public class CoursePageController : Controller
    {
        private readonly CourseAPIController _api;

        public CoursePageController(CourseAPIController api)
        {
            _api = api;
        }

        // GET: CoursePage/List -> A webpage that prints all the courses data in table
        public IActionResult List()
        {

            List<Course> courseList = _api.CoursesList();
            //Directs to /Views/CoursePage/List.cshtml
            return View(courseList);
        }
        [HttpGet]
        // GET: CoursePage/Show -> A webpage that prints a particular course data in table
        public IActionResult Show(int id)
        {

            Course courses = _api.CourseInfo(id);
            //Directs to /Views/CoursePage/Show.cshtml
            return View(courses);
        }
    }
}
