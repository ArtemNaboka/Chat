using FreelanceAsp.Models.FreelanceViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceHunter.Models.FreelanceViewModels.Rating
{
    public class Dislike
    {
        public int Id { get; set; }

        [Required]
        public string Disliker { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
