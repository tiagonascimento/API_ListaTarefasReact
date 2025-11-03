using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ListaTarefas.Infra.repository.interfaces
{
    public interface IUserRepository
    {
        public Task AddUser(User user);

        public Task<User> GetUser(User user);
   
    }
}
