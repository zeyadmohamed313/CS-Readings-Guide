using Core.Configurations;
using Core.Entites;
using Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class AppDbContext:IdentityDbContext<AppUser,IdentityRole,string>
    {
        #region Fields
        #endregion
        #region Constructors
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.
                UseSqlServer("Data Source=DESKTOP-8QKV55J\\SQLEXPRESS;Initial Catalog=Cs Readings Guide;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new FavouriteListConfiguration());
            modelBuilder.ApplyConfiguration(new CurrenltyReadingListConfiguration());
            modelBuilder.ApplyConfiguration(new ToReadListConfiguration());
            modelBuilder.ApplyConfiguration(new ReadListConfiguration());
            modelBuilder.ApplyConfiguration(new NoteConfiguration());

        }
        #endregion

        #region TablesInMyDb
        public DbSet<AppUser> appUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<CurrentlyReadingList> CurrentlyReadingLists { get; set; }
        public DbSet<ToReadList> ToReadLists { get; set; }
        public DbSet<ReadList> ReadLists { get; set; }
        public DbSet<FavouriteList> FavouriteLists { get; set;}

        #endregion

    }
}
