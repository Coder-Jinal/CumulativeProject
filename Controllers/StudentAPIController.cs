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
                        DateTime enroldate = Convert.ToDateTime(ResultSet["enroldate"]);

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
                        DateTime enroldate = Convert.ToDateTime(ResultSet["enroldate"]);


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

        /// <summary>
        /// It adds the student to the database named students
        /// </summary>
        /// <param name="StudentData">Student Object</param>
        /// <example>
        /// POST: api/StudentData/AddStudent
        /// Headers: Content-Type: application/json
        /// Request Body:
        /// {
        /// "firstName": "Jinal",
        /// "lastName": "Patel",
        /// "studentNumber": "N0185",
        /// "enrolDate": "2024-12-01T02:39:31"
        /// } -> 45
        /// </example>
        /// <returns>
        /// It returns the inserted Student Id from the database if successful. Or 0 if Unsuccessful
        /// </returns>
        [HttpPost(template: "AddStudent")]
        public int AddStudent(Student StudentData)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                MySqlCommand Command = Connection.CreateCommand();

                Command.CommandText = "insert into students (studentfname, studentlname, studentnumber, enroldate) values (@studentfname, @studentlname, @studentnumber, @enroldate)";
                Command.Parameters.AddWithValue("@studentfname", StudentData.FirstName);
                Command.Parameters.AddWithValue("@studentlname", StudentData.LastName);
                Command.Parameters.AddWithValue("@studentnumber", StudentData.StudentNumber);
                Command.Parameters.AddWithValue("@enroldate", StudentData.EnrolDate);
                Command.ExecuteNonQuery();

                return Convert.ToInt32(Command.LastInsertedId);

            }
            return 0;
        }


        /// <summary>
        /// It deletes a student from the database named students
        /// </summary>
        /// <param name="StudentId">Primary key of the student to delete</param>
        /// <example>
        /// DELETE: api/StudentData/DeleteStudent -> 1
        /// </example>
        /// <returns>
        /// It returns a number of rows affected by delete operation.
        /// </returns>
        [HttpDelete(template: "DeleteStudent/{StudentId}")]
        public int DeleteStudent(int StudentId)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();


                Command.CommandText = "delete from students where studentid=@id";
                Command.Parameters.AddWithValue("@id", StudentId);
                return Command.ExecuteNonQuery();

            }
            return 0;
        }


        /// <summary>
        /// It updates a student in the database.
        /// </summary>
        /// <example>
        /// PUT: api/Student/UpdateStudent/5
        /// Headers: Content-Type: application/json
        /// Request Body:
        /// {
        ///	    "Firstname":"Jinal",
        ///	    "Lastname":"Patel",
        ///	    "EmployeeNumber":"N67585",
        ///	    "Hiredate":"2024-12-24"
        /// } -> 
        /// {
        ///     "Firstname":"Jinal",
        ///	    "Lastname":"Patel",
        ///	    "EmployeeNumber":"N67585",
        ///	    "Hiredate":"2024-12-24"
        /// }
        /// </example>
        /// <param name="StudentId">Student Object</param>
        /// <param name="StudentData">The Student ID primary key</param>
        /// <returns>
        /// It returns the updated Student object
        /// </returns>

        [HttpPut(template: "UpdateStudent/{StudentId}")]
        public Student UpdateStudent(int StudentId, [FromBody] Student StudentData)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();

                Command.CommandText = "update students set studentfname=@studentfname, studentlname=@studentlname, studentnumber=@studentnumber, enroldate=@enroldate where studentid=@id";
                Command.Parameters.AddWithValue("@studentfname", StudentData.FirstName);
                Command.Parameters.AddWithValue("@studentlname", StudentData.LastName);
                Command.Parameters.AddWithValue("@studentnumber", StudentData.StudentNumber);
                Command.Parameters.AddWithValue("@enroldate", StudentData.EnrolDate);

                Command.Parameters.AddWithValue("@id", StudentId);


                Command.ExecuteNonQuery();



            }

            return FindStudent(StudentId);
        }
    }
}
