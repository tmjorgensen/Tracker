using Infrastructure.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
;

var builder = WebApplication.CreateBuilder(args);

var b = builder.Configuration.GetSection("Store");

builder.Services.AddOptions<StoreConfiguration>().BindConfiguration("Store");

builder.Services.AddDbContext<AppDbContext>((provider, options) =>
{
    var config = provider.GetRequiredService<IOptions<StoreConfiguration>>().Value;
    _ = config.Provider.ToLowerInvariant() switch
     {
         "sqlserver" => options.UseSqlServer(
             (builder.Configuration.GetConnectionString("SqlServer")
                 ?? throw new Exception($"Database connection string missing for provider {config.Provider}:")),
             x => x.MigrationsAssembly("Infrastructure.Store.SqlServerMigrations")),

         "mysql" => options.UseMySQL(
             (builder.Configuration.GetConnectionString("MySql")
                 ?? throw new Exception($"Database connection string missing for provider {config.Provider}:")),
             x => x.MigrationsAssembly("Infrastructure.Store.MySqlMigrations")),

         _ => throw new Exception($"Unsupported database provider {config.Provider}.")
     };
});

//var dbProvider = builder.Configuration.GetValue("StoreProvider", "SqlServer");
//builder.Services.AddDbContext<AppDbContext>(options =>
//    _ = dbProvider?.ToLowerInvariant() switch
//    {
//        "sqlserver" => options.UseSqlServer(
//            (builder.Configuration.GetConnectionString("SqlServer") 
//                ?? throw new Exception($"Database connection string missing for provider {dbProvider}:")),
//            x => x.MigrationsAssembly("Infrastructure.Store.SqlServerMigrations")),
//        "mysql" => options.UseMySQL(
//            (builder.Configuration.GetConnectionString("MySql") 
//                ?? throw new Exception($"Database connection string missing for provider {dbProvider}:")),
//            x => x.MigrationsAssembly("Infrastructure.Store.MySqlMigrations")),
//        _ => throw new Exception($"Unsupported database provider {dbProvider}.")
//    }
//);

// Add services to the container.

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
