using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Data;
using ApiTest.Dtos.Student;
using ApiTest.Mappers;
using ApiTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiTest.Controller.Course;
using Microsoft.AspNetCore.Http;

namespace ApiTest.Controller.Student
{

    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StudentController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students.Select(n => n.ToDto()).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student.ToDto());
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudentsByCourse(int courseId)
        {
            var students = await _context.Students.Where(s => s.courseId == courseId).ToListAsync();
            return Ok(students.Select(n => n.ToDto()).ToList());
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] CreateStudentRequestDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var existingStudentByEmail = await _context.Students.FirstOrDefaultAsync(s => s.email == createDto.email);
            if (existingStudentByEmail != null)
            {
                return BadRequest("El estudiante con este correo electrónico ya está inscrito en un curso.");
            }
            
           
            var courseExists = await _context.Courses.AnyAsync(c => c.id == createDto.courseId);
            if (!courseExists)
            {
                return BadRequest($"El curso con ID {createDto.courseId} no existe.");
            }

            var student = createDto.ToStudentFromCreateDto();
     
             {
                 student.name = createDto.name;
                 student.email = createDto.email;
                 student.phone = createDto.phone;
                 student.courseId = createDto.courseId;
             }
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var courseName = await GetCourseNameById(createDto.courseId);
            await FirebaseHelper.SendPushNotificationToTopicAsync(
                topic: "course_notifications",
                title: "Se agregó un nuevo estudiante",
                body: $"Estudiante: {student.name}, se ha inscrito al curso: {courseName}"
            );
            
            return CreatedAtAction(nameof(GetStudent), new { id = student.id }, student.ToDto());
        }

        private async Task<string> GetCourseNameById(int courseId)
        {
            var course = await _context.Courses
                .Where(c => c.id == courseId)
                .Select(c => c.name)
                .FirstOrDefaultAsync();

            return course ?? "Curso no encontrado";
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDto>> UpdateStudent(int id, [FromBody] UpdateStudentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }


            if (student.email != updateDto.email)
            {
                var existingStudentWithNewEmail = await _context.Students.FirstOrDefaultAsync(s => s.email == updateDto.email && s.id != id);
                if (existingStudentWithNewEmail != null)
                {
                    return BadRequest("El nuevo correo electrónico ya está en uso por otro estudiante.");
                }
            }

            student.name = updateDto.name;
            student.email = updateDto.email;
            student.phone = updateDto.phone;

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(student.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}