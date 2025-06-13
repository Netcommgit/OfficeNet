using OfficeNet.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeNet.Domain.Entities
{
    public class OpinionPollOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PollOptionId { get;set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public OpinionPollTopic Question { get; set; }
        public string OptionName { get; set; }
    }
}
