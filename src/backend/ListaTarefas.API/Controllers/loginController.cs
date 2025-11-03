using BankMore.Application.service.jwt;
using Domain.Entities;
using ListaTarefas.Aplication.user.interfaces;
using ListaTarefas.Aplication.validations.interfaces;
using ListTarefas.Communication.request;
using ListTarefas.Communication.response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListaTarefas.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {
        [HttpPost()]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<LoginResponse> login(RequestUser userJson, [FromServices] IValidadorUser validador,[FromServices] IJwt _jwt, [FromServices] IUserAplication userAplication)
        {
            validador.ThrowValidador(userJson);
            var user = await userAplication.Login(userJson);            
            var token = _jwt.GenerateToken(user);

            
            return new LoginResponse
            {
                Success = true,
                Token = token,
                Expires = DateTime.UtcNow.AddHours(1), // Ou pegar do configuration
                user = new ResponseUser
                {
                    id = user.Id
                },
                Message = "Login realizado com sucesso"
            };
           

            
        }
      

    }
}
