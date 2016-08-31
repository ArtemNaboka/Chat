using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceAsp.Models.FreelanceViewModel
{
    public class Offer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }

        [Required]
        public string Text { get; set; }

        public string DateOfCreate { get; set; }

        public virtual Advert Advert { get; set; }
    }
}
