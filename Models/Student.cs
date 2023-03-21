using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Student
    {
        [Key]
        public int StuId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [DisplayName("Father Name")]
        public string? FatherName{ get; set; }
        [Required]
        public string? Age{ get; set; }
        [Required]
        [DisplayName("Class")]
        public string? Standard { get; set; }
   



    }
}
