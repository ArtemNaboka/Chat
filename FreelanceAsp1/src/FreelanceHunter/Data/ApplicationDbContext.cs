using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FreelanceHunter.Models;
using FreelanceAsp.Models.FreelanceViewModel;
using FreelanceAsp.Models.FreelanceViewModels;
using FreelanceHunter.Models.FreelanceViewModels;

namespace FreelanceHunter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Advert> Adverts { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<ProfileComment> ProfileComments { get; set; }

        public DbSet<UserPerformer> UsersPerformers { get; set; }

        public DbSet<CompletedTask> CompletedTasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserPerformer>().HasKey(c => new { c.AdvertId, c.User });
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }
    }
}
