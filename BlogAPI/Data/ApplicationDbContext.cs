using BlogAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BlogAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Member>? Members {  get; set; }
        public DbSet<Entry>? Entries { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<MemberEntry>? MemberEntries { get; set; }
        public DbSet<MemberComment>? MemberComments { get; set; }
        public DbSet<UserLikeDislike>? UserLikeDislikes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MemberEntry>().HasKey(me => new { me.EntryId, me.MemberId });
            builder.Entity<MemberComment>().HasKey(mc => new { mc.MemberId,mc.CommentId});
            builder.Entity<UserLikeDislike>().HasKey(mc => new { mc.MemberId, mc.EntryId });

            // Composite key for Bookmark entity
            builder.Entity<Bookmark>()
                .HasKey(b => new { b.MemberId, b.EntryId });
            builder.Entity<Bookmark>()
        .HasOne(b => b.Member)
        .WithMany() // Assuming no navigation property on AppUser for Bookmarks
        .HasForeignKey(b => b.MemberId)
        .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            builder.Entity<Bookmark>()
                .HasOne(b => b.Entry)
                .WithMany() // Assuming no navigation property on Entry for Bookmarks
                .HasForeignKey(b => b.EntryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Comment entity relationships
            builder.Entity<Comment>()
                .HasKey(c => c.Id); // Single primary key for Comment

           
            builder.Entity<Comment>()
                .HasOne(c => c.Entry)
                .WithMany(e => e.Comments)
                .HasForeignKey(c => c.EntryId)
                .OnDelete(DeleteBehavior.Cascade); // Configure delete behavior as needed

            // Configure the relationship between Comment and Member
            builder.Entity<Comment>()
                .HasOne(c => c.Member)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MemberId)
                .OnDelete(DeleteBehavior.Restrict); // Configure delete behavior as needed
        }
        public DbSet<BlogAPI.Models.Bookmark> Bookmark { get; set; } = default!;


    }
}
