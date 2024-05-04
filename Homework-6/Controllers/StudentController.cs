using Homework_6.Model;
using Homework_6.Service;
using Microsoft.AspNetCore.Mvc;

namespace Homework_6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] StudentRequestDTO student)
        {
            var id = _studentService.AddStudent(student);
            return Ok(id.ToString());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound("Student not found with ID: " + id);
            }
            return Ok(student);
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _studentService.GetAllStudents();
            if (students.Count == 0)
            {
                return NotFound("Student list is emplty");
            }
            return Ok(students);
        }
        [HttpGet("total")]
        public IActionResult GetTotalStudents()
        {
            var total = _studentService.GetTotalStudents();
            return Ok(total);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var deleted = _studentService.DeleteStudent(id);
            if (deleted)
            {
                return NoContent();
            }
            return NotFound("Student not found with ID: " + id);
        }
    }
}
