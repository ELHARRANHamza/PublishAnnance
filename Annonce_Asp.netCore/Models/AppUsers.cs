using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models
{
    public class AppUsers:IdentityUser
    {
        [Required]
        [StringLength(30,MinimumLength = 3)]
        public string nom { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string prenom { get; set; }
        public string image { get; set; }
        public string UserType { get; set; }
        public IList<Annonces> GetAnnonces { get; set; }

    }
}
