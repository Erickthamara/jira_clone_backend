using jira_clone_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace jira_clone_backend.Data
{
    public class JiraContext(DbContextOptions<JiraContext> options) : DbContext(options)
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
    }


}
