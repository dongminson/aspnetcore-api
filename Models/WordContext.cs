using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_api.Models
{
    public class WordContext : DbContext
    {
        public WordContext(DbContextOptions<WordContext> options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Word.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordEntry>().HasMany(w => w.Definitions).WithOne(a => a.Word).HasForeignKey(a => a.WordId);
            modelBuilder.Seed();
        }
        public DbSet<WordEntry> Words { get; set; }
        public DbSet<DefinitionEntry> Definitions { get; set; }

    }
}
