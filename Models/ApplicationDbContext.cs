using Microsoft.EntityFrameworkCore;

namespace TextEditorMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TextEditorModel> TestEditor { get; set; }
    }
}
