using FreelanceAsp.Models.FreelanceViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceAsp.Models.FreelanceViewModels
{
    public class ProfileComment
    {
        public int Id { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Text { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
