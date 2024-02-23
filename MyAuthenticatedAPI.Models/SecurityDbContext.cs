
using Microsoft.EntityFrameworkCore;
using MyAuthenticatedAPI.Models;

namespace MyAuthenticatedAPI.Models
{
    public class SecurityDbContext : DbContext
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
        {
        }
    }
}