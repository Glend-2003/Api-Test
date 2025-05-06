using ApiTest.Dtos.Course;
using ApiTest.Models;

namespace ApiTest.Mappers.CourseMappers
{
    public static class CourseMapper {
        public static CourseDto ToDto(this Course course) {
            return new CourseDto {
                id = course.id,
                name = course.name,
                description = course.description,
                imageUrl = course.imageUrl,
                schedule = course.schedule,
                professor = course.professor
            };
        }
        
        public static Course ToCourseFromCreateDto(this CreateCourseRequestDto createDto) {
            return new Course {
                name = createDto.name,
                description = createDto.description,
                schedule = createDto.schedule,
                professor = createDto.professor
            };
        }
    }
}