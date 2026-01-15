using cctvtemplate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cctvtemplate.Configuration
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(b => b.Description).IsRequired().HasMaxLength(256);
            builder.Property(b =>b.ImageUrl).IsRequired().HasMaxLength(2048);
            builder.Property(b => b.PostedDate).IsRequired();
            builder.HasOne(t => t.Tag).WithMany(b => b.Blogs).HasForeignKey(b => b.TagId).HasPrincipalKey(t => t.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
