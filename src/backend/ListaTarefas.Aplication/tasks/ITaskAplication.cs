using ListTarefas.Communication.request;
using ListTarefas.Communication.response;



namespace ListaTarefas.Aplication.tasks
{
    public interface ITaskAplication
    {

       public Task<bool> CreateTask(CreateRequestTaskJson taskJson);
        public Task<List<ResponseTask>> GetAllTask();

        public Task<Domain.UserTask> GetTask(Guid IdTask);     

        public Task UpdateTask(Guid IdTask);


        public Task DeleteTask(Guid idTask);

    }
}
