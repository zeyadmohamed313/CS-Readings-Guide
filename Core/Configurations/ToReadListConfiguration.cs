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
    public class ToReadListConfiguration:IEntityTypeConfiguration<ToReadList>
    {
        public void Configure(EntityTypeBuilder<ToReadList> builder)
        {
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.AppUserId)
                .IsRequired();

            builder.HasOne(u => u.AppUser)
            .WithOne(f => f.ToReadList)
            .HasForeignKey<ToReadList>(e => e.AppUserId);
        }
    }
}
