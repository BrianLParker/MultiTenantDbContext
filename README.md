# MultiTenantDbContext

How I generated the Migrations and Optimizations
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


This will create ApplicationDbContext for all users. 
```
Update-Database -Context ApplicationDbContext
```

There is no point doing this for tenant database as it has a dynamic connection string.
When the user navigates to the Tenant Data the service will create the Tenant Database if not initialized.

`appsettings.json`
```js
  "Tenants": [
    "TenantA",
    "TenantB",
    "TenantC"
  ],

```
These strings appear on Register screen and become a claim for the user.

The selected string will also become part of the connection string for the database.

Adding more strings here will add more tenants.