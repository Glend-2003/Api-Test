using System;
using System.ComponentModel.DataAnnotations;

namespace ApiTest.Dtos.Course
{
    public class CreateCourseRequestDto
    {

        [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "El nombre del curso no puede consistir únicamente de espacios.")] 
        //[RegularExpression(@"^.*\D.*$", ErrorMessage = "El campo {0} no puede contener solo números.")]
        [StringLength(100)]
        public string name { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "La descripción no puede consistir únicamente de espacios.")]
        //[RegularExpression(@"^.*\D.*$", ErrorMessage = "El campo {0} no puede contener solo números.")]
        [StringLength(100)]
        public string description { get; set; }

        
        public IFormFile? imageUrl { get; set; }

        [Required(ErrorMessage = "El Horario del curso es obligatorio.")]
        [RegularExpression(@"^.*\D.*$", ErrorMessage = "El campo {0} no puede contener solo números.")]
        [StringLength(100)]
        public string schedule { get; set; }

        [Required(ErrorMessage = "El profesor es obligatorio.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "El profesor no puede consistir únicamente de espacios.")]
        //[RegularExpression(@"^.*\D.*$", ErrorMessage = "El campo {0} no puede contener solo números.")]
        [StringLength(100)]
        public string professor { get; set; }
    }
}