using Microsoft.EntityFrameworkCore;
using Contract_Monthly_Claim_System_POE.Models; // Ensure the namespace matches your DbContext location


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout for user inactivity
    options.Cookie.HttpOnly = true; // Security: Prevent client-side script access
    options.Cookie.IsEssential = true; // Required for session to function
});

// Add MySQL database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redirect HTTP traffic to HTTPS
app.UseStaticFiles();       // Serve static files (e.g., CSS, JS, images)
app.UseRouting();           // Enable routing for controllers
app.UseSession();           // Enable session middleware
app.UseAuthorization();     // Authorization middleware

// Map the default route for MVC controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
