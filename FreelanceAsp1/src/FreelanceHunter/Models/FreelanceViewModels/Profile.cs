using FreelanceAsp.Models.FreelanceViewModels;
using FreelanceHunter.Models.FreelanceViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceAsp.Models.FreelanceViewModel
{
    public class Profile
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Information { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public string Likers { get; set; }

        public string Dislikers { get; set; }

        public string Completed { get; set; }

        public virtual List<CompletedTask> CompletedTasks { get; set; }

        public virtual List<ProfileComment> ProfileComments { get; set; }

        public Profile()
        {

        }

        public Profile(string name)
        {
            Name = name;
        }

    }
}
