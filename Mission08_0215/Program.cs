using Microsoft.EntityFrameworkCore;
using Mission08_0215.Models;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();

// Register TaskContext with SQLite connection string
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlite("Data Source=TaskQuadrant.sqlite"));

// Register the Repository Pattern - inject ITaskRepository, get TaskRepository
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

// Seed the database on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TaskContext>();
    SeedData.Initialize(context);
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
