using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Naobiz.Data
{
    public class AppDbContext : DbContext
    {
        readonly IWebHostEnvironment m_Environment;

        public AppDbContext(IWebHostEnvironment environment, DbContextOptions options)
            : base(options)
        {
            m_Environment = environment;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(entity => entity.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(entity => entity.TaxId);
            modelBuilder.Entity<User>()
                .HasIndex(entity => entity.Name);
            modelBuilder.Entity<User>()
                .HasIndex(entity => entity.Phone);
            modelBuilder.Entity<User>()
                .HasIndex(entity => entity.Address);
            modelBuilder.Entity<User>()
                .HasIndex(entity => entity.Country);
            modelBuilder.Entity<User>()
                .HasIndex(entity => entity.Province);
            modelBuilder.Entity<User>()
                .HasIndex(entity => entity.City);
            modelBuilder.Entity<User>()
                .HasIndex(entity => entity.ZipCode);

            modelBuilder.Entity<UserGroup>()
                .HasIndex(entity => entity.Name)
                .IsUnique();

            modelBuilder.Entity<Activity>()
                .HasIndex(entity => entity.Name)
                .IsUnique();
            modelBuilder.Entity<Activity>()
                .HasData(LoadArray<Activity>(Path.Combine(m_Environment.WebRootPath, "activities.json")));

            modelBuilder.Entity<UserActivityLink>()
                .HasKey(entity => new { entity.UserId, entity.ActivityId });

            modelBuilder.Entity<ChatMessage>()
                .HasIndex(entity => entity.Text);
            modelBuilder.Entity<ChatMessage>()
                .HasOne(entity => entity.Creator)
                .WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ChatMessage>()
                .HasOne(entity => entity.Recipient)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ForumGroup>()
                .HasIndex(entity => entity.Name)
                .IsUnique();
            modelBuilder.Entity<ForumGroup>()
                .HasData(LoadArray<ForumGroup>(Path.Combine(m_Environment.WebRootPath, "forum-groups.json")));

            modelBuilder.Entity<ForumTopic>()
                .HasIndex(entity => entity.Title);

            modelBuilder.Entity<ForumMessage>()
                .HasIndex(entity => entity.Text);
            modelBuilder.Entity<ForumMessage>()
                .HasOne(entity => entity.Creator)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ForumAttachment>()
                .HasIndex(entity => new { entity.MessageId, entity.Name })
                .IsUnique();

            modelBuilder.Entity<ResourceType>()
                .HasIndex(entity => entity.Name)
                .IsUnique();
            modelBuilder.Entity<ResourceType>()
                .HasData(LoadArray<ResourceType>(Path.Combine(m_Environment.WebRootPath, "resource-types.json")));

            modelBuilder.Entity<Resource>()
                .HasIndex(entity => entity.Title);
            modelBuilder.Entity<Resource>()
                .HasIndex(entity => entity.Description);
            modelBuilder.Entity<Resource>()
                .HasOne(entity => entity.Owner)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ResourceAttachment>()
                .HasIndex(entity => new { entity.ResourceId, entity.Name })
                .IsUnique();
        }

        private T[] LoadArray<T>(string filePath)
        {
            var objs = JsonConvert.DeserializeObject<T[]>(File.ReadAllText(filePath));
            for (var i = 0; i < objs.Length; ++i)
            {
                dynamic obj = objs[i];
                obj.Id = i + 1;
            }
            return objs;
        }

        public DbSet<User> Users { get; set; }

        public void HashUserPasswords()
        {
            foreach (var user in Users.Where(user => user.Password != null))
            {
                user.SetPassword(user.Password);
                user.Password = null;
            }
            SaveChanges();
        }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<UserActivityLink> UserActivityLinks { get; set; }

        public IQueryable<Activity> GetActivities(User user) => UserActivityLinks.Where(link => link.User == user).Select(link => link.Activity);

        public IQueryable<User> GetUsers(Activity activity) => UserActivityLinks.Where(link => link.Activity == activity).Select(link => link.User);

        public void AddActivity(User user, Activity activity) => UserActivityLinks.Add(new UserActivityLink { User = user, Activity = activity });

        public async Task RemovevActivityAsync(User user, Activity activity) =>
            UserActivityLinks.Remove(await UserActivityLinks.SingleOrDefaultAsync(link => link.User == user && link.Activity == activity));

        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<ForumGroup> ForumGroups { get; set; }

        public IQueryable<ForumGroup> GetForumGroups(User user) => user != null ?
            user.Admin ? ForumGroups : ForumGroups.Where(group => group.UserGroup == null || group.UserGroup == user.Group) :
            ForumGroups.Where(group => group.UserGroup == null);

        public DbSet<ForumTopic> ForumTopics { get; set; }

        public DbSet<ForumMessage> ForumMessages { get; set; }

        public DbSet<ForumAttachment> ForumAttachments { get; set; }

        public DbSet<ResourceType> ResourceTypes { get; set; }

        public IQueryable<ResourceType> GetResourceTypes(User user) => user.Admin ? ResourceTypes : ResourceTypes.Where(type => type.UserGroup == null || type.UserGroup == user.Group);

        public DbSet<Resource> Resources { get; set; }

        public DbSet<ResourceAttachment> ResourceAttachments { get; set; }

        public DbSet<Blob> Blobs { get; set; }
    }
}
