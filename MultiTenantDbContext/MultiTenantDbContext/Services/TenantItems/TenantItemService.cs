// Copyright (c) 2023, Brian Parker. All Rights Reserved.
// TenantItemService.cs licensed under the MIT License.

using Microsoft.EntityFrameworkCore;
using MultiTenantDbContext.Data;
using MultiTenantDbContext.Models;
using MultiTenantDbContext.Services.Tenants;

namespace MultiTenantDbContext.Services.TenantItems
{
    public class TenantItemService(
        IDbContextFactory<TenantDbContext> dbContextFactory,
        ITenantService tenantService) : ITenantItemService
    {
        private readonly IDbContextFactory<TenantDbContext> dbContextFactory = dbContextFactory;
        private readonly ITenantService tenantService = tenantService;

        public async ValueTask<SomeData> AddItemAsync(SomeData someData)
        {
            using var db = await CreateAsync();
            var result = db.SomeItems.Add(someData);
            await db.SaveChangesAsync();
            return result.Entity;
        }

        public async ValueTask<IQueryable<SomeData>> QueryItemsAsync()
        {
            var db = await CreateAsync();
            return db.SomeItems;
        }


        private async ValueTask<TenantDbContext> CreateAsync()
        {
            string? tenantName = await this.tenantService.GetTenantNameAsync();
            string connectionString = this.tenantService.GetTenantConnectionString(tenantName!);
            TenantDbContext db = await this.dbContextFactory.CreateDbContextAsync();

            db.Database.SetConnectionString(connectionString);
            var exists = await db.Database.CanConnectAsync();  //
            if (!exists)                                       //
            {                                                  //
                db.Database.Migrate();                         // There are better ways outside the scope of this poc
            }                                                  // Ideally only called once per tenant setup... 
            return db;
        }
    }
}
