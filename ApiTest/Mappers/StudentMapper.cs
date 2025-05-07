using ApiTest.Dtos.Student;
using ApiTest.Models;

namespace ApiTest.Mappers
{
    public static class StudentMapper {
        public static StudentDto ToDto(this Student student) {
            return new StudentDto {
                id = student.id,
                name = student.name,
                email = student.email,
                phone = student.phone,
                courseId = student.courseId
            };
        }
        
        public static Student ToStudentFromCreateDto(this CreateStudentRequestDto createDto) {
            return new Student {
                name = createDto.name,
                email = createDto.email,
                phone = createDto.phone,
                courseId = createDto.courseId
            };
        }
    }
}