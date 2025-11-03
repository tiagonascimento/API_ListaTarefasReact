
using ListaTarefas.Exception;
using ListTarefas.Communication.request;

namespace ListaTarefas.Aplication.validations.interfaces
{
    public interface IValidadorTask
    {
        public void ThrowValidador(CreateRequestTaskJson obj);    
        public List<string> ValidateReturnError(CreateRequestTaskJson objeto);
        public bool IsValid(CreateRequestTaskJson objeto);
       
    }
}
