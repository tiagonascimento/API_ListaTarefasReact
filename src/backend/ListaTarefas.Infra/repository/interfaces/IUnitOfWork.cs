
using System.Linq.Expressions;

namespace ListaTarefas.Infra.repository.interfaces
{
    public interface IUnitOfWork
    {
        
        public Task Commit() ;
    }
}
