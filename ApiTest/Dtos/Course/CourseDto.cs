using System;
using System.ComponentModel.DataAnnotations;

namespace ApiTest.Dtos.Course
{
    public class CourseDto
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }
       [Required]
        [StringLength(200)]
        public string description { get; set; }
       
        public string imageUrl { get; set; }
        [Required]
        [StringLength(100)]
        public string schedule { get; set; }
        [Required]
        [StringLength(100)]
        public string professor { get; set; }
    }
}