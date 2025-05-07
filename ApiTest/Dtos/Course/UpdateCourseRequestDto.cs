using System;
using System.ComponentModel.DataAnnotations;

namespace ApiTest.Dtos.Course
{
    public class UpdateCourseRequestDto
    {
        [Required]
        [RegularExpression(@"^.*\D.*$", ErrorMessage = "El campo {0} no puede contener solo números.")]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [RegularExpression(@"^.*\D.*$", ErrorMessage = "El campo {0} no puede contener solo números.")]
        [StringLength(200)]
        public string description { get; set; }

        [StringLength(500)]
        public string imageUrl { get; set; }

        [Required]
        [RegularExpression(@"^.*\D.*$", ErrorMessage = "El campo {0} no puede contener solo números.")]
        [StringLength(100)]
        public string schedule { get; set; }

        [Required]
        [RegularExpression(@"^.*\D.*$", ErrorMessage = "El campo {0} no puede contener solo números.")]
        [StringLength(100)]
        public string professor { get; set; }
    }
}