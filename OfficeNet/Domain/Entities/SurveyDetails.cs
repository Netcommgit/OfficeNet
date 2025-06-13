using OfficeNet.Infrastructure.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace OfficeNet.Domain.Entities
{
    public class SurveyDetails
    {
        [Key]
        public int SurveyId { get; set; }
        public string? SurveyName { get; set; }
        
        [JsonConverter(typeof(DdMmYyyyDateConverter))]
        public DateTime? SurveyStart { get; set; }
        [JsonConverter(typeof(DdMmYyyyDateConverter))]
        public DateTime? SurveyEnd { get; set; }
        public string? SurveyInstruction { get; set; }
        public string? SurveyConfirmation {get;set;}
        public int? SurveyView {get;set;}
        public int AuthView { get;set;}
        public int? PlantId { get; set; }
        public int? DepartmentId {get;set;}
        public bool IsExcel { get; set; }
        public bool SurveyStatus { get; set; }
        public bool Archieve { get; set; }
        public string? CreatedBy { get;set;}
        public DateTime? CreatedOn { get;set;}
        public string? ModifiedBy { get;set;}
        public DateTime? ModifiedOn { get; set; }
       
    }
}
