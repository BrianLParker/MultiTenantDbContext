// Copyright (c) 2023, Brian Parker. All Rights Reserved.
// Program.cs licensed under the MIT License.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiTenantDbContext.Client.Pages;
using MultiTenantDbContext.Components;
using MultiTenantDbContext.Components.Account;
using MultiTenantDbContext.Data;
using MultiTenantDbContext.Data.Optimizations.IdentityContext;
using MultiTenantDbContext.Data.Optimizations.TenantContext;
using MultiTenantDbContext.Models;
using MultiTenantDbContext.Services.TenantItems;
using MultiTenantDbContext.Services.Tenants;

namespace MultiTenantDbContext
{
    public partial class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            LocalConfiguration LocalConfiguration = builder.Configuration.Get<LocalConfiguration>()!;
            builder.Services.AddScoped<ITenantService, TenantService>();
            builder.Services.AddScoped<ITenantItemService, TenantItemService>();
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.Services.AddSingleton(_ => LocalConfiguration);
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();
            builder.Services.AddTransient<IClaimsTransformation, TenantClaimTransformation>();

            var connectionString = LocalConfiguration.ConnectionStrings["DefaultConnection"];

            builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(LocalConfiguration.ConnectionStrings["DefaultConnection"]);
                options.UseModel(ApplicationDbContextModel.Instance);
            });

            builder.Services.AddDbContextFactory<TenantDbContext>(options =>
            {
                var tenantConnectionString = string.Format(LocalConfiguration.ConnectionStrings["TenantConnection"], "Default");
                options.UseSqlServer(tenantConnectionString);
                options.UseModel(TenantDbContextModel.Instance);
            }, ServiceLifetime.Scoped);

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Counter).Assembly);

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();
            app.Run();
        }

    }
}
