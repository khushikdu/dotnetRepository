using Homework_6.Model;
using Homework_6.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Homework_6.Controllers
{
    /// <summary>
    /// Controller for managing student data.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentController"/> class.
        /// </summary>
        /// <param name="studentService">The student service.</param>
        /// <param name="logger">The logger.</param>
        public StudentController(StudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="student">The student to add.</param>
        /// <returns>The ID of the added student.</returns>
        [HttpPost]
        public IActionResult AddStudent([FromBody] StudentRequestDTO student)
        {
            try
            {
                _logger.LogInformation("Adding new student: {@student}", student);
                var id = _studentService.AddStudent(student);
                _logger.LogInformation("New student added with ID: {id}", id);
                return Ok(id.ToString());
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Failed to add student: {message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding student.");
                return StatusCode(500, "An unexpected error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Retrieves a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve.</param>
        /// <returns>The student with the specified ID.</returns>
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching student with ID: {id}", id);
                var student = _studentService.GetStudentById(id);
                if (student == null)
                {
                    _logger.LogInformation("Student not found with ID: {id}", id);
                    return NotFound($"Student not found with ID: {id}");
                }
                _logger.LogInformation("Student list returned successfully");
                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching student.");
                return StatusCode(500, "An unexpected error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Retrieves all students.
        /// </summary>
        /// <returns>A list of all students.</returns>
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            try
            {
                _logger.LogInformation("Fetching all students");
                var students = _studentService.GetAllStudents();
                if (students.Count == 0)
                {
                    _logger.LogInformation("Student list is empty");
                    return NotFound("Student list is empty");
                }
                _logger.LogInformation("Students details returned successfully");
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching students.");
                return StatusCode(500, "An unexpected error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Retrieves the total number of students.
        /// </summary>
        /// <returns>The total number of students.</returns>
        [HttpGet("total")]
        public IActionResult GetTotalStudents()
        {
            try
            {
                _logger.LogInformation("Fetching total count of students");
                var total = _studentService.GetTotalStudents();
                _logger.LogInformation("Total students: {total}", total);
                return Ok(total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching total count of students.");
                return StatusCode(500, "An unexpected error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                _logger.LogInformation("Deleting student with ID: {id}", id);
                var deleted = _studentService.DeleteStudent(id);
                if (deleted)
                {
                    _logger.LogInformation("Student deleted successfully with ID: {id}", id);
                    return Ok($"Student with Id : {id} deleted successfully");
                }
                _logger.LogInformation("Student not found with ID: {id}", id);
                return NotFound($"Student not found with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting student.");
                return StatusCode(500, "An unexpected error occurred while processing the request.");
            }
        }
    }
}