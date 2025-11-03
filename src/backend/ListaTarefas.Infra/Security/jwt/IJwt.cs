using Domain.Entities;

namespace BankMore.Application.service.jwt
{
    public interface IJwt
    {
        public string GenerateToken(User user);

        public string GenerateToken(string userId, string email, IEnumerable<string> roles);


    }
}
