namespace LHelper.Data
{
    using Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;


    public class LHelperDbContext : IdentityDbContext<User>
    {
        public LHelperDbContext(DbContextOptions<LHelperDbContext> options)
            : base(options)
        {
        }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Replay> Replies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<UserCategory>()
                .HasKey(uc => new { uc.CategoryId, uc.UserId });

            builder
                .Entity<UserCategory>()
                .HasOne(uc => uc.Category)
                .WithMany(c => c.Trainers)
                .HasForeignKey(uc => uc.CategoryId);

            builder
                .Entity<UserCategory>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(uc => uc.UserId);

            builder
                .Entity<Topic>()
                .HasOne(t => t.User)
                .WithMany(u => u.Topics)
                .HasForeignKey(t => t.UserId);

            builder
                .Entity<Topic>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Topics)
                .HasForeignKey(t => t.CategoryId);

            builder
                .Entity<Replay>()
                .HasOne(r => r.User)
                .WithMany(u => u.Replies)
                .HasForeignKey(r => r.UserId);

            builder
                .Entity<Replay>()
                .HasOne(r => r.Topic)
                .WithMany(t => t.Replies)
                .HasForeignKey(r => r.TopicId);


            base.OnModelCreating(builder);

        }
    }
}
