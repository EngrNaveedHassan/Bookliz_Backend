using Bookliz_Backend.Models;
using Bookliz_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookliz_Backend.Controllers
{
    [Authorize(Roles = "Librarian")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly ILibrarianService _librarianService;

        public UserController(IStudentService studentService, ITeacherService teacherService, ILibrarianService librarianService)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _librarianService = librarianService;
        }

        //
        //  Students
        //
        // GET: api/user/getstudents
        [HttpGet("getstudents")]
        public IActionResult GetAllStudents()
        {
            var students = _studentService.GetAllStudents();
            if (students == null)
                return NotFound("No students found.");

            return Ok(students);
        }

        // POST: api/user/addstudent
        [HttpPost("addstudent")]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (student == null)
                return BadRequest("Student data is required.");

            var isAdded = await _studentService.CreateStudentAsync(student);
            if (!isAdded)
                return BadRequest("Failed to add student.");

            return Ok("Student Added Successfully.");
        }

        // GET: api/user/getstudent/{RegNumber}
        [HttpGet("getstudent/{RegNumber}")]
        public async Task<IActionResult> GetStudent(string RegNumber)
        {
            var student = await _studentService.GetStudentByRegNumberAsync(RegNumber);
            if (student == null)
                return NotFound("Student not found.");

            return Ok(student);
        }

        // POST: api/user/updatestudent
        [HttpPost("updatestudent/{RegNumber}")]
        public async Task<IActionResult> UpdateStudent(string RegNumber, [FromBody] Student student)
        {
            if (student == null)
                return BadRequest("Student data is required.");

            var isUdated = await _studentService.UpdateStudentAsync(RegNumber, student);
            if (!isUdated)
                return BadRequest("Failed to update student.");

            return Ok("Student Updated Successfully.");
        }

        // DELETE: api/user/deletestudent
        [HttpDelete("deletestudent/{RegNumber}")]
        public async Task<IActionResult> DeleteStudent(string RegNumber)
        {
            if (string.IsNullOrEmpty(RegNumber))
                return BadRequest("Registration number is required.");

            var isDeleted = await _studentService.DeleteStudentAsync(RegNumber);
            if (!isDeleted)
                return NotFound("Student not found.");

            return Ok("Student Deleted Successfully.");
        }


        //
        //  Teachers
        //
        // GET: api/user/getteachers
        [HttpGet("getteachers")]
        public IActionResult GetAllTeachers()
        {
            var teachers = _teacherService.GetAllTeachers();
            if (teachers == null || !teachers.Any())
            {
                return NotFound("No teachers found.");
            }
            return Ok(teachers);
        }

        // GET: api/user/getteacher/{EmpNumber}
        [HttpGet("getteacher/{EmpNumber}")]
        public async Task<IActionResult> GetTeacher(string EmpNumber)
        {
            var teacher = await _teacherService.GetTeacherByEmpNumberAsync(EmpNumber);
            if (teacher == null)
                return NotFound("Teacher not found.");

            return Ok(teacher);
        }

        // POST: api/user/addteacher
        [HttpPost("addteacher")]
        public async Task<IActionResult> AddTeacher([FromBody] Teacher teacher)
        {
            if (teacher == null)
                return BadRequest("Teacher data is required.");

            var isAdded = await _teacherService.CreateTeacherAsync(teacher);
            if (!isAdded)
                return BadRequest("Failed to add teacher.");

            return Ok("Teacher Added Successfully.");
        }

        // DELETE: api/user/deleteteacher
        [HttpDelete("deleteteacher/{EmpNumber}")]
        public async Task<IActionResult> DeleteTeacher(string EmpNumber)
        {
            if (string.IsNullOrEmpty(EmpNumber))
                return BadRequest("Employee number is required.");

            var isDeleted = await _teacherService.DeleteTeacherAsync(EmpNumber);
            if (!isDeleted)
                return NotFound("Teacher not found.");

            return Ok("Teacher Deleted Successfully.");
        }

        // POST: api/user/updateteacher
        [HttpPost("updateteacher/{EmpNumber}")]
        public async Task<IActionResult> UpdateTeacher(string EmpNumber, [FromBody] Teacher teacher)
        {
            if (teacher == null)
                return BadRequest("Teacher data is required.");

            var isUpdated = await _teacherService.UpdateTeacherAsync(EmpNumber, teacher);
            if (!isUpdated)
                return NotFound("Teacher not found.");

            return Ok("Teacher Updated Successfully.");
        }


        //
        //  Librarians
        //
        //POST: api/user/addlibrarian
        [HttpPost("addlibrarian")]
        public IActionResult AddLibrarian([FromBody] Librarian librarian)
        {
            if (librarian == null)
            {
                return BadRequest("Librarian data is required.");
            }

            _librarianService.AddLibrarian(librarian);
            return Ok("Librarian Added Successfully.");
        }

        // POST: api/user/updatelibrarian
        [HttpPost("updatelibrarian/{EmpNumber}")]
        public IActionResult UpdateLibrarian([FromBody] Librarian librarian)
        {
            if (librarian == null)
            {
                return BadRequest("Librarian data is required.");
            }

            _librarianService.UpdateLibrarian(librarian);
            return Ok("Librarian Updated Successfully.");
        }

        // DELETE: api/user/deletelibrarian
        [HttpDelete("deletelibrarian/{EmpNumber}")]
        public IActionResult DeleteLibrarian(string EmpNumber)
        {
            if (string.IsNullOrEmpty(EmpNumber))
            {
                return BadRequest("Employee number is required.");
            }
            _librarianService.DeleteLibrarian(EmpNumber);
            return Ok("Librarian Deleted Successfully.");
        }

        // GET: api/user/getlibrarian/{EmpNumber}
        [HttpGet("getlibrarian/{EmpNumber}")]
        public IActionResult GetLibrarian(string EmpNumber)
        {
            var librarian = _librarianService.GetLibrarianByEmpNumber(EmpNumber);
            if (librarian == null)
            {
                return NotFound("Librarian not found.");
            }
            return Ok(librarian);
        }

        // GET: api/user/getlibrarians
        [HttpGet("getlibrarians")]
        public IActionResult GetLibrarians()
        {
            var librarians = _librarianService.GetAllLibrarians();
            if (librarians == null || !librarians.Any())
            {
                return NotFound("No librarians found.");
            }
            return Ok(librarians);
        }
    }
}