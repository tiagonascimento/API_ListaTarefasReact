using ListaTarefas.Aplication.user.interfaces;
using ListaTarefas.Aplication.validations.interfaces;
using ListTarefas.Communication.request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListaTarefas.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost()]
        // [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> User(RequestUser userJson, [FromServices] IValidadorUser validador, [FromServices] IUserAplication user)
        {
            validador.ThrowValidador(userJson);
            user.CreateUser(userJson);
            return Ok();
        }
    }
}
