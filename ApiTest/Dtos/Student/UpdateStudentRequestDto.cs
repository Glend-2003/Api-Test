using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTest.Dtos.UpdateStudentRequestDto
{
    public class UpdateStudentRequestDto
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
    }
}