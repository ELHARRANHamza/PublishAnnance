using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.ViewModel
{
    public class Change_Password
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme Password")]
        [Compare("newPassword",
         ErrorMessage = "Password and Confirmation Do Not Match.")]
        public string ComparePassword { get; set; }
    }
}
