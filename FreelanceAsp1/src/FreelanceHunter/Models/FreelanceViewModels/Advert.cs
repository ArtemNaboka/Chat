using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceAsp.Models.FreelanceViewModel
{
    public class Advert
    {
        public int Id { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string Task { get; set; }

        [Required]
        [Range(10, 10000, ErrorMessage = "Цена услуги должна быть в диапазоне от 10 до 10 000")]
        public decimal Price { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string DateOfCreate { get; set; }

        public bool IsRelevant { get; set; }

        public virtual List<Offer> Offers { get; set; }
    }
}
