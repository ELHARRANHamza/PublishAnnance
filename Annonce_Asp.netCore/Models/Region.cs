using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models
{
    public class Region
    {
        public int id { get; set; }
        [Required]
        [StringLength(100,MinimumLength = 6)]
        public string Region_Nom { get; set; }
        public IList<Annonces> GetAnnonces { get; set; }
        public IList<Ville> GetVilles { get; set; }
    }
}
