// Copyright (c) 2023, Brian Parker. All Rights Reserved.
// ITenantItemService.cs licensed under the MIT License.

using MultiTenantDbContext.Models;

namespace MultiTenantDbContext.Services.TenantItems
{
    public interface ITenantItemService
    {
        ValueTask<SomeData> AddItemAsync(SomeData someData);
        ValueTask<IQueryable<SomeData>> QueryItemsAsync();
    }
}
