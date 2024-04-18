using Core.Entites.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configurations
{
    public class AppUserConfiguration:IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

            builder.Property(u => u.UserName)
                .IsRequired(); 

            builder.Property(u => u.Email)
                .IsRequired(); 

            builder.Property(u => u.FirstName)
                .IsRequired(); 

            builder.Property(u => u.LastName)
                .IsRequired(); 


          
                
            builder.HasMany(n => n.Notes)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .IsRequired(false);


            // Faster Operations With Indexing
            builder.HasIndex(u => u.Email).IsUnique();

        }
    }
}
