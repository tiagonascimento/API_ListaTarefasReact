using Domain.Entities;
using ListaTarefas.Infra.repository.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaTarefas.Infra.repository
{
    public class UserRepository: IUserRepository
    {

        private readonly TarefasDbContext _dbContext;

        public UserRepository(TarefasDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }
        public async Task<User> GetUser(User user)
        {
            return await _dbContext.Users.Where(c => c.Email == user.Email && c.Password == user.Password).FirstOrDefaultAsync();
        }     
    }
}
