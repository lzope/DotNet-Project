using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication5.DTO;
using WebApplication5.Models;

namespace WebApplication5.Controllers.Data
{
    public class UserContext : IdentityDbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<SalesLeadEntity>NewUsers{ get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<UserList> userList { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map UserView to your SQL view
            modelBuilder.Entity<UserList>().ToTable("aspnetusers");
        }
    }
}




   