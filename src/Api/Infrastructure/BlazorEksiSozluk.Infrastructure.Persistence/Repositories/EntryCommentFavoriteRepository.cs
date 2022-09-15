using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Api.Domain.Models;
using BlazorEksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BlazorEksiSozluk.Infrastructure.Persistence.Repositories
{
    public class EntryCommentFavoriteRepository : GenericRepository<EntryCommentFavorite>, IEntryCommentFavoriteRepository
    {
        public EntryCommentFavoriteRepository(BlazorEksiSozlukContext dbContext) : base(dbContext)
        {
        }
    }
}
