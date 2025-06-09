using AccountService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register repository as singleton
builder.Services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();

builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();
