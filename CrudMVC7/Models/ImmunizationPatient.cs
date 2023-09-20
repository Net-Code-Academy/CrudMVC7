namespace CrudMVC7.Models
{
    public class ImmunizationPatient
    {
        public int Id { get; set; }

        public int ContactId { get; set; }

        public int ImmunizationId { get; set; }

        public DateTime ScheduledDate { get; set; }

        public DateTime? ApplicationDate { get; set; } = null;

        public string State { get; set; }

        public Contact? Contact { get; set; }

        public Immunization? Immunization { get; set; }
    }
}
