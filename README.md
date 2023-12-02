# MultiTenantDbContext

`ApplicationDbContext`
```
Add-Migration -Context ApplicationDbContext CreateInitialIdentitySchema -OutputDir Data/Migrations/IdentityContext
Optimize-DbContext -Context ApplicationDbContext -OutputDir Data/Optimizations/IdentityContext
```

`TenantDbContext` [Per Tenant]
```
Add-Migration -Context TenantDbContext CreateInitialTenantSchema -OutputDir Data/Migrations/TenantContext
Optimize-DbContext -Context TenantDbContext -OutputDir Data/Optimizations/TenantContext
```

```
Update-Database -Context ApplicationDbContext
```

There is no point doing this for tenant database as it has a dynamic connection string.

`appsettings.json` Will appear on Register screen and become a claim for the user.
```js
  "Tenants": [
    "TenantA",
    "TenantB",
    "TenantC"
  ],

```