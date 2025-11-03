using ListaTarefas.Aplication.tasks;
using ListaTarefas.Aplication.user;
using ListaTarefas.Aplication.user.interfaces;
using ListaTarefas.Aplication.validations;
using ListaTarefas.Aplication.validations.interfaces;
using ListaTarefas.Utils.cryptography;
using ListaTarefas.Utils.cryptography.cryptosettings;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ListaTarefas.Aplication
{
    public static class DependecyInjectionExtension
    {
        public static void AddAplication(this IServiceCollection service)
        {
            addValidador(service);
            addSeting(service);
            adAplication(service);
        }
        private static void addValidador(IServiceCollection service)
        {
            service.AddScoped<IValidadorTask, ValidadorTask>();
            service.AddScoped<IValidadorUser, ValidadorUser>();
        }
        private static void addSeting(IServiceCollection service)
        {
            service.AddScoped<IMapper, Mapper>();
            service.AddScoped<ICrypto, Crypto>();           
        }
        private static void adAplication(IServiceCollection service)
        {
            service.AddScoped<IUserAplication, UserAplication>();
            service.AddScoped<ITaskAplication, TaskAplication>();
            
        }
    }
}
