using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using passionprojectn01708024.Data;
using passionprojectn01708024.Interfaces; // Your interfaces
using passionprojectn01708024.Services; // Your services

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
	?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configure DbContext with Identity support
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

// Add ASP.NET Core Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
	options.SignIn.RequireConfirmedAccount = true; // Adjust according to your needs
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Register your services and interfaces
builder.Services.AddScoped<IEventService, EventService>();
// Uncomment and add other services as needed
builder.Services.AddScoped<IAttendeeService, AttendeeService>();
builder.Services.AddScoped<ILocationService, LocationService>();

// Add MVC support
builder.Services.AddControllersWithViews();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
		c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
	});
}
else
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

// Map Razor pages if you're using them
app.MapRazorPages();

app.Run();
