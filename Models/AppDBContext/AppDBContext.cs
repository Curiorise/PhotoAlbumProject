using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPhotoAlbumProject.Models.AppDBContext
{
    public class AppDBContext : IdentityDbContext 
    {
        private readonly DbContextOptions<AppDBContext> _options;

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            _options = options;
        }

        public DbSet<PhotoModel> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
