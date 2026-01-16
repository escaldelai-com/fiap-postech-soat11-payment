using Restaurant.Payment.Application;
using Restaurant.Payment.Data;
using Restaurant.Payment.ExternalServices;
using Restaurant.Payment.Facade;
using Restaurant.Payment.Presenter;
using Restaurant.Payment.WebApi.Middleware;
using Restaurant.Payment.WebApi;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services
    .AddControllers()
    .AddNewtonsoftJson();
builder.Services
    .AddPresenter()
    .AddData()
    .AddExternalServices()
    .AddApplication()
    .AddFacade()
    .AddAuthentication(builder.Configuration)
    .AddRestaurantAuthorization()
    .AddOpenApi();


// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.MapOpenApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandler();
app.UseErrorHandler();
app.MapControllers();


// Run the application
app.Run();
