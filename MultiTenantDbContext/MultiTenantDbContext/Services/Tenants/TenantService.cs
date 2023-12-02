// Copyright (c) 2023, Brian Parker. All Rights Reserved.
// TenantService.cs licensed under the MIT License.

using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using MultiTenantDbContext.Models;

namespace MultiTenantDbContext.Services.Tenants
{
    internal class TenantService(AuthenticationStateProvider authenticationStateProvider, LocalConfiguration localConfiguration) : ITenantService
    {
        private readonly AuthenticationStateProvider authenticationStateProvider = authenticationStateProvider;
        private readonly LocalConfiguration localConfiguration = localConfiguration;

        public async ValueTask<string?> GetTenantNameAsync()
        {
            AuthenticationState authenticationState = await this.authenticationStateProvider.GetAuthenticationStateAsync();
            if (authenticationState == null) throw new InvalidOperationException();
            if (!(authenticationState.User?.Identity?.IsAuthenticated ?? false)) throw new InvalidOperationException();
            ClaimsPrincipal claimsPrincipal = authenticationState.User;
            return claimsPrincipal.FindFirstValue(claimType: "tenant");
        }

        public string GetTenantConnectionString(string tenant) =>
            string.Format(this.localConfiguration.ConnectionStrings[key: "TenantConnection"], tenant);
    }
}
