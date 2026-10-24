using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SimpleMedia.Data
{
    public class SimpleMediaDbContext : IdentityDbContext
    {
        public SimpleMediaDbContext(DbContextOptions<SimpleMediaDbContext> options)
            : base(options)
        {
        }
    }
}
