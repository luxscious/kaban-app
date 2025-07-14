using Microsoft.EntityFrameworkCore;
using KabanApp.Models;


namespace KabanApp.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TaskItem> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
            .Property(t => t.Status)
            .HasConversion<string>();
            base.OnModelCreating(modelBuilder);
        }
    }
}



