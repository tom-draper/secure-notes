using Microsoft.EntityFrameworkCore;

namespace SecureNotes.Helpers.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<NoteRecord> Notes { get; set; }
    }

    public class NoteRecord
    {
        public int Id { get; set; }
        public string NoteID { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Content { get; set; }
    }
}
