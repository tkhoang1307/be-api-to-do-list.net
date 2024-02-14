using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Entities;

namespace ToDoList.Configuration
{
    public class UserStoryConfiguration : IEntityTypeConfiguration<UserStory>
    {
        public void Configure(EntityTypeBuilder<UserStory> builder)
        {
            builder.HasKey(us => us.Id);

            //builder.HasOne(us => us.User)
            //    .WithMany(u => u.UserStories)
            //    .HasForeignKey(us => us.CreatedBy);

            //builder.HasOne(us => us.Status)
            //    .WithMany(s => s.UserStories)
            //    .HasForeignKey(us => us.IdStatus);
        }
    }
}
