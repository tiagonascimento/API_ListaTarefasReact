using Domain.Entities;
using ListaTarefas.Domain;
using Microsoft.EntityFrameworkCore;
using ListaTarefas.Domain.enums;


namespace ListaTarefas.Infra.repository
{
    public class TarefasDbContext : DbContext
    {
        public TarefasDbContext(DbContextOptions options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum < Domain.enums.TaskStatus > ("task_status");
            modelBuilder.HasPostgresEnum<Domain.enums.TaskStatus>();      
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskConfiguration).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
