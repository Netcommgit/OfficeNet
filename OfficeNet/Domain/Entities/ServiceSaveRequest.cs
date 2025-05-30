namespace OfficeNet.Domain.Entities
{
    public class ServiceSaveRequest
    {
        public SurveyDetails SurveyDetails { get; set; }
        public List<string> UserList { get; set; }
        public SurveyQuestion surveyQuestion { get; set; }
        public List<string> questionType { get; set; }
    }
}
