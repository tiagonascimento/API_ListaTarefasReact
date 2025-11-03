
using ListaTarefas.Infra.repository.interfaces;

namespace ListaTarefas.Infra.repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TarefasDbContext _dbcontex;
        public UnitOfWork(TarefasDbContext dbContex) => _dbcontex = dbContex;

        public async Task Commit() => await _dbcontex.SaveChangesAsync();
    }
}
