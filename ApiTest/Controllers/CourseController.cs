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
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");

        public CourseController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            var coursesDto = courses.Select(courses => courses.ToDto());
            return Ok(courses.Select(n => n.ToDto()).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCourse([FromRoute] int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(u => u.id == id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course.ToDto());
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromForm] CreateCourseRequestDto createDto)
        {   
            if(createDto.imageUrl == null || createDto.imageUrl.Length == 0){
                return BadRequest("No file uploades");
            }

            var course = createDto.ToCourseFromCreateDto();
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            var fileName = course.id.ToString() + Path.GetExtension(createDto.imageUrl.FileName);
            var filePath = Path.Combine(_imagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create)){
                await createDto.imageUrl.CopyToAsync(stream);
            }
            
            course.imageUrl = fileName;
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.id }, course.ToDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCourse([FromRoute] int id, UpdateCourseRequestDto updateDto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(course => course.id == id);
            if (course == null)
            {
                return NotFound();
            }

            course.name = updateDto.name;
            course.description = updateDto.description;
            course.schedule = updateDto.schedule;
            course.professor = updateDto.professor;

            await _context.SaveChangesAsync();

            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(course.ToDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(course => course.id == id);
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