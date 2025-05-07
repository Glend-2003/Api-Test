using System;
using System.ComponentModel.DataAnnotations;

namespace ApiTest.Dtos.Student
{
    public class CreateStudentRequestDto
    {
        [Required(ErrorMessage = "El nombre del estudiante es obligatorio.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "El nombre del estudiante no puede consistir únicamente de espacios.")]
        //[RegularExpression(@"^.*\D.*$", ErrorMessage = "El campo {0} no puede contener solo números.")]
        [StringLength(100)]
        public string name { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido, tiene que ser Ejemplo: xxxxxx@gmail.com/.hotmail etc...")]
        [StringLength(100)]
        public string email { get; set; }

        [Required(ErrorMessage = "El numero del estudiante es obligatorio.")]
        //[RegularExpression(@"^\+?[0-8]{8,15}$", ErrorMessage = "El formato del teléfono no es válido. Ejemplo: +50612345678 o 12345678.")]
        [StringLength(100)]
        public string phone { get; set; }

        [Required(ErrorMessage = "El ID del curso es obligatorio.")]
        public int courseId { get; set; }
    }
}