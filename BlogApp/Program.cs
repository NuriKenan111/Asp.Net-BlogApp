using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options =>{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("sql_connection");
    options.UseSqlite(connectionString);
    // var version = new MySqlServerVersion(new Version(8, 2, 0));
    // options.UseMySql(connectionString, version);
});

builder.Services.AddScoped<IPostRepository, EfCoreRepository>();

var app = builder.Build();

SeedData.FillTestData(app);

app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
