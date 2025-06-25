using Application.Common.Interfaces;
using Domain.Constants;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Threading; // for CancellationToken




namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            // 1. Connection string
            var connectionString = builder.Configuration.GetConnectionString("MyProjectDb");
            Guard.Against.Null(connectionString, message: "Connection string 'MyProjectDb' not found.");

            // 2. EF interceptors
            builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            // 3. DbContext + both async and sync seeding
            builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);

                // runtime async seeding
                // EF expects Func<DbContext, bool, CancellationToken, Task>
                options.UseAsyncSeeding((ctx, designTime, ct) =>
                    sp.GetRequiredService<ApplicationDbContextInitialiser>()
                      .SeedAsync()            // <-- no args
                );

                // design-time sync seeding
                // EF expects Action<DbContext, bool>
                options.UseSeeding((ctx, designTime) =>
                    sp.GetRequiredService<ApplicationDbContextInitialiser>()
                      .SeedAsync()            // <-- no args
                      .GetAwaiter()
                      .GetResult()
                );
            });

            // 4. expose IApplicationDbContext
            builder.Services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            // 5. Initialiser
            builder.Services.AddScoped<ApplicationDbContextInitialiser>();

            // 6. Identity
           builder.Services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api"))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    }

                    context.Response.Redirect(context.RedirectUri);
                    return Task.CompletedTask;
                };
            });


            // 7. Misc
            builder.Services.AddSingleton(TimeProvider.System);
            builder.Services.AddTransient<IIdentityService, IdentityService>();

            // 8. Authorization
            builder.Services.AddAuthorization(options =>
                options.AddPolicy(Policies.CanPurge,
                    policy => policy.RequireRole(Roles.Administrator)));
        }
    }
}
