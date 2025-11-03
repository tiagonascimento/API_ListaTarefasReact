using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ListaTarefas.Infra.repository
{
    public class UserConfiguration : IEntityTypeConfiguration<User>

    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("login");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(u => u.Password)
                   .HasColumnName("senha")
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.Email)
                   .HasColumnName("email")
                   .IsRequired()
                   .HasMaxLength(255);          
        }
    }
}
