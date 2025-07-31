using Api.Configurations;
using Application;
using Application.Model;
using Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging();
builder.Services.AddSwaggerGen();
builder.Logging.AddLog4Net();
builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.json").AddEnvironmentVariables();

var applicationConfiguration = builder.Configuration.Get<ApplicationConfiguration>();


builder.Services.AddSingleton(applicationConfiguration);
var loggerFactory = new LoggerFactory().AddLog4Net();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowCredentials()
            .AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseCors("AllowFrontend");
app.MapControllers();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseHsts();
}
await app.UseMigrations();

app.Run();