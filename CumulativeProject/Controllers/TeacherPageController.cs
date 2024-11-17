using CumulativeProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CumulativeProject.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly TeacherAPIController _api;
        private readonly CourseAPIController _courseApi;

        public TeacherPageController(TeacherAPIController api, CourseAPIController courseApi)
        {
            _api = api;
            _courseApi = courseApi; 
        }

        public IActionResult List()
        {
            List<Teacher> Teachers = _api.ListTeachers();
            return View(Teachers);
        }

        public IActionResult Show(int id)
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);

            if (SelectedTeacher == null)
            {
                return NotFound();
            }

            List<Course> AllCourses = _courseApi.ListCourses();
            List<Course> TeacherCourses = AllCourses.Where(c => c.TeacherId == id).ToList();

            ViewData["Teacher"] = SelectedTeacher;
            ViewData["Courses"] = TeacherCourses;

            return View();
        }
    }
}
