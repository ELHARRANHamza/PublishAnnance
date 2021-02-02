using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.ViewModel
{
    public class News_ViewModel
    {
        public int id { get; set; }
        [DataType(DataType.Url)]
        public string url { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime date_Publiciter { get; set; }
        public IFormFile file { get; set; }
    }
}
