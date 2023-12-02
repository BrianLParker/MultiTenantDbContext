// Copyright (c) 2023, Brian Parker. All Rights Reserved.
// TenantClaimTransformation.cs licensed under the MIT License.

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using MultiTenantDbContext.Data;

namespace MultiTenantDbContext
{
    public class TenantClaimTransformation(UserManager<ApplicationUser> users) : IClaimsTransformation
    {
        private readonly UserManager<ApplicationUser> users = users;

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var userId = users.GetUserId(principal)!;
            var user = await users.FindByIdAsync(userId);
           // if (user == null) { return null; }
            var tenantId = user!.TenantId;
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            var claimType = "tenant";
            if (!principal.HasClaim(claim => claim.Type == claimType))
            {
                claimsIdentity.AddClaim(new Claim(claimType, tenantId!));
            }

            principal.AddIdentity(claimsIdentity);
            return principal;
        }
    }
}
