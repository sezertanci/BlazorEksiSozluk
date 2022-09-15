using BlazorEksiSozluk.Api.Domain.Models;
using BlazorEksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorEksiSozluk.Infrastructure.Persistence.EntityConfigurations.EntryCommentEntity
{
    public class EntryCommentEntityConfiguration : BaseEntityConfiguration<EntryComment>
    {
        public override void Configure(EntityTypeBuilder<EntryComment> builder)
        {
            base.Configure(builder);

            builder.ToTable("EntryComment", BlazorEksiSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.Entry)
                .WithMany(x => x.EntryComments)
                .HasForeignKey(x => x.EntryId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.EntryComments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
