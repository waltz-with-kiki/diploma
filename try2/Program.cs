using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using try2.DAL;
using try2.DAL.Interfaces;
using try2.DAL.Models;
using try2.DAL.Repositories;
using try2.Domain.Entities;
using Version = try2.DAL.Models.Version;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

/*builder.Services.AddDbContext<AccountDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));*/


builder.Services.AddDbContext<AirplanesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("POSTGRESQL")));



builder.Services.AddTransient<IRepository<Version>, VersionRepository>();

builder.Services.AddTransient<IRepository<Project>, ProjectRepository>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();



app.UseCors(
    x =>
    {
        x.WithHeaders().AllowAnyHeader();
        x.WithOrigins("https://localhost:44449");
        x.WithMethods().AllowAnyMethod();
    }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
