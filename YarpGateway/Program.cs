using Microsoft.Extensions.Logging.Console;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddLogging(loggingBuilder => loggingBuilder
            .ClearProviders()
            .AddSimpleConsole(conf =>
            {
                conf.ColorBehavior = LoggerColorBehavior.Enabled;
                conf.SingleLine = true;
                conf.TimestampFormat = "HH:mm:ss ";
            }));

var app = builder.Build();

//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapReverseProxy();
app.Run();

