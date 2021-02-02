using Annonce_Asp.netCore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.ViewModel
{
    public class Create_AnnonceViewModel
    {
        public int id { get; set; }
        public string Ann_Texte { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Ann_Email { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string Telephone { get; set; }
        [Required]
        [StringLength(6, MinimumLength = 4)]
        public string Code_Postal { get; set; }

        public string images { get; set; }
        public int pos { get; set; }
        public DateTime date_Publiciter { get; set; }
        public int id_Cat { get; set; }
        public IList<Categories> categories { get; set; }
        public int id_Ville { get; set; }
        public IList<Ville> ville { get; set; }
        public int id_region { get; set; }
        public IList<Region>  region { get; set; }
        public AppUsers users { get; set; }
        public IFormFile file { get; set; }
    }
}
