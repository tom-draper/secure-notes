using Microsoft.EntityFrameworkCore;

namespace SecureNotes.Helpers.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
    }

    public class Note
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
    }
}
