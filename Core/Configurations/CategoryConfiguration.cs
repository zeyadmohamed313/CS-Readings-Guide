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
    public class CategoryConfiguration:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();
            builder.Property(c => c.Name)
                .IsRequired().HasMaxLength(20);

            builder.Property(c => c.Description)
                .IsRequired(false); // Description is required

            builder.HasMany(c => c.Books) // Category has many Books
                .WithOne(b => b.Category) // Book belongs to one Category
                .HasForeignKey(b => b.CategoryId) // Define foreign key
                .IsRequired(false); // CategoryId is Nullable in Book

        }
    }
}
