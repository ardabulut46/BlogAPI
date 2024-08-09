using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MemberComment> MemberComments { get; set; }
        public DbSet<MemberEntry> MemberEntries { get; set; }
        public DbSet<UserLikeDislike> UserLikeDislikes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Bookmark>()
                 .HasOne(b => b.Member)
                 .WithMany()
                 .HasForeignKey(b => b.MemberId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Bookmark>()
                 .HasOne(b => b.Entry)
                 .WithMany()
                 .HasForeignKey(b => b.EntryId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
                 .HasOne(b => b.Entry)
                 .WithMany()
                 .HasForeignKey(b => b.EntryId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(c => c.Member)
                .WithMany()
                .HasForeignKey(c => c.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(c => c.Entry)
                .WithMany()
                .HasForeignKey(c => c.EntryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(c => c.Parent)
                .WithMany()
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Entry>()
                .HasOne(c => c.Member)
                .WithMany()
                .HasForeignKey(e => e.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MemberComment>()
                .HasOne(c => c.AppUser)
                .WithMany()
                .HasForeignKey(c => c.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MemberComment>()
                .HasOne(c => c.Comment)
                .WithMany()
                .HasForeignKey(c => c.CommentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MemberEntry>()
               .HasOne(c => c.Member)
               .WithMany()
               .HasForeignKey(c => c.MemberId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MemberEntry>()
                .HasOne(c => c.Entry)
                .WithMany()
                .HasForeignKey(c => c.EntryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserLikeDislike>()
               .HasOne(c => c.AppUser)
               .WithMany()
               .HasForeignKey(c => c.MemberId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserLikeDislike>()
                .HasOne(c => c.Entry)
                .WithMany()
                .HasForeignKey(c => c.EntryId)
                .OnDelete(DeleteBehavior.Restrict);









        }
    }
}
