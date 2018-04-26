using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Synchronized.Model;

namespace Synchronized.Data
{
    public class SynchronizedDbContext : IdentityDbContext<ApplicationUser>
    {
        public SynchronizedDbContext(DbContextOptions options) : base(options)
        {
        }

        public SynchronizedDbContext() : base() { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionTag> QusetionTags { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb; Database = SynchronizedData; Trusted_Connection = true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Post>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Post>()
                .Property(p => p.DatePosted)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<Question>().HasBaseType<CommentedPost>();

            builder.Entity<Answer>().HasBaseType<CommentedPost>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);

            builder.Entity<Comment>().HasBaseType<Post>()
                .HasOne(c => c.DataPost)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.PostId);

            builder.Entity<ApplicationUser>()
                .Property(u => u.JoiningDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<QuestionTag>().HasKey(s => new { s.QuestionId, s.TagId });

            builder.Entity<QuestionTag>()
                .HasOne(qt => qt.Question)
                .WithMany(q => q.QuestionTags)
                .HasForeignKey(qt => qt.QuestionId);

            builder.Entity<QuestionTag>()
                .HasOne(qt => qt.Tag)
                .WithMany(t => t.QuestionTags)
                .HasForeignKey(qt => qt.TagId);

            builder.Entity<Vote>().HasKey(s => new { s.VoterId, s.PostId });

            builder.Entity<Vote>()
                .HasOne(v => v.Post)
                .WithMany(p => p.Votes)
                .HasForeignKey(v => v.PostId);

            builder.Entity<Vote>()
                .HasOne(v => v.Voter)
                .WithMany(p => p.Votes)
                .HasForeignKey(v => v.VoterId);



            base.OnModelCreating(builder);
        }
    }
}
