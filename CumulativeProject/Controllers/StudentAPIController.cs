using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CumulativeProject.Models;
using System;
using MySql.Data.MySqlClient;


namespace CumulativeProject.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// It returns a list of students from the given database named school
        /// </summary>
        /// <example>
        /// GET api/Student/ListStudents -> [{"studentId":1,"firstName":"Sarah","lastName":"Valdez","studentNumber":"N1678","enrolDate":"2018-06-18"},
        /// {"studentId":2,"firstName":"Jennifer","lastName":"Faulkner","studentNumber":"N1679","enrolDate":"2018-08-02"},....]
        /// </example>
        /// <returns>
        ///  A list of student objects
        /// </returns>
        [HttpGet]
        [Route(template:"ListStudents")]
        public List<Student> ListStudents()
        {
            List<Student> Students = new List<Student>();

            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM students";

                using (MySqlDataReader ResultSet = command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        int Id = Convert.ToInt32(ResultSet["studentid"]);
                        string fname = ResultSet["studentfname"].ToString();
                        string lname = ResultSet["studentlname"].ToString();
                        string studentno = ResultSet["studentnumber"].ToString();
                        DateOnly enroldate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["enroldate"]));

                        Student CurrentStudent = new Student()
                        {
                            StudentId = Id,
                            FirstName = fname,
                            LastName = lname,
                            EnrolDate = enroldate,
                            StudentNumber = studentno
                            
                        };
                        Students.Add(CurrentStudent);

                    }
                }
                return Students;
            }

            
        }



        /// <summary>
        /// It returns student from database named school using student id
        /// </summary>
        /// <exmaple>
        /// GET api/Student/FindStudent/3 -> {"studentId":3,"firstName":"Austin","lastName":"Simon","studentNumber":"N1682","enrolDate":"2018-06-14T00:00:00"}
        /// </exmaple>
        /// <returns>
        /// It returns a student object
        /// </returns>
        [HttpGet]
        [Route(template:"FindStudent/{id}")]
        public Student FindStudent(int id)
        {
            Student SelectedStudent = new Student();

            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM students WHERE studentid = @id";
                command.Parameters.AddWithValue("@id", id);


                using (MySqlDataReader ResultSet = command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        int Id = Convert.ToInt32(ResultSet["studentid"]);
                        string fname = ResultSet["studentfname"].ToString();
                        string lname = ResultSet["studentlname"].ToString();
                        string studentno = ResultSet["studentnumber"].ToString();
                        DateOnly enroldate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["enroldate"]));


                        SelectedStudent.StudentId = Id;
                        SelectedStudent.FirstName = fname;
                        SelectedStudent.LastName = lname;
                        SelectedStudent.EnrolDate = enroldate;
                        SelectedStudent.StudentNumber = studentno;

                        
                    }
                }
            }
            return SelectedStudent;


        }

    }
}
