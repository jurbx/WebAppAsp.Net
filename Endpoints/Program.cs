

using Endpoints;
using Endpoints.Entity;
using Microsoft.EntityFrameworkCore;

var builder = CreateHostBuilder(args);
var app = builder.Build();
app.Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args).
    ConfigureWebHostDefaults(builder =>
    {
        builder.UseStartup<Startup>();
    });