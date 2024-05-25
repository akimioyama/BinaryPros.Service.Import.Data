using Serilog;

using Service.Registration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hc, lc) => lc.ReadFrom.Configuration(hc.Configuration));

builder.Services
    .MainExtension(builder.Configuration)
    .AddHealthChecks();


var app = builder.Build();

app.MapHealthChecks("/hc");

app.Run();