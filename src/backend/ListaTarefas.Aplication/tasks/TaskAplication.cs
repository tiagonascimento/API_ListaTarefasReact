using Domain.Entities;
using ListaTarefas.Aplication.service.mapster;
using ListaTarefas.Domain;
using ListaTarefas.Infra.repository.interfaces;
using ListaTarefas.Utils.cryptography;
using ListTarefas.Communication.request;
using ListTarefas.Communication.response;
using Mapster;
using MapsterMapper;
using System.Threading.Tasks;

namespace ListaTarefas.Aplication.tasks
{
    public class TaskAplication: ITaskAplication
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskAplication(IMapper mapper, ICrypto crypto, ITaskRepository taskRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            MapsterConfig.Configuration();
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;

        }
        public  async Task<bool> CreateTask(CreateRequestTaskJson taskJson)
        {
  
            var taskUser = taskJson.Adapt<UserTask>();
            await _taskRepository.AddTask(taskUser);
            await _unitOfWork.Commit();
            return true; 
        }
        public async Task<List<ResponseTask>> GetAllTask()
        {                     
            var tasks =  await _taskRepository.GetAllTask();
            var taskUser = tasks.Adapt<List<ResponseTask>>();
            return taskUser;
        }
        public async Task<List<ResponseTask>> AlterTask()
        {
            var tasks = await _taskRepository.GetAllTask();
            var taskUser = tasks.Adapt<List<ResponseTask>>();
            return taskUser;
        }
        public async Task<List<ResponseTask>> DeleteTask()
        {
            var tasks = await _taskRepository.GetAllTask();
            var taskUser = tasks.Adapt<List<ResponseTask>>();
            return taskUser;
        }

        public async Task<UserTask> GetTask(Guid IdTask)
        {
            return await _taskRepository.GetTask(IdTask);           
        }

        public async Task UpdateTask(Guid IdTask)
        {
            UserTask task = await GetTask(IdTask);
            if (task == null) return;


            task.ToggleStatus();
             _taskRepository.UpdateTask(task);
            await _unitOfWork.Commit();

            //  return updatedTask;

        }

        public async Task DeleteTask(Guid idTask)
        {
            await _taskRepository.DeleteTask(idTask);
            await _unitOfWork.Commit();
            //   return taskUser;
        }
    }
}
