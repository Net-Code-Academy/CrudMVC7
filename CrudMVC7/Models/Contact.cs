using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CrudMVC7.Models
{
    public class Contact
    {
        [Key]
        [DisplayName("DNI")]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El apellido materno es obligatorio")]
        public string MotherLastName { get; set; }

        [Required(ErrorMessage = "El número de celular es obligatorio")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [StringLength(9)]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatorio")]       
        [Display(Name = "Fecha")]
        [DataType(DataType.Date, ErrorMessage = "Fecha no valida")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio")]
        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public List<ImmunizationPatient>? ImmunizationPatients { get; set; }
    }
}
