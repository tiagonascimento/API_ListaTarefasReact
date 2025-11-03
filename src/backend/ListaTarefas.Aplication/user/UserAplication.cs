using Domain.Entities;
using ListaTarefas.Aplication.service.mapster;
using ListaTarefas.Aplication.user.interfaces;
using ListaTarefas.Infra.repository;
using ListaTarefas.Infra.repository.interfaces;
using ListaTarefas.Utils.cryptography;
using ListTarefas.Communication.request;
using ListTarefas.Communication.response;
using Mapster;
using MapsterMapper;

namespace ListaTarefas.Aplication.user
{
    public class UserAplication : IUserAplication
    {
        private readonly IMapper _mapper;
        private readonly ICrypto _crypto;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserAplication(IMapper mapper, ICrypto crypto, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            MapsterConfig.Configuration();
            _crypto = crypto;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;

        }
        public Task<ResponseUser> CreateUser(RequestUser usuario)
        {
            var user = usuario.Adapt<User>();
            user.SetPassword(_crypto.EncryptionString(usuario.Password));
            var teste = user;

           // await _userRepositorioWrite.AddUser(user);
           // await _unitOfWork.Commit();
        //    return true;

            throw new NotImplementedException();
        }

        public async Task<User> Login(RequestUser usuario)
        {
            var user = usuario.Adapt<User>();
            return await _userRepository.GetUser(user);           
        }
    }
}
