using Application;
using Application.Features.Behaviors;
using Application.Models;
using Insfrastructure;
using MediatR;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApi.Middlewares;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration);
var cacheSettingConfiguration = builder.Configuration.GetSection("CacheSettings");
builder.Services.Configure<CacheSettings>(cacheSettingConfiguration);

//configure Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = cacheSettingConfiguration["DistinationUrl"];
    
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseHsts();
app.UseCors();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DisplayRequestDuration();
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
        // ...
    });
    
}

app.Run();
