using ApiTpEncode.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTpEncode.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext>options) : base(options) { }
        public DbSet<Usuario> usuarios { get; set; }
    }
}
