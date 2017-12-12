using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Amenity> AmenityItems { get; set; }
        public DbSet<AmenityGroup> AmenityGroupItems { get; set; }

    }
}