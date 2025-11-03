
using Domain.Entities;
using ListaTarefas.Domain;
using ListTarefas.Communication.request;
using ListTarefas.Communication.response;
using Mapster;

namespace ListaTarefas.Aplication.service.mapster
{
    static public class MapsterConfig
    {
        public static void Configuration()
        {
            // Configuração para RequestUser -> Usuario
            TypeAdapterConfig<RequestUser, User>
                .NewConfig()
                .MapWith(dto => new User(dto.Email, dto.Password));

            // Configuração para Usuario -> RequestUser
            TypeAdapterConfig<User, RequestUser>
                .NewConfig()
                .Map(dest => dest.Email, src => src.Email);               


            TypeAdapterConfig<CreateRequestTaskJson, UserTask>
                .NewConfig()
                .MapWith(dto => new UserTask(dto.Title, dto.Description));

            // Configuração para Usuario -> RequestUser
            TypeAdapterConfig<UserTask, ResponseTask>
                .NewConfig()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.title, src => src.Title)
                .Map(dest => dest.description, src => src.Description);
        }
    }
}
