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
    public class BookConfiguration:IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {


            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();
            builder.Property(c => c.Title)
                .IsRequired().HasMaxLength(20);
            builder.Property(c => c.Description)
                .IsRequired(false);
            builder.Property(c => c.Author)
                .IsRequired(false).HasMaxLength(20);
            builder.Property(c => c.PublishTime)
                .IsRequired(false);
            builder.Property(c => c.ImageUrl)
                .IsRequired(false);
            builder.Property(c => c.Content)
                .IsRequired(false);
            builder.Property(c => c.CategoryId)
                .IsRequired(false);

            builder.HasOne(c => c.Category) // Category has many Books
                .WithMany(b => b.Books) // Book belongs to one Category
                .HasForeignKey(b => b.CategoryId) // Define foreign key
                .IsRequired(false); // CategoryId is Nullable in Book


            // Indexing for Faster Operations 
            builder.HasIndex(c => c.CategoryId);

        }
    }
}
