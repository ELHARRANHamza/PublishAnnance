using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models
{
    public class Latest_News
    {
        public int id { get; set; }
        [DataType(DataType.Url)]
        public string url { get; set; }
        public string  Titre { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime date_Publiciter { get; set; }
    }
}
