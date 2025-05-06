using System;
using System.ComponentModel.DataAnnotations;
namespace ApiTest.Dtos.UpdateCourseRequestDto
{
    public class UpdateCourseRequestDto
    {
        [Required]
        [StringLength(100)]
        public string name { get; set; }
        [Required]
        [StringLength(200)]
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