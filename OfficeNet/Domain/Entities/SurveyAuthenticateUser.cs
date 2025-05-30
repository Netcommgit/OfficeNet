using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OfficeNet.Domain.Entities
{
    public class SurveyAuthenticateUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SurveyId { get; set; }           
        public string UserId { get; set; }             
        public int? PlantId { get; set; }            
        public bool? IsSubmitted { get; set; } = false;
        public DateTime? SubmittedDate { get; set; }
        public int? AuthType { get; set; }           
        public bool? Archive { get; set; }           
        public string? CreatedBy { get; set; }       
        public DateTime? CreatedOn { get; set; }    
        public string? ModifiedBy { get; set; }      
        public DateTime? ModifiedOn { get; set; }   
    }
}
