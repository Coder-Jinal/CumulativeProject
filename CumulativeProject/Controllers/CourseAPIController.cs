using CumulativeProject.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CumulativeProject.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public CourseAPIController(SchoolDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// It returns a list of courses from database named school
        /// </summary>
        /// <example>
        /// GET api/Course/ListCourses -> [{"courseId":1,"courseName":"Web Application Development","courseCode":"http5101","teacherId":1,"startDate":"2018-09-04","finishDate":"2018-12-14"},
        /// {"courseId":2,"courseName":"Project Management","courseCode":"http5102","teacherId":2,"startDate":"2018-09-04","finishDate":"2018-12-14"},....]
        /// </example>
        /// <returns>
        /// Returns a list of course object
        /// </returns>
        [HttpGet]
        [Route(template:"ListCourses")]
        public List<Course> ListCourses()
        {
            List<Course> courses = new List<Course>();

            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM courses";

                using (MySqlDataReader ResultSet = command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        int Id = Convert.ToInt32(ResultSet["courseid"]);
                        string coursename = ResultSet["coursename"].ToString();
                        string coursecode = ResultSet["coursecode"].ToString();
                        DateOnly startdate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["startdate"]));
                        DateOnly finishdate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["finishdate"]));
                        int teacherId = Convert.ToInt32(ResultSet["teacherid"]);


                        Course CurrentCourse = new Course()
                        {
                            CourseId = Id,
                            CourseName = coursename,
                            CourseCode = coursecode,
                            StartDate = startdate,
                            FinishDate = finishdate,
                            TeacherId = teacherId
                        };
                        courses.Add(CurrentCourse);

                    }
                }
            }

            return courses;
        }


        /// <summary>
        /// It returns a course from database anmed school using CourseId
        /// </summary>
        /// <example>
        /// GET api/Course/FindCourse/5 -> {"courseId":5,"courseName":"Database Development","courseCode":"http5105","teacherId":8,"startDate":"2018-09-04","finishDate":"2018-12-14"}
        /// </example>
        /// <returns>
        /// Returns a course object
        /// </returns>
        [HttpGet]
        [Route(template:"FindCourse/{id}")]
        public Course FindCourse(int id)
        {
            Course SelectedCourse = new Course();

            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM courses WHERE courseid = @id";
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader ResultSet = command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        int Id = Convert.ToInt32(ResultSet["courseid"]);
                        string coursename = ResultSet["coursename"].ToString();
                        string coursecode = ResultSet["coursecode"].ToString();
                        DateOnly startdate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["startdate"]));
                        DateOnly finishdate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["finishdate"]));
                        int teacherId = Convert.ToInt32(ResultSet["teacherid"]);


                        SelectedCourse.CourseId = Id;
                        SelectedCourse.CourseName = coursename;
                        SelectedCourse.CourseCode = coursecode;
                        SelectedCourse.StartDate = startdate;
                        SelectedCourse.FinishDate = finishdate;
                        SelectedCourse.TeacherId = teacherId;
                    }
                }
            }

            return SelectedCourse;
        }
    }
}
