using ApiAggregator.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExternalApiSettings(builder.Configuration);
builder.Services.AddExternalApiServices();
builder.Services.AddCommonServices();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseSwaggerDocumentation();
app.MapControllers();
app.Run();