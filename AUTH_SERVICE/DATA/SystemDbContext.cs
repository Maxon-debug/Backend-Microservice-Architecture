using AUTH_SERVICE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AUTH_SERVICE.DATA
{
    public class SystemDbContext : IdentityDbContext<SystemUser>
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options) { }

        public DbSet<SystemUser> ApplicationUsers { get; set; }
    }
}
