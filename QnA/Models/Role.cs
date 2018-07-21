using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QnA.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required(ErrorMessage="Name field is required!!!")]
        public string Name { get; set; }
    }
}
