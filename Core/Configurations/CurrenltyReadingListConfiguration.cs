using Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configurations
{
    public class CurrenltyReadingListConfiguration:IEntityTypeConfiguration<CurrentlyReadingList>
    {
        public void Configure(EntityTypeBuilder<CurrentlyReadingList> builder)
        {
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.AppUserId)
                .IsRequired();

            builder.HasOne(u => u.AppUser)
            .WithOne(f => f.CurrentlyReadingList)
            .HasForeignKey<CurrentlyReadingList>(e => e.AppUserId);
        }
    }
}
