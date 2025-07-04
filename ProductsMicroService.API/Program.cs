using eCommerce.ProductsService.DataAccessLayer;
using eCommerce.ProductsService.BusinessLogicLayer;
using FluentValidation.AspNetCore;
using ProductsMicroService.API.Middleware;
using eCommerce.ProductsService.API.Endpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Add Dal and bll services
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

builder.Services.AddControllers();


//FluentValidations
builder.Services.AddFluentValidationAutoValidation();


// add model binder to read values from json to enum
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

//auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapProductAPIEndpoints();

app.Run();
