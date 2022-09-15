using BlazorEksiSozluk.Api.Domain.Models;
using BlazorEksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorEksiSozluk.Infrastructure.Persistence.EntityConfigurations.EntryEntity
{
    public class EntryVoteEntityConfiguration : BaseEntityConfiguration<EntryVote>
    {
        public override void Configure(EntityTypeBuilder<EntryVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("EntryVote", BlazorEksiSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.Entry)
                .WithMany(x => x.EntryVotes)
                .HasForeignKey(x => x.EntryId);
        }
    }
}
