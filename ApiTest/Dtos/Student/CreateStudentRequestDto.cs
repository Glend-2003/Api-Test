using System;
using System.ComponentModel.DataAnnotations;

namespace ApiTest.Dtos.Student
{
    public class CreateStudentRequestDto
    {
        [Required]
         [StringLength(100)]
        public string name { get; set; }
        [Required]
         [StringLength(100)]
        public string email { get; set; }
        [Required]
         [StringLength(100)]
        public string phone { get; set; }

        [Required] 
        public int courseId { get; set; } 
    }
}