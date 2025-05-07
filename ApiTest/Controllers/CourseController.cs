using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Data;
using ApiTest.Dtos.Course;
using ApiTest.Mappers;
using ApiTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controller.Course
{
    //test de endpoint http://localhost:5237/swagger/index.html
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CourseController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            return Ok(courses.Select(n => n.ToDto()).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course.ToDto());
        }
        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse([FromBody] CreateCourseRequestDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = createDto.ToCourseFromCreateDto();

             {
                 course.name = createDto.name;
                 course.description = createDto.description;
                 course.imageUrl = createDto.imageUrl;
                 course.schedule = createDto.schedule;
                 course.professor = createDto.professor;
             }
            
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.id }, course.ToDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, UpdateCourseRequestDto updateDto)
        {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            course.name = updateDto.name;
            course.description = updateDto.description;
            course.imageUrl = updateDto.imageUrl;
            course.schedule = updateDto.schedule;
            course.professor = updateDto.professor;

            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var isCourseAssociatedWithStudents = await _context.Students.AnyAsync(s => s.courseId == id);
            if (isCourseAssociatedWithStudents)
            {
                return BadRequest("No se puede eliminar el curso porque tiene estudiantes asociados.");
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}