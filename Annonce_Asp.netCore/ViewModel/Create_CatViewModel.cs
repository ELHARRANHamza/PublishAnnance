using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.ViewModel
{
    public class Create_CatViewModel
    {
        public int id { get; set; }
        [Required]
        [MinLength(6)]
        public string nom_Cat { get; set; }
        public string Image { get; set; }
        public IFormFile GetFile { get; set; }
    }
}
