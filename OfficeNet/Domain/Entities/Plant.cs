using System.ComponentModel.DataAnnotations;

namespace OfficeNet.Domain.Entities
{
    public class Plant
    {
        [Key]
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public string? PlantDescription { get; set; }
        public string? SAPCode { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool Status { get; set; }

    }
}
