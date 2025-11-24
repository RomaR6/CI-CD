using Microsoft.AspNetCore.Mvc;

using WEB_API.Models;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents([FromQuery] string? name)
        {
            
            IEnumerable<Student> students = InMemoryDb.Students;

            
            if (!string.IsNullOrWhiteSpace(name))
            {
                students = students.Where(s =>
                    (s.FirstName != null && s.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase)) ||
                    (s.LastName != null && s.LastName.Contains(name, StringComparison.OrdinalIgnoreCase)));
            }

            
            return Ok(students);
        }

        
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            var student = InMemoryDb.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                            return NotFound(); 
            
            return Ok(student); 
        }

        
        [HttpPost]
        public ActionResult<Student> CreateStudent(Student student)
        {
           

            student.Id = InMemoryDb.Students.Any() ? InMemoryDb.Students.Max(s => s.Id) + 1 : 1;
            InMemoryDb.Students.Add(student);

            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student updatedStudent)
        {
            var student = InMemoryDb.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound(); 
            }

      

            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            student.Email = updatedStudent.Email;
            student.PhoneNumber = updatedStudent.PhoneNumber;
            student.Course = updatedStudent.Course;

            return NoContent(); 
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = InMemoryDb.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound(); 
            }

            InMemoryDb.Students.Remove(student);
            return NoContent(); 
        }
    }
}