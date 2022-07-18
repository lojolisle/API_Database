using Microsoft.EntityFrameworkCore;

namespace ApiDatabaseProject2.Data
{
    public class DemoDbContext :DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {

        }

        public DbSet<tblUser> tblUsers { get; set; }
    }
}
