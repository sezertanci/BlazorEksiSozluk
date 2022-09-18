using BlazorEksiSozluk.Common.Events.UserEvent;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BlazorEksiSozluk.Projections.UserWorkerService.Services
{
    public class UserService
    {
        private readonly string connectionString;

        public UserService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<Guid> CreateEmailConfirmation(UserEmailChangedEvent @event)
        {
            var guid = Guid.NewGuid();

            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO EmailConfirmation (Id, OldEmailAddress, NewEmailAddress, CreateDate) VALUES(@Id, @OldEmailAddress, @NewEmailAddress, GETDATE())", new
            {
                Id = guid,
                @event.NewEmailAddress,
                @event.OldEmailAddress
            });

            return guid;
        }
    }
}
