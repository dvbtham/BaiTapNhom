namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class itforumEntities : DbContext
    {
        public itforumEntities()
            : base("name=itforumEntities")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostTag> PostTags { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<SocialNetwork> SocialNetworks { get; set; }
        public virtual DbSet<SocNetType> SocNetTypes { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<SourceCode> SourceCodes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryName)
                .IsFixedLength();

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Posts)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Picture>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<SourceCode>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Video>()
                .Property(e => e.Link)
                .IsUnicode(false);
        }
    }
}
