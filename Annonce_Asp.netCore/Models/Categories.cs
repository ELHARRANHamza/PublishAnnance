using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models
{
    public class Categories
    {
        public int id { get; set; }
        [Required]
        [MinLength(6)]
        public string nom_Cat { get; set; }
        public IList<Annonces> GetAnnonces { get; set; }
        public string Image { get; set; }
    }
}
