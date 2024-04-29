using Microsoft.AspNetCore.Authentication.JwtBearer;
using RealEstate_Dapper_UI.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Login/Index"; //login olmadan �nce y�nlendirilecek sayfa
    opt.LogoutPath = "/Login/Logout"; //��k�� yap�ld�ktan sonra y�nlendirilecek sayfa
    opt.AccessDeniedPath = "/Pages/AccessDenied"; //yetkisiz eri�imde y�nlendirilecek sayfa
    opt.Cookie.HttpOnly = true; //cookie'yi sadece http �zerinden eri�ilebilir yapar
    opt.Cookie.SameSite = SameSiteMode.Strict; //cookie'nin g�venli�i i�in kullan�l�r
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; //cookie'nin g�venli�i i�in kullan�l�r
    opt.Cookie.Name = "RealEstateJwt"; //cookie'nin ismi
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoginService, LoginService>();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
