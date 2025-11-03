
using Domain.Entities;
using ListTarefas.Communication.request;
using ListTarefas.Communication.response;

namespace ListaTarefas.Aplication.user.interfaces
{
    public interface IUserAplication
    {
        public Task<ResponseUser> CreateUser(RequestUser usuario);
        public Task<User> Login(RequestUser usuario);
    }
}
