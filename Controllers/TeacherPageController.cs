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

        [HttpGet]
        public IActionResult List()
        {
            List<Teacher> Teachers = _api.ListTeachers();
            return View(Teachers);
        }

        [HttpGet]   
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

        
        [HttpPost]
        public IActionResult Create(Teacher NewTeacher)
        {
            int TeacherId = _api.AddTeacher(NewTeacher);
            return RedirectToAction("Show", new { id = TeacherId });
        }

        [HttpGet]
        public IActionResult New(int id)
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult DeleteConfirm(int id) 
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);
            return View(SelectedTeacher);
        }

        [HttpPost]  
        public IActionResult Delete(int id)
        {
            int TeacherId = _api.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);
            return View(SelectedTeacher);
        }
        [HttpPost]
        public IActionResult Update(int id, string FirstName, string LastName, DateTime HireDate, string EmployeeNumber, decimal Salary)
        {
            Teacher UpdatedTeacher = new Teacher();
            UpdatedTeacher.FirstName = FirstName;
            UpdatedTeacher.LastName = LastName;
            UpdatedTeacher.HireDate = HireDate;
            UpdatedTeacher.EmployeeNumber = EmployeeNumber;
            UpdatedTeacher.Salary = Salary;

            _api.UpdateTeacher(id, UpdatedTeacher);
            return RedirectToAction("Show", new { id = id });
        }

    }
}
