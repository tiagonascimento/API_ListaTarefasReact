
using ListaTarefas.Infra.repository.interfaces;
using ListTarefas.Communication.response;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ListaTarefas.Infra.repository
{
    public class TaskRepository: ITaskRepository
    {
        private readonly TarefasDbContext _dbContext;

        public TaskRepository(TarefasDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddTask(Domain.UserTask userTask)
        {
            await _dbContext.Tasks.AddAsync(userTask);         
        }
        public async Task<Domain.UserTask> GetTask(Guid IdTask)
        {           
            return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == IdTask);    
        }
        public async Task<List<Domain.UserTask>> GetAllTask()
        {
            return await _dbContext.Tasks.ToListAsync();
        }
        public void UpdateTask(Domain.UserTask updatedTask)
        {
            try
            {
                _dbContext.Tasks.Update(updatedTask);
            }
            catch (System.Exception ex)
            {

                new ResponseException(ex.Message);
            }
             
        }
        public async Task DeleteTask(Guid idTask)
        {
            var task = await _dbContext.Tasks.FindAsync(idTask);
            if (task == null) return;

            _dbContext.Tasks.Remove(task);
        }

    }
}
