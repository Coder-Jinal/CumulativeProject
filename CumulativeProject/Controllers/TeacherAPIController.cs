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
                            DateOnly JoinDate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["hiredate"]));
                            string empno = ResultSet["employeenumber"].ToString();
                            string sal = ResultSet["salary"].ToString();

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
                        DateOnly JoinDate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["hiredate"]));
                        string empno = ResultSet["employeenumber"].ToString();
                        string sal = ResultSet["salary"].ToString();

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
    }
}
