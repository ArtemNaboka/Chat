using FreelanceAsp.Models.FreelanceViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceHunter.Models.FreelanceViewModels
{
    public class CompletedTask
    {
        public int Id { get; set; }

        public int AdvertId { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
