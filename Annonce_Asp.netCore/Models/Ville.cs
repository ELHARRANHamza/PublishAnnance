using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models
{
    public class Ville
    {
        public int id { get; set; }
        [Required]
        [StringLength(45,MinimumLength = 3)]
        public string ville_Name { get; set; }
        public IList<Annonces> GetAnnonces { get; set; }
        public Region GetRegion { get; set; }
    }
}
