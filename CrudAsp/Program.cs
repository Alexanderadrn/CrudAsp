using CrudAsp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CrudaspContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CrudContext"));

});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "API",
                      builder =>
                      {
                          builder.WithHeaders("*");
                          builder.WithOrigins("*");
                          builder.WithMethods("*");

                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCors("API");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
