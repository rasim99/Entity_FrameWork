using Entity_FrameWork.Constants;
using Entity_FrameWork.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_FrameWork.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Teacher> Teachers {  get; set; }
       public DbSet<Group> Groups {  get; set; }
       public DbSet<Student> Students {  get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.DefaultConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Group>().Property(x => x.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<Teacher>().Property(x => x.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<Student>().Property(x => x.IsDeleted).HasDefaultValue(false);

            
            modelBuilder.Entity<Group>().HasQueryFilter(h=>h.IsDeleted==false);
            modelBuilder.Entity<Teacher>().HasQueryFilter(h=>h.IsDeleted==false);
            modelBuilder.Entity<Student>().HasQueryFilter(h=>h.IsDeleted==false);

        }
    }
}
