// Copyright (c) 2023, Brian Parker. All Rights Reserved.
// TenantDbContext.cs licensed under the MIT License.

using Microsoft.EntityFrameworkCore;
using MultiTenantDbContext.Models;

namespace MultiTenantDbContext.Data
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
            : base(options)
        {
        }

        public DbSet<SomeData> SomeItems { get; set; }
    }
}
