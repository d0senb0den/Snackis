using Microsoft.EntityFrameworkCore;
using SnackisAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Data
{
    public class SnackisContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Report> Reports { get; set; }

        public SnackisContext(DbContextOptions<SnackisContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Category
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired();

            //SubCategory
            modelBuilder.Entity<SubCategory>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<SubCategory>().Property(s => s.Description).IsRequired();

                //Bygger relationen mellan Category och SubCategory.
            modelBuilder.Entity<SubCategory>().HasOne<Category>().WithMany().HasForeignKey(s => s.CategoryID).OnDelete(DeleteBehavior.Cascade);

            //Post
            modelBuilder.Entity<Post>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Post>().Property(p => p.Content).IsRequired();
            modelBuilder.Entity<Post>().Property(p => p.User).IsRequired();

            modelBuilder.Entity<Post>().HasOne<SubCategory>().WithMany().HasForeignKey(s => s.SubCategoryID).OnDelete(DeleteBehavior.Cascade);

            //Comment
            modelBuilder.Entity<Comment>().Property(c => c.Content).IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.UserID).IsRequired();

            modelBuilder.Entity<Comment>().HasOne<Post>().WithMany().HasForeignKey(s => s.PostID).OnDelete(DeleteBehavior.Cascade);

            //Message
            modelBuilder.Entity<Message>().Property(m => m.Content).IsRequired();
            modelBuilder.Entity<Message>().Property(m => m.FromUser).IsRequired();
            modelBuilder.Entity<Message>().Property(m => m.ToUser).IsRequired();

            //Report
            modelBuilder.Entity<Report>().Property(r => r.Content).IsRequired();
            modelBuilder.Entity<Report>().Property(r => r.Post).IsRequired();
            modelBuilder.Entity<Report>().Property(r => r.Comment).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
