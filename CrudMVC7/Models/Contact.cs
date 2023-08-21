using System.ComponentModel.DataAnnotations;

namespace CrudMVC7.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El apellido materno es obligatorio")]
        public string MotherLastName { get; set; }

        [Required(ErrorMessage = "El número de celular es obligatorio")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatorio")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es valido")]
        public string Email { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
