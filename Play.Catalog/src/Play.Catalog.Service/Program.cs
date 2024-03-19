using MongoDB.Driver;
using Play.Catalog.MongoDB;
using Play.Catalog.Service.Models;

using Play.Common.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
builder.Services.AddMongo().AddMongoRepository<Item>("items");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    _ = app.MapControllerRoute(
        name: "items",
        pattern: "{controller=Items}/");

    // _ = endpoints.MapControllerRoute(
    // name: "default",
    // pattern: "{controller=Home}/{action=Index}/{id?}");

    // _ = endpoints.MapControllerRoute(
    //                 name: "Teacher",
    //                 pattern: "Teacher/{controller=Teacher}/{action=Index}/{id?}");

    // _ = endpoints.MapControllerRoute(
    //                 name: "Student",
    //                 pattern: "Student/{controller=Student}/{action=Index}/{id?}");

});



app.Run();
