
using EmailOtpApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // Enable MVC
builder.Services.AddScoped<EmailOtpService>(); //Add controller service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Use MapControllerRoute for MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Otp}/{action=Index}/{id?}");

app.Run();