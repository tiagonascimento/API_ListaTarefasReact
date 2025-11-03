using FluentValidation;
using ListaTarefas.Aplication.validations.interfaces;
using ListaTarefas.Exception;
using ListaTarefas.Utils.validations;
using ListTarefas.Communication.request;

namespace ListaTarefas.Aplication.validations
{
    public class ValidadorUser: AbstractValidator<RequestUser>, IValidadorUser
    {
        public ValidadorUser()
        {
            RuleFor(post => post.Name).validatorIsNullOrEmpt(ErrorMessage.USER_EMPTY);
            RuleFor(post => post.Name).validatorSizeMin(3, ErrorMessage.USER_MIN);
            RuleFor(post => post.Password).validatorSizeMin(6, ErrorMessage.PASSWORD_MIN);
            RuleFor(post => post.Password).validatorIsNullOrEmpt(ErrorMessage.Password_EMPTY);         
        }

        public void ThrowValidador(RequestUser obj)
        {
            var resultado = Validate(obj);

            if (!resultado.IsValid)
            {
                var erros = resultado.Errors.Select(e => e.ErrorMessage).ToList();
                throw new TasksListExceptionValidate(erros);
            }
        }

        // Método para verificar retornar o erro
        public List<string> ValidateReturnError(RequestUser objeto)
        {
            var resultado = Validate(objeto);
            return resultado.Errors.Select(e => e.ErrorMessage).ToList();
        }

        // Método para verificar se é válido
        public bool IsValid(RequestUser objeto)
        {
            return Validate(objeto).IsValid;
        }
    }
}
