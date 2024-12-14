using CumulativeProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CumulativeProject.Controllers
{
    public class StudentPageController : Controller
    {
        private readonly StudentAPIController _api;
        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Student> Students = _api.ListStudents();
            return View(Students);
        }

        [HttpGet]
        public IActionResult Show(int id)
        {
            Student SelectedStudent = _api.FindStudent(id);
            return View(SelectedStudent);
        }


        //public IActionResult Create(string fname, string lname, string studentno, string enroldate)
        //{

        //    string firstname = fname;
        //    string lastname = lname;
        //    DateOnly Enroldate = DateOnly.FromDateTime(Convert.ToDateTime(enroldate));
        //    string studentnumber = studentno;

        //    Student NewStudent = new Student();

        //    NewStudent.FirstName = firstname;
        //    NewStudent.LastName = lastname;
        //    NewStudent.EnrolDate = Enroldate;
        //    NewStudent.StudentNumber = studentnumber;


        //    int StudentId = _api.AddStudent(NewStudent);

        //    return RedirectToAction("Show", new { id = StudentId });

        //}

        [HttpPost]
        public IActionResult Create(Student NewStudent)
        {
            int StudentId = _api.AddStudent(NewStudent);

            return RedirectToAction("Show", new { id = StudentId });
        }

        [HttpGet]
        public IActionResult New(int id)
        {
            return View();
        }


        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            Student SelectedStudent = _api.FindStudent(id);
            return View(SelectedStudent);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            int StudentId = _api.DeleteStudent(id);
            return RedirectToAction("List");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student SelectedStudent = _api.FindStudent(id);
            return View(SelectedStudent); 
        }



        [HttpPost]
        public IActionResult Update(int id, string FirstName, string LastName, DateTime EnrolDate, string StudentNumber)
        {
            Student UpdatedStudent = new Student();
            UpdatedStudent.FirstName = FirstName;
            UpdatedStudent.LastName = LastName;
            UpdatedStudent.EnrolDate = EnrolDate;
            UpdatedStudent.StudentNumber = StudentNumber;
            _api.UpdateStudent(id, UpdatedStudent);

            return RedirectToAction("Show", new { id = id });
        }
    }
}
