using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Recipe.Entities;


namespace DataAccess.EFCore
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Recipe>().HasMany(r => r.Categories);
            modelBuilder.Entity<Category>().HasMany(c => c.Recipes);
            modelBuilder.Entity<Recipe>().HasMany(r => r.Ingredients).WithOne(i => i.Recipe);
            modelBuilder.Entity<Recipe>().HasMany(r => r.Directions).WithOne(d => d.Recipe);
        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Direction> Directions { get; set; }
    }
}
