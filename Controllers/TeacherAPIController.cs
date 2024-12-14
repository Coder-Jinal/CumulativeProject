using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CumulativeProject.Controllers;
using MySql.Data.MySqlClient;
using CumulativeProject.Models;
using System;

namespace CumulativeProject.Controllers
{
    [Route("api/Teacher")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// It returns a list of teachers from the given database named school
        /// </summary>
        /// <example>
        /// GET api/Teacher/ListTeachers -> [{"teacherId":1,"firstName":"Alexander","lastName":"Bennett","hireDate":"2016-08-05T00:00:00","employeeNumber":"T378","salary":"55.30"},
        /// {"teacherId":2,"firstName":"Caitlin","lastName":"Cummings","hireDate":"2014-06-10T00:00:00","employeeNumber":"T381","salary":"62.77"},....]
        /// </example>
        /// <returns>
        /// Returns a list of teacher object
        /// </returns>
        [HttpGet]
        [Route(template: "ListTeachers")]
        public List<Teacher> ListTeachers()
        {
            List<Teacher> Teachers = new List<Teacher>();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "SELECT * FROM teachers";

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                            int Id = Convert.ToInt32(ResultSet["teacherid"]);
                            string fname = ResultSet["teacherfname"].ToString();
                            string lname = ResultSet["teacherlname"].ToString();
                            DateTime JoinDate = Convert.ToDateTime(ResultSet["hiredate"]);
                            string empno = ResultSet["employeenumber"].ToString();
                            decimal sal = Convert.ToDecimal(ResultSet["salary"]);

                        Teacher CurrentTeacher = new Teacher()
                        {
                            TeacherId = Id,
                            FirstName = fname,
                            LastName = lname,
                            HireDate = JoinDate,
                            EmployeeNumber = empno,
                            Salary = sal
                        };
                        Teachers.Add(CurrentTeacher);
                    }
                }
            }

            return Teachers;
        }



        /// <summary>
        /// It returns teacher from the given database named school using TeacherId
        /// </summary>
        /// <example>
        /// GET api/Teacher/FindTeacher/4 -> {"teacherId":4,"firstName":"Lauren","lastName":"Smith","hireDate":"2014-06-22T00:00:00","employeeNumber":"T385","salary":"74.20"}
        /// </example>
        /// <returns>
        /// Returns a teacher object
        /// </returns>
        [HttpGet]
        [Route("FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher SelectedTeacher = new Teacher();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "SELECT * FROM teachers WHERE teacherid = @id";
                Command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        int Id = Convert.ToInt32(ResultSet["teacherid"]);
                        string fname = ResultSet["teacherfname"].ToString();
                        string lname = ResultSet["teacherlname"].ToString();
                        DateTime JoinDate = Convert.ToDateTime(ResultSet["hiredate"]);
                        string empno = ResultSet["employeenumber"].ToString();
                        decimal sal = Convert.ToDecimal(ResultSet["salary"]);

                        SelectedTeacher.TeacherId = Id;
                        SelectedTeacher.FirstName = fname;
                        SelectedTeacher.LastName = lname;
                        SelectedTeacher.HireDate = JoinDate;
                        SelectedTeacher.EmployeeNumber = empno;
                        SelectedTeacher.Salary = sal;

                    }
                }
            }
            return SelectedTeacher;
        }


        /// <summary>
        ///  It adds the teacher to the database named teachers
        /// </summary>
        /// <param name="TeacherData">Teacher Object</param>
        /// <example>
        /// POST: api/TeacherData/AddTeacher
        /// Headers: Content-Type: application/json
        /// Request Body:
        /// {
        ///     "firstName": "Jinal",
        ///     "lastName": "Patel",
        ///     "hireDate": "2016-08-05T00:00:00",
        ///     "employeeNumber": "T453",
        ///     "salary": 78.37  
        /// } -> 23
        /// </example>
        /// <returns>
        /// It returns the inserted Teacher Id from the database if successful. Or 0 if Unsuccessful
        /// </returns>
        [HttpPost(template: "AddTeacher")]
        public int AddTeacher([FromBody] Teacher TeacherData)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();

                Command.CommandText = "insert into teachers (teacherfname, teacherlname, hiredate, employeenumber, salary) values (@teacherfname, @teacherlname, @hiredate, @employeenumber, @salary)";
                Command.Parameters.AddWithValue("@teacherfname", TeacherData.FirstName);
                Command.Parameters.AddWithValue("@teacherlname", TeacherData.LastName);
                Command.Parameters.AddWithValue("@employeenumber", TeacherData.EmployeeNumber);
                Command.Parameters.AddWithValue("@hiredate", TeacherData.HireDate);
                Command.Parameters.AddWithValue("@salary", TeacherData.Salary);
              
                Command.ExecuteNonQuery();

                return Convert.ToInt32(Command.LastInsertedId);

            }
            return 0;
        }



        /// <summary>
        /// It deletes a teacher from the database named teachers
        /// </summary>
        /// <param name="TeacherId">Primary key of the teacher to delete</param>
        /// <eaxample>
        /// DELETE: api/TeacherData/DeleteTeacher -> 1
        /// </eaxample>
        /// <returns>
        /// It returns a number of rows affected by delete operation.
        /// </returns>
        [HttpDelete(template: "DeleteTeacher/{TeacherId}")]
        public int DeleteTeacher(int TeacherId)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                //Establish a new command (query) for our database
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "delete from teachers where teacherid=@id";
                Command.Parameters.AddWithValue("@id", TeacherId);
                return Command.ExecuteNonQuery();

            }
            return 0;
        }



        /// <summary>
        /// Updates a teacher in the database. Data is Teacher object, request query contains ID
        /// </summary>
        /// <param name="TeacherId">Teacher Object</param>
        /// <param name="TeacherData">The Teacher ID primary key</param>
        /// <example>
        /// PUT: api/Teacher/UpdateTeacher/4
        /// Headers: Content-Type: application/json
        /// Request Body:
        /// {
        ///	    "Firstname":"Jinal",
        ///	    "Lastname":"Patel",
        ///	    "Hiredate":"2024-12-01",
        ///	    "EmployeeNumber":"N106",
        ///	    "Salary":"5.00"
        /// } -> 
        /// {
        ///     "Firstname":"Jinal",
        ///	    "Lastname":"Patel",
        ///	    "Hiredate":"2024-12-01",
        ///	    "EmployeeNumber":"N106",
        ///	    "Salary":"5.00"
        /// }
        /// </example>
        /// <returns>
        /// It returns the updated Teacher object
        /// </returns>
        [HttpPut(template: "UpdateTeacher/{TeacherId}")]
        public Teacher UpdateTeacher(int TeacherId, [FromBody] Teacher TeacherData)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();

                Command.CommandText = "update teachers set teacherfname=@teacherfname, teacherlname=@teacherlname, hiredate=@hiredate, employeenumber=@employeenumber, salary=@salary where teacherid=@id";
                Command.Parameters.AddWithValue("@teacherfname", TeacherData.FirstName);
                Command.Parameters.AddWithValue("@teacherlname", TeacherData.LastName);
                Command.Parameters.AddWithValue("@hiredate", TeacherData.HireDate);
                Command.Parameters.AddWithValue("@employeenumber", TeacherData.EmployeeNumber);
                Command.Parameters.AddWithValue("@salary", TeacherData.Salary);

                Command.Parameters.AddWithValue("@id", TeacherId);

                Command.ExecuteNonQuery();



            }

            return FindTeacher(TeacherId);
        }
    }
}
