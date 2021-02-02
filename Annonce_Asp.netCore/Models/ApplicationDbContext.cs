using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annonce_Asp.netCore.Models
{
    public class ApplicationDbContext:IdentityDbContext<AppUsers>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Annonces> annonces { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Ville> villes { get; set; }
        public DbSet<Region> regions { get; set; }
        public DbSet<Latest_News> news { get; set; }

    }
}
