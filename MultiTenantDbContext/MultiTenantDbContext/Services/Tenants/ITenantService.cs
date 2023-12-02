// Copyright (c) 2023, Brian Parker. All Rights Reserved.
// ITenantService.cs licensed under the MIT License.

namespace MultiTenantDbContext.Services.Tenants
{
    public interface ITenantService
    {
        string GetTenantConnectionString(string tenant);
        ValueTask<string?> GetTenantNameAsync();
    }

}
