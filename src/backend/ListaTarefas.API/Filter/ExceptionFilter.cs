using ListaTarefas.Exception;
using ListTarefas.Communication.response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ListaTarefas.API.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is TasksListExceptionBase)
                TaskException(context);
            else
                unknownException(context);
        }

        private void TaskException(ExceptionContext context)
        {
            if (context.Exception is TasksListExceptionValidate)
            {
                var ex = context.Exception as TasksListExceptionValidate;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseException(ex.ErrorMessage));
            }
        }

        private void unknownException(ExceptionContext context)
        {
            //add log para erro na aplicacao

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseException(context.Exception.InnerException.Message));// ErrorMessage.UNKNOWN_ERROR));
        }
    }
}
