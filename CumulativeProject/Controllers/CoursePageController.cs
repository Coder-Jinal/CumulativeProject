using CumulativeProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CumulativeProject.Controllers
{
    public class CoursePageController : Controller
    {
        private readonly CourseAPIController _api;
        public CoursePageController(CourseAPIController api)
        {
            _api = api;
        }

        //public IActionResult List()
        //{
        //    List<Course> Courses = _api.ListCourses();
        //    return View(Courses);
        //}

        public IActionResult List()
        {
            List<Course> Courses = _api.ListCourses();
            return View(Courses);
        }

        public IActionResult Show(int id)
        {
            Course SelectedCourse = _api.FindCourse(id);
            return View(SelectedCourse);
        }


        //public IActionResult Create(string coursecode, string teacherid, string startdate, string finishdate, string coursename)
        //{

        //    string Coursecode = coursecode;
        //    int Teacherid = Convert.ToInt32(teacherid);
        //    DateTime Startdate = Convert.ToDateTime(startdate);
        //    DateTime Finishdate = Convert.ToDateTime(finishdate);
        //    string Coursename = coursename;

        //    Course NewCourse = new Course();

        //    NewCourse.CourseCode = Coursecode;
        //    NewCourse.TeacherId = Teacherid;
        //    NewCourse.StartDate = Startdate;
        //    NewCourse.FinishDate = Finishdate;
        //    NewCourse.CourseName = Coursename;

        //    int CourseId = _api.AddCourse(NewCourse);

        //    return RedirectToAction("Show", new { id = CourseId });

        //}


        //[HttpPost]
        //public IActionResult Create(Course NewCourse)
        //{
        //    int courseId = _api.AddCourse(NewCourse);

        //    // Redirects to the "Show" action with the ID of the newly created course
        //    return RedirectToAction("Show", new { id = courseId });
        //}

        [HttpPost]
        public IActionResult Create(Course NewTeacher)
        {
            int CourseId = _api.AddCourse(NewTeacher);
            return RedirectToAction("Show", new { id = CourseId });
        }

        [HttpGet]
        public IActionResult New(int id)
        {
            return View();
        }


        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            Course SelectedCourse = _api.FindCourse(id);
            return View(SelectedCourse);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            int CourseId = _api.DeleteCourse(id);
            return RedirectToAction("List");
        }
    }
}
