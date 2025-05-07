using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTest.Dtos.Student
{
    public class UpdateStudentRequestDto
    {
        [Required]
        [RegularExpression(@"^.*\D.*$", ErrorMessage = "El campo {0} no puede contener solo números.")]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(100)]
        public string phone { get; set; }
    }
}