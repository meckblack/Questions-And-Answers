using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QnA.Models
{
    public class Answers
    {
        public int AnswersId { get; set; }

        [Required(ErrorMessage ="Body field is required!!!")]
        public string Body { get; set; }

        public int QuestionsId { get; set; }
        [ForeignKey("QuestionsId")]
        public Questions questions { get; set; }

    }
}
