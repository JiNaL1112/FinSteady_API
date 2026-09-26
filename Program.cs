




using FinSteady_API.Infrastructure;
using FinSteady_API.Repositories;
using FinSteady_API.Repositories.Interface;
using FinSteady_API.Service.Interface;
using FinSteady_API.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
// Add services to the container.
builder.Services.AddDbContext<SmartSaverDatabaseContext>(option =>
{
    // option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    // option.UseSqlServer("Data Source=AJ; Initial Catalog=SmartSaverDatabase; Trusted_Connection=True; TrustServerCertificate=True;");
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddEntityFrameworkNpgsql();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISavingGoalRepository, SavingGoalRepository>();
builder.Services.AddScoped<ISavingRuleRepository, SavingRuleRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();


// Add DummyDataService registration
builder.Services.AddScoped<IDummyDataService, DummyDataService>();

// Load configuration settings from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Optimizing API performance with techniques like response compression and HTTP/2.
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true; // Enable compression for HTTPS requests (optional)
});

//Optimizing API performance with techniques like response compression and HTTP/2.
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Optimal; // Set compression level (optional)
});

var app = builder.Build();


// Ensure dummy data when application starts
using (var scope = app.Services.CreateScope())
{
    var dummyDataService = scope.ServiceProvider.GetRequiredService<IDummyDataService>();
    dummyDataService.EnsureAllDummyDataAsync().Wait(); // EnsureAllDummyDataAsync should be awaited in a non-async context
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
