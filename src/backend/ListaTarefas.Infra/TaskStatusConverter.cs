

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ListaTarefas.Infra
{
    public class TaskStatusConverter : ValueConverter<ListaTarefas.Domain.enums.TaskStatus, string>
    {
        public TaskStatusConverter() : base(
           v => v.ToString(),
           v => (ListaTarefas.Domain.enums.TaskStatus)Enum.Parse(typeof(ListaTarefas.Domain.enums.TaskStatus), v))
        { }
    }
}
