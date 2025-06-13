using OfficeNet.Infrastructure.Mapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OfficeNet.Domain.Entities
{
    public class OpinionPollTopic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }             
        public string Topic { get; set; }         
        public bool SelectionType { get; set; }   
        public bool ShowResults { get; set; }
        [JsonConverter(typeof(DdMmYyyyDateConverter))]
        public DateTime FromDate { get; set; }
        [JsonConverter(typeof(DdMmYyyyDateConverter))]
        public DateTime ToDate { get; set; }      
        public bool IsLive { get; set; }          
        public bool Status { get; set; }          
        public bool Archive { get; set; }         
        public int Uid { get; set; }              
        public int PlantId { get; set; }
        public string? CreatedBy { get; set; }
        [JsonConverter(typeof(DdMmYyyyDateConverter))]
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        [JsonConverter(typeof(DdMmYyyyDateConverter))]
        public DateTime ModifiedOn { get; set; }
    }
}
