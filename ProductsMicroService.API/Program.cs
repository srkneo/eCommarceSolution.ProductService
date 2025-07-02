using eCommerce.ProductsService.DataAccessLayer;
using eCommerce.ProductsService.BusinessLogicLayer;
using FluentValidation.AspNetCore;
using ProductsMicroService.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Add Dal and bll services
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

builder.Services.AddControllers();


//FluentValidations
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

//auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
