using System.Diagnostics;
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
        [HttpGet]
        // GET: CoursePage/New -> A webpage that asks user to add new course information
        public IActionResult New()
        {
            //Directs to /Views/CoursePage/New.cshtml
            return View();
        }
        [HttpPost]
        //POST: CoursePage/Create -> 
        //Header : Content-Type: application/x-www-url-encoded
        //FORM DATA : coursecode={CourseCode}&teacherid={TeacherId}&coursename={CourseName}
        //Receives information about the course and goes to the list of courses.
        public IActionResult Create(int CourseId,string CourseCode, int TeacherId, DateTime StartDate,DateTime FinishDate,string CourseName)
        {
            //creating Course object
            Course newCourse = new Course();

            newCourse.CourseId = CourseId;
            newCourse.CourseCode = CourseCode;
            newCourse.TeacherId = TeacherId;
            newCourse.StartDate = StartDate;
            newCourse.FinishDate = FinishDate;
            newCourse.CourseName = CourseName;


            Debug.WriteLine(CourseCode);
            Debug.WriteLine(TeacherId);
            Debug.WriteLine(CourseName);
            Debug.WriteLine(StartDate);
            Debug.WriteLine(FinishDate);

            int courseId = _api.addCourse(newCourse);


            // redirects to /Views/CoursePage/List.cshtml
            return RedirectToAction("List", new { id = courseId });
        }
        //[HttpGet]
        //GET: CoursePage/DeleteConfirm -> An article that asks a user if they want to delete course data in table
        public IActionResult DeleteConfirm(int id)
        {
            Course courseSelected = _api.CourseInfo(id);
            // redirects to /Views/CoursePage/DeleteConfirm.cshtml
            return View(courseSelected);
        }
        [HttpPost]
        //POST: CoursePage/Delete -> 
        //Header : Content-Type: application/x-www-url-encoded
        //deletes information about the course and goes to the list of courses.
        public IActionResult Delete(int id)
        {
            int courseId = _api.deleteCourse(id);

            // redirects to /Views/CoursePage/List.cshtml
            return RedirectToAction("List");
        }
    }
}
