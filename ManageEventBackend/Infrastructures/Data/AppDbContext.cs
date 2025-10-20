using ManageEventBackend.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageEventBackend.Infrastructures.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventMember> EventMembers { get; set; }
        public DbSet<EventProcess> EventProcesses { get; set; }
        public DbSet<EventGift> EventGifts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Author)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Event>()
                .HasMany(e => e.EventMembers)
                .WithOne(em => em.Event)
                .HasForeignKey(em => em.EventId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Event>()
                .HasMany(e => e.EventProcesses)
                .WithOne(ep => ep.Event)
                .HasForeignKey(ep => ep.EventId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Event>()
                .HasMany(e => e.EventGifts)
                .WithOne(eg => eg.Event)
                .HasForeignKey(eg => eg.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventMember>().ToTable("EventMembers");
            modelBuilder.Entity<EventProcess>().ToTable("EventProcesses");
            modelBuilder.Entity<EventGift>().ToTable("EventGifts");
        }
    }
}
