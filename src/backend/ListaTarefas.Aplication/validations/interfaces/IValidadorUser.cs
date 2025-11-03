
using ListTarefas.Communication.request;

namespace ListaTarefas.Aplication.validations.interfaces
{
    public interface IValidadorUser
    {
        public void ThrowValidador(RequestUser obj);
        public List<string> ValidateReturnError(RequestUser objeto);
        public bool IsValid(RequestUser objeto);
    }
}
