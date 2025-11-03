
namespace ListaTarefas.Exception
{
    public class TasksListExceptionValidate:TasksListExceptionBase
    {
        public IList<string> ErrorMessage { get; set; }
       
        public TasksListExceptionValidate(IList<string> errorMessage)
        {
            ErrorMessage = errorMessage;
        }
       


    }
}
