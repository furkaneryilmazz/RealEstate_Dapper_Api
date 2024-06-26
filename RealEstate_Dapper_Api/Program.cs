using RealEstate_Dapper_Api.Containers;
using RealEstate_Dapper_Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.ContainerDependencies();


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader() //D��ar�dan gelen isteklerde header bilgilerini kontrol etme
        .AllowAnyMethod()   //D��ar�dan gelen isteklerde method bilgilerini kontrol etme
        .SetIsOriginAllowed((host) => true) //D��ar�dan gelen isteklerde origin bilgilerini kontrol etme
        .AllowCredentials(); //D��ar�dan gelen isteklerde credential bilgilerini kontrol etme credential: kimlik bilgisi
    });
});
builder.Services.AddHttpClient();
builder.Services.AddSignalR();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy"); //CorsPolicy ad�nda bir policy tan�mlad�k ve bu policy'yi kullan dedik

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<SignalRHub>("/signalrhub");
//localhost:1234/swagger/category/index
//localhost:1234/signalrhub

app.Run();