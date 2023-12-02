// Copyright (c) 2023, Brian Parker. All Rights Reserved.
// ApplicationUser.cs licensed under the MIT License.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MultiTenantDbContext.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(length: 450)]
        public string? TenantId { get; set; }
    }
}
