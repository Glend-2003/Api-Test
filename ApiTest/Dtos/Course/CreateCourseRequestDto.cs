using System;
using System.ComponentModel.DataAnnotations;

namespace ApiTest.Dtos.Course
{
    public class CreateCourseRequestDto
    {
        [Required]
        [StringLength(100)]
        public string name { get; set; }
        [Required]
        [StringLength(100)]
        public string description { get; set; }
        [StringLength(500)]
        public string imageUrl { get; set; }
        [Required]
        [StringLength(100)]
        public string schedule { get; set; }
        [Required]
        [StringLength(100)]
        public string professor { get; set; }
    }
}