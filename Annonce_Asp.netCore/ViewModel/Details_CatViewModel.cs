using Annonce_Asp.netCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.ViewModel
{
    public class Details_CatViewModel
    {
        public int id { get; set; }
        public IList<Annonces> GetAnnonces { get; set; }
        public int id_Region { get; set; }
        public IList<Region> GetRegions { get; set; }
        public int id_Ville { get; set; }
        public IList<Ville> GetVilles { get; set; }
        public IList<Categories> GetContreCat { get; set; }

        public Categories Cat { get; set; }


    }
}
