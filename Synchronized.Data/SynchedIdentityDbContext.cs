﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Synchronized.Domain;

namespace Synchronized.Data
{
    public class SynchedIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public SynchedIdentityDbContext(string connStr) : base()
        {
            _connectionString = connStr;
        }

        public SynchedIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        //public SynchronizedDbContext() : base() { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<VotedPost> CommentedPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionTag> QusetionTags { get; set; }
        public DbSet<QuestionView> QuestionViews { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public string _connectionString { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_connectionString);
            optionsBuilder.UseNpgsql(_connectionString);
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Tag>()
                .Property(t => t.Id)
                .ValueGeneratedNever();

            builder.Entity<Post>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Post>()
                .Property(p => p.DatePosted)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NOW()");

            builder.Entity<Question>().HasBaseType<VotedPost>();

            builder.Entity<Answer>().HasBaseType<VotedPost>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);

            builder.Entity<Comment>().HasBaseType<Post>()
                .HasOne(c => c.VotedPost)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.PostId);

            builder.Entity<ApplicationUser>()
                .Property(u => u.JoiningDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NOW()");

            // Many to many relationship between users and questio views
            builder.Entity<QuestionView>().HasKey(s => new { s.QuestionId, s.UserId });

            builder.Entity<QuestionView>()
                .HasOne(s => s.Question)
                .WithMany(q => q.QuestionViews)
                .HasForeignKey(s => s.QuestionId);

            builder.Entity<QuestionView>()
                .HasOne(s => s.User)
                .WithMany(u => u.QuestionViews)
                .HasForeignKey(s => s.UserId);

            // Many to many relationship between users and flags
            builder.Entity<PostFlag>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<PostFlag>()
                .HasOne(s => s.Post)
                .WithMany(q => q.PostFlags)
                .HasForeignKey(s => s.PostId);

            builder.Entity<PostFlag>()
                .HasOne(s => s.User)
                .WithMany(u => u.Flags)
                .HasForeignKey(s => s.UserId);

            // Many to many relationship between users and delete votes
            builder.Entity<DeleteVote>().HasKey(s => new { s.PostId, s.UserId });

            builder.Entity<DeleteVote>()
                .HasOne(s => s.Post)
                .WithMany(q => q.DeleteVotes)
                .HasForeignKey(s => s.PostId);

            builder.Entity<DeleteVote>()
                .HasOne(s => s.User)
                .WithMany(u => u.DeleteVotes)
                .HasForeignKey(s => s.UserId);

            // Many to many relationship between users and tags
            builder.Entity<QuestionTag>().HasKey(s => new { s.QuestionId, s.TagId });

            builder.Entity<QuestionTag>()
                .HasOne(qt => qt.Question)
                .WithMany(q => q.QuestionTags)
                .HasForeignKey(qt => qt.QuestionId);

            builder.Entity<QuestionTag>()
                .HasOne(qt => qt.Tag)
                .WithMany(t => t.QuestionTags)
                .HasForeignKey(qt => qt.TagId);

            // Many to many relationship between users and questions
            builder.Entity<Subscription>().HasKey(s => new { s.UserId, s.QuestionId });

            builder.Entity<Subscription>()
                .HasOne(s => s.Subscriber)
                .WithMany(s => s.Subscriptions)
                .HasForeignKey(s => s.UserId);

            builder.Entity<Subscription>().
                HasOne(s => s.Question).
                WithMany(s => s.Subscriptions).
                HasForeignKey(s => s.QuestionId);

            // Many to many relationship between users and votes
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

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
