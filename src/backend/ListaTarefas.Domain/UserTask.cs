
using ListaTarefas.Exception;
using ListaTarefas.Utils.validations;

namespace ListaTarefas.Domain

{
    public class UserTask: BaseEntity
    {
        // Private fields
        private string _title;
        private string _description;




        // Construtor com parâmetros
        public UserTask(string title, string description) 
        {
            _title = title;
            _description = description;
            CreatedAt = DateTime.UtcNow;
            Status = ListaTarefas.Domain.enums.TaskStatus.Pending;
            
        }
        // Construtor entity
        public UserTask(){}


        // Propriedades
       
        public string Title => _title;
        public string Description => _description;
        public ListaTarefas.Domain.enums.TaskStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }   
        public void SetTitle(string title)
        {
            title.ValidateNotEmpty(ErrorMessage.TITLE_EMPTY);
            _title = title.Trim();
        }
        public void SetDescription(string description)
        {
            description.ValidateNotEmpty(ErrorMessage.DESCRIPTION_EMPTY);
            description.ValidateMaxLength(50,ErrorMessage.DESCRIPTION_MAX);
            _description = description.Trim();
        }

        // Métodos de domínio
        public void MarkAsDone() => Status = ListaTarefas.Domain.enums.TaskStatus.Done;
        public void MarkAsPending() => Status = ListaTarefas.Domain.enums.TaskStatus.Pending;
        public void ToggleStatus() => Status = Status == enums.TaskStatus.Done ? enums.TaskStatus.Pending : enums.TaskStatus.Done;

        public TimeSpan TimeSinceCreation => DateTime.UtcNow - CreatedAt;       
    }
}
