using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrudMVC7.Models
{
    public class Immunization
    {
        [Key]
        public int ImmunizationId { get; set; }

        [Required(ErrorMessage = "El nombre de la vacuna es obligatorio")]
        [DisplayName("Denominación")]
        public string Name { get; set; }

        [Required(ErrorMessage ="El periodo de vacunación es obligatorio")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [DisplayName("Periodo")]
        public int Period { get; set; }

        [Required(ErrorMessage = "")]
        [DisplayName("Debe ingresar una descripción")]

        public string Description { get; set; }

        public List<ImmunizationPatient>? ImmunizationPatients { get; set; }
    }
}
