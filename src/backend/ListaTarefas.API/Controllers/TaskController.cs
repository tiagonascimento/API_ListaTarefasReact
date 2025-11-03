using ListaTarefas.Aplication.tasks;
using ListaTarefas.Aplication.validations.interfaces;
using ListTarefas.Communication.request;
using ListTarefas.Communication.response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ListaTarefas.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet()]
        [Authorize]
        [ProducesResponseType(typeof(ResponseTask), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseTask), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Tasks([FromServices] ITaskAplication taskAplicaion)
        {
            var tasks = await taskAplicaion.GetAllTask();
            return Ok(tasks);
        }
        [HttpPost()]
        [Authorize]
        [ProducesResponseType(typeof(ResponseTask), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseTask), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarTasks([FromServices] IValidadorTask validador, [FromServices] ITaskAplication taskAplicaion, CreateRequestTaskJson requestTaskJson)
        {
            validador.ThrowValidador(requestTaskJson);
            await taskAplicaion.CreateTask(requestTaskJson);
            return Ok();
        }
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseTask), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseTask), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarTasks(Guid id, [FromServices] ITaskAplication taskAplicaion)
        {
            await taskAplicaion.UpdateTask(id);
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseTask), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseTask), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletarTasks(Guid id,[FromServices] ITaskAplication taskAplicaion)
        {
            await taskAplicaion.DeleteTask(id);
            return Ok();
        }
    }
}
