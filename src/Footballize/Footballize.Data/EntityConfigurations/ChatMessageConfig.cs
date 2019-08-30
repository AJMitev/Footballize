namespace Footballize.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ChatMessageConfig : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text)
                .IsRequired()
                .IsUnicode();

            builder.Property(x => x.Sender)
                .IsRequired()
                .IsUnicode();

            builder.HasOne(x => x.Gather)
                .WithMany(x => x.ChatMessages)
                .HasForeignKey(x => x.GatherId);
        }
    }
}