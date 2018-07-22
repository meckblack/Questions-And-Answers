using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QnA.Models
{
    public class Questions
    {
        public int QuestionsId { get; set; }

        [Required(ErrorMessage="Header field is required!!!")]
        public string Header { get; set; }

        [Required(ErrorMessage = "Body field is required!!!")]
        public string Body { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; }

        public string Time { get; set; }
    }
}
