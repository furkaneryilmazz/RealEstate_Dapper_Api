using Microsoft.AspNetCore.Authentication.JwtBearer;
using RealEstate_Dapper_UI.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Login/Index"; //login olmadan önce yönlendirilecek sayfa
    opt.LogoutPath = "/Login/Logout"; //çýkýþ yapýldýktan sonra yönlendirilecek sayfa
    opt.AccessDeniedPath = "/Pages/AccessDenied"; //yetkisiz eriþimde yönlendirilecek sayfa
    opt.Cookie.HttpOnly = true; //cookie'yi sadece http üzerinden eriþilebilir yapar
    opt.Cookie.SameSite = SameSiteMode.Strict; //cookie'nin güvenliði için kullanýlýr
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; //cookie'nin güvenliði için kullanýlýr
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
