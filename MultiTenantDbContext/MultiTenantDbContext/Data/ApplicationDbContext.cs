// Copyright (c) 2023, Brian Parker. All Rights Reserved.
// ApplicationDbContext.cs licensed under the MIT License.

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MultiTenantDbContext.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
    }
}
