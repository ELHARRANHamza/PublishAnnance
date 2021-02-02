using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.ViewModel
{
    public class Roles_ViewModels
    {
        public string id { get; set; }
        [Required]
        public string nom_role { get; set; }
    }
}
