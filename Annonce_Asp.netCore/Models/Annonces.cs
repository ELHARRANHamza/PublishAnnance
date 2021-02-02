using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models
{
    public class Annonces
    {
        public int id { get; set; }
        [Required]
        [MinLength(6)]
        public string Ann_Texte { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Ann_Email { get; set; }
        [Required]
        [StringLength(11,MinimumLength =11)]
        public string Telephone { get; set; }
        [Required]
        [StringLength(6,MinimumLength = 4)]
        public string Code_Postal { get; set; }

        public string images { get; set; }
        public int pos { get; set; }
        public DateTime date_Publiciter { get; set; }
        public Categories categories { get; set; }
        public Ville ville { get; set; }
        public Region region { get; set; }
        public AppUsers users { get; set; }

    }
}
