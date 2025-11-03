using ListaTarefas.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ListaTarefas.Infra.repository
{
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.UserTask>
    {
        public void Configure(EntityTypeBuilder<Domain.UserTask> builder)
        {

            builder.ToTable("tasks");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                 .HasColumnName("id")
                 .IsRequired();

            builder.Property(u => u.Title)
                   .HasColumnName("title")
                   .IsRequired();

            builder.Property(u => u.Description)
                   .HasColumnName("description")
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(t => t.Status)
                          .HasColumnName("status")
                          .IsRequired();

            builder.Property(t => t.CreatedAt)
                .HasColumnName("created_at")
                .HasConversion(
                    v => v, // para o banco
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // ao ler do banco
                );


        }
    }

}


