using ChatApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.ContextDB
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<ChatMessage> messages => Set<ChatMessage>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Text)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(x => x.Sentiment)
                    .HasMaxLength(50);

                entity.Property(x => x.CreatedAtUtc)
                    .IsRequired();
            });
        }
    }
}
