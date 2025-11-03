

using Microsoft.EntityFrameworkCore;

namespace ListaTarefas.Infra.repository.interfaces
{
    public interface ITaskRepository
    {
        public  Task AddTask(Domain.UserTask userTask);

        public  Task<Domain.UserTask> GetTask(Guid IdTask);

        public  Task<List<Domain.UserTask>> GetAllTask();

        public void  UpdateTask(Domain.UserTask updatedTask);


        public  Task DeleteTask(Guid idTask);



    }
}
