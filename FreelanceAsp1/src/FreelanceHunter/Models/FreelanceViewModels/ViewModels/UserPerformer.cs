using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceHunter.Models.FreelanceViewModels
{
    public class UserPerformer
    {
        [Key]
        public int AdvertId { get; set; }

        [Key]
        public string User { get; set; }
    }
}
