using BlazorEksiSozluk.Api.Domain.Models;
using BlazorEksiSozluk.Common.Infrastructure;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEksiSozluk.Infrastructure.Persistence.Context
{
    internal class SeedData
    {
        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(configuration["BlazorEksiSozlukConnectionString"]);

            var context = new BlazorEksiSozlukContext(dbContextBuilder.Options);

            var users = new Faker<User>("tr")
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.CreateDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.UserName, x => x.Internet.UserName())
                .RuleFor(x => x.EmailAddress, x => x.Internet.Email(x.Person.FirstName, x.Person.LastName))
                .RuleFor(x => x.Password, x => PasswordEncryptor.Encrypt(x.Internet.Password()))
                .RuleFor(x => x.EmailComfirmed, x => x.PickRandom(true, false))
                .Generate(500);

            var userIds = users.Select(x => x.Id);

            await context.AddRangeAsync(users);

            var entryGuids = Enumerable.Range(0, 200).Select(x => Guid.NewGuid()).ToList();

            var counter = 0;

            var entries = new Faker<Entry>("tr")
                .RuleFor(x => x.Id, x => entryGuids[counter++])
                .RuleFor(x => x.CreateDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.Subject, x => x.Lorem.Sentence(5, 5))
                .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
                .RuleFor(x => x.UserId, x => x.PickRandom(userIds))
                .Generate(200);

            await context.AddRangeAsync(entries);

            var comments = new Faker<EntryComment>("tr")
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.CreateDate, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
                .RuleFor(x => x.UserId, x => x.PickRandom(userIds))
                .RuleFor(x => x.EntryId, x => x.PickRandom(entryGuids))
                .Generate(1000);

            await context.AddRangeAsync(comments);

            await context.SaveChangesAsync();
        }
    }
}
