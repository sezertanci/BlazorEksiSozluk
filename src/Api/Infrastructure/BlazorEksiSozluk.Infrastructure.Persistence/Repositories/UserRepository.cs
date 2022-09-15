using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Api.Domain.Models;
using BlazorEksiSozluk.Infrastructure.Persistence.Context;

namespace BlazorEksiSozluk.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BlazorEksiSozlukContext dbContext) : base(dbContext)
        {
        }
    }
}
