using Microsoft.EntityFrameworkCore;
using KabanApp.Data;
using KabanApp.Models;
using TaskStatusEnum = KabanApp.Models.TaskStatus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Entity Framework and configure the database connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Ensure database is created
    context.Database.EnsureCreated();

    // Check if we already have data
    if (!context.Tasks.Any())
    {
        var tasks = new List<TaskItem>
        {
            new TaskItem
            {
                Title = "Setup Database",
                Description = "Configure SQLite and Entity Framework",
                Status = TaskStatusEnum.Done,
                CreatedDate = DateTime.Now.AddDays(-3)
            },
            new TaskItem
            {
                Title = "Create Models",
                Description = "Build TaskItem and TaskStatus models",
                Status = TaskStatusEnum.Done,
                CreatedDate = DateTime.Now.AddDays(-2)
            },
            new TaskItem
            {
                Title = "Build Controllers",
                Description = "Implement CRUD operations",
                Status = TaskStatusEnum.InProgress,
                CreatedDate = DateTime.Now.AddDays(-1)
            },
            new TaskItem
            {
                Title = "Create Views",
                Description = "Build Kanban board interface",
                Status = TaskStatusEnum.Ready,
                CreatedDate = DateTime.Now
            },
            new TaskItem
            {
                Title = "Add Drag & Drop",
                Description = "Implement drag and drop functionality",
                Status = TaskStatusEnum.Backlog,
                CreatedDate = DateTime.Now
            },
            new TaskItem
            {
                Title = "Testing",
                Description = "Write unit tests",
                Status = TaskStatusEnum.Backlog,
                CreatedDate = DateTime.Now
            }
        };

        context.Tasks.AddRange(tasks);
        context.SaveChanges();
    }
}

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

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Content-Security-Policy",
        "script-src 'self'; object-src 'none';");
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
