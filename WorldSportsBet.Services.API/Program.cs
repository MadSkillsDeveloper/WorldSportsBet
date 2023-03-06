using Hangfire;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Net.Http.Headers;
using WorldSportsBet.Services.API.ApplicationServices;
using WorldSportsBet.Services.API.ApplicationServices.Workers;
using WorldSportsBet.Services.API.Extensions;
using WorldSportsBetting.Services.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration
                    .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", false, true)
                     .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSpaStaticFiles(cfg =>
{
    cfg.RootPath = "clientapp/dist";
});

//Install Prepare Hangfire as Messaging Queue with Mongo
builder.Services.AddTransient<IJobs, Jobs>();
builder.ConfigureHangfireServices(configuration.GetConnectionString("MongoDbConnection"));
builder.Services.AddSignalR();

builder.ConfigureApplicationServices("CurrencyRateDatabase", "ApiSettings");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureJobs(configuration.GetConnectionString("MongoDbConnection"));
app.UseHangfireDashboard();
app.UseAuthorization();

app.MapControllers();
app.MapHub<CurrencyRateHub>("/CurrencyRate");


var spaPath = "/app";

if (app.Environment.IsDevelopment())
{
    app.MapWhen(p => p.Request.Path.StartsWithSegments(spaPath), client =>
    {
        client.UseSpa(spa =>
        {
            spa.UseProxyToSpaDevelopmentServer("https://localhost:4000");
        });
    });
}
else
{
    app.Map(new PathString(spaPath), client =>
    {
        client.UseSpaStaticFiles();
        client.UseSpa(spa =>
        {
            spa.Options.SourcePath = "clientapp";
            spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ResponseHeaders responseHeaders = ctx.Context.Response.GetTypedHeaders();
                    responseHeaders.CacheControl = new CacheControlHeaderValue
                    {
                        NoCache = true,
                        NoStore = true,
                        MustRevalidate = true,
                    };
                }
            };
        });
    });
}


app.Run();
