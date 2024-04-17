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
    public class NoteConfiguration:IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(n => n.Id); 

            builder.Property(n => n.NoteContent)
                .IsRequired(); // NoteContent is required

            // Define foreign key relationships
            builder.HasOne(n => n.Book)
                .WithMany(b => b.Notes)
                .HasForeignKey(n => n.BookId)
                .IsRequired(); 

            builder.HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId)
                .IsRequired(); 

        }
    }
}
