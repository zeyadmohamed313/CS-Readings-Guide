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
    public class ReadListConfiguration:IEntityTypeConfiguration<ReadList>
    {
        public void Configure(EntityTypeBuilder<ReadList> builder)
        {
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.AppUserId)
                .IsRequired();

            builder.HasOne(u => u.AppUser)
            .WithOne(f => f.ReadList)
            .HasForeignKey<ReadList>(e => e.AppUserId);
        }
    }
}
