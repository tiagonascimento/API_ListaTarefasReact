
namespace ListaTarefas.Domain
{

    public class BaseEntity
    {
        protected BaseEntity() {}
        public Guid Id { get; protected set; } = Guid.NewGuid();
    }
}

