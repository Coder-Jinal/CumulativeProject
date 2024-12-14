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


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Course SelectedCourse = _api.FindCourse(id);
            return View(SelectedCourse);
        }


        [HttpPost]
        public IActionResult Update(int id, string CourseCode, int TeacherId, DateTime StartDate, DateTime FinishDate, string CourseName)
        {

            Course UpdatedCourse = new Course();

            UpdatedCourse.CourseCode = CourseCode;
            UpdatedCourse.TeacherId = TeacherId;
            UpdatedCourse.StartDate = StartDate;
            UpdatedCourse.FinishDate = FinishDate;
            UpdatedCourse.CourseName = CourseName;
            _api.UpdateCourse(id, UpdatedCourse);

            return RedirectToAction("Show", new { id = id });
        }
    }
}
