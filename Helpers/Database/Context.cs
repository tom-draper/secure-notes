using Microsoft.EntityFrameworkCore;

namespace SecureNotes.Helpers.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<NoteRecord> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Remove quotes by setting lowercase convention for table and column names
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Set table name to lowercase
                var tableName = entity.GetTableName();
                if (tableName != null)
                {
                    entity.SetTableName(tableName.ToLower());
                }

                // Set column names to lowercase
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }

                // Set key names to lowercase
                foreach (var key in entity.GetKeys())
                {
                    var keyName = key.GetName();
                    if (keyName != null)
                    {
                        key.SetName(keyName.ToLower());
                    }
                }

                // Set foreign keys to lowercase
                foreach (var foreignKey in entity.GetForeignKeys())
                {
                    var constraintName = foreignKey.GetConstraintName();
                    if (constraintName != null)
                    {
                        foreignKey.SetConstraintName(constraintName.ToLower());
                    }
                }

                // Set indexes to lowercase
                foreach (var index in entity.GetIndexes())
                {
                    var databaseName = index.GetDatabaseName();
                    if (databaseName != null)
                    {
                        index.SetDatabaseName(databaseName.ToLower());
                    }
                }
            }
        }
    }

    public class NoteRecord
    {
        public int Id { get; set; }
        public required string NoteID { get; set; }
        public DateTime? Timestamp { get; set; }
        public required string Content { get; set; }
    }
}
