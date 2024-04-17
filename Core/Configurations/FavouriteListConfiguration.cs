using Core.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Core.Configurations
{
    public class FavouriteListConfiguration : IEntityTypeConfiguration<FavouriteList>
    {       
            public void Configure(EntityTypeBuilder<FavouriteList> builder)
            {
                builder.HasKey(pk => pk.Id);
                builder.Property(p=>p.AppUserId)
                    .IsRequired();

                builder.HasOne(u => u.AppUser)
                .WithOne(f => f.FavouriteList)
                .HasForeignKey<FavouriteList>(e => e.AppUserId);
            }
    }
}
