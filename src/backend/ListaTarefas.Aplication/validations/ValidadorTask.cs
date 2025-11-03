using FluentValidation;
using ListaTarefas.Aplication.validations.interfaces;
using ListaTarefas.Exception;
using ListaTarefas.Utils.validations;
using ListTarefas.Communication.request;

namespace ListaTarefas.Aplication.validations
{
    public class ValidadorTask : AbstractValidator<CreateRequestTaskJson>, IValidadorTask
    {
        public ValidadorTask()
        {
            RuleFor(post => post.Title).validatorIsNullOrEmpt(ErrorMessage.TITLE_EMPTY);
            RuleFor(post => post.Title).validatorSizeMax(50,ErrorMessage.TITLE_MAX);
            RuleFor(post => post.Title).validatorSizeMin(3, ErrorMessage.TITLE_MIN);
            RuleFor(post => post.Description).validatorIsNullOrEmpt(ErrorMessage.DESCRIPTION_EMPTY);
            RuleFor(post => post.Description).validatorSizeMax(250, ErrorMessage.DESCRIPTION_MAX);
            RuleFor(post => post.Description).validatorSizeMin(3, ErrorMessage.DESCRIPTION_MIN);
         }

        public void ThrowValidador(CreateRequestTaskJson obj)
        {
            var resultado = Validate(obj);

            if (!resultado.IsValid)
            {
                var erros = resultado.Errors.Select(e => e.ErrorMessage).ToList();
                throw new TasksListExceptionValidate(erros);
            }
        }

        // Método para verificar retornar o erro
        public List<string> ValidateReturnError(CreateRequestTaskJson objeto)
        {
            var resultado = Validate(objeto);
            return resultado.Errors.Select(e => e.ErrorMessage).ToList();
        }

        // Método para verificar se é válido
        public bool IsValid(CreateRequestTaskJson objeto)
        {
            return Validate(objeto).IsValid;
        }
    }
}
