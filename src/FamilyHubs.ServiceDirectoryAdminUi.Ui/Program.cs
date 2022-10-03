using FamilyHubs.ServiceDirectoryAdminUi.Ui.Extensions;
using FamilyHubs.ServiceDirectoryAdminUi.Ui.Services;
using System.IdentityModel.Tokens.Jwt;
//using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddClientServices()
    .AddWebUIServices(builder.Configuration);

builder.Services.AddTransient<IViewModelToApiModelHelper, ViewModelToApiModelHelper>();

// Add services to the container.
builder.Services.AddRazorPages();

// Add Session middleware
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(600); //TODO - set time in config
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    //options.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies");

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

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();

public partial class Program { }

