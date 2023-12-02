using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MultiTenantDbContext.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
    }

    public class TenantDbContext(DbContextOptions<TenantDbContext> options) : DbContext
    {

    }

    public class SomeData
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(length: 450)]
        public required string Name { get; set; }
    }
}
