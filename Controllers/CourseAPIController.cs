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
                        DateTime startdate = Convert.ToDateTime(ResultSet["startdate"]);
                        DateTime finishdate = Convert.ToDateTime(ResultSet["finishdate"]);
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
                        DateTime startdate = Convert.ToDateTime(ResultSet["startdate"]);
                        DateTime finishdate = Convert.ToDateTime(ResultSet["finishdate"]);
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


        /// <summary>
        /// It adds the course to the database named courses
        /// </summary>
        /// <param name="CourseData">Course Object</param>
        /// <example>
        /// POST: api/CourseData/AddCourse
        /// Headers: Content-Type: application/json
        /// Request Body:
        /// {
        /// "courseName": "Full stack development",
        /// "courseCode": "http5206",
        /// "teacherId": 4,
        /// "startDate": "2019-01-08T00:00:00",
        /// "finishDate": "2019-04-27T00:00:00",
        /// } -> 15
        /// </example>
        /// <returns>
        /// It returns the inserted Course Id from the database if successful. Or 0 if Unsuccessful
        /// </returns>
        [HttpPost(template: "AddCourse")]
        public int AddCourse([FromBody] Course CourseData)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();

                Command.CommandText = "INSERT INTO courses (coursecode, teacherid, startdate, finishdate, coursename) VALUES (@coursecode, @teacherid, @startdate, @finishdate, @coursename)";
                Command.Parameters.AddWithValue("@coursecode", CourseData.CourseCode);
                Command.Parameters.AddWithValue("@teacherid", CourseData.TeacherId);
                Command.Parameters.AddWithValue("@startdate", CourseData.StartDate);
                Command.Parameters.AddWithValue("@finishdate", CourseData.FinishDate);
                Command.Parameters.AddWithValue("@coursename", CourseData.CourseName);
                Command.ExecuteNonQuery();

                return Convert.ToInt32(Command.LastInsertedId);

            }
            return 0;
        }


        /// <summary>
        /// It deletes a course from the database named courses
        /// </summary>
        /// <param name="CourseId">Primary key of the course to delete</param>
        /// <example>
        /// DELETE: api/CourseData/DeleteCourse -> 1
        /// </example>
        /// <returns>
        /// It returns a number of rows affected by delete operation.
        /// </returns>
        [HttpDelete(template: "DeleteCourse/{CourseId}")]
        public int DeleteCourse(int CourseId)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();


                Command.CommandText = "delete from courses where courseid=@id";
                Command.Parameters.AddWithValue("@id", CourseId);
                return Command.ExecuteNonQuery();

            }
            return 0;
        }


        /// <summary>
        /// It updates a course in the database.
        /// </summary>
        /// <example>
        /// PUT: api/Course/UpdateCourse/1
        /// Headers: Content-Type: application/json
        /// Request Body:
        /// {
        ///	    "Coursename":"Intro to web development",
        ///	    "CourseCode":"http-5113",
        ///	    "TeacherId":"2",
        ///	    "StartDate":"2024-12-14",
        ///	    "Finishdate":"2024-12-14"
        /// } -> 
        /// {
        ///     "Coursename":"Intro to web development",
        ///	    "CourseCode":"http-5113",
        ///	    "TeacherId":"2",
        ///	    "StartDate":"2024-12-14",
        ///	    "Finishdate":"2024-12-14"
        /// }
        /// </example>
        /// <param name="CourseId">Course Object</param>
        /// <param name="CourseData">The Course ID primary key</param>
        /// <returns>
        /// It returns the updated Course object
        /// </returns>

        [HttpPut(template: "UpdateCourse/{CourseId}")]
        public Course UpdateCourse(int CourseId, [FromBody] Course CourseData)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();

                Command.CommandText = "update courses set coursecode=@coursecode, teacherid=@teacherid, startdate=@startdate, finishdate=@finishdate, coursename=@coursename where courseid=@id";
                Command.Parameters.AddWithValue("@coursecode", CourseData.CourseCode);
                Command.Parameters.AddWithValue("@teacherid", CourseData.TeacherId);
                Command.Parameters.AddWithValue("@startdate", CourseData.StartDate);
                Command.Parameters.AddWithValue("@finishdate", CourseData.FinishDate);
                Command.Parameters.AddWithValue("@coursename", CourseData.CourseName);

                Command.Parameters.AddWithValue("@id", CourseId);

                Command.ExecuteNonQuery();



            }

            return FindCourse(CourseId);
        }

    }
}
