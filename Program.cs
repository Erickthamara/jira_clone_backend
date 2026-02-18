using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



DotNetEnv.Env.Load();

string postgresUri = Environment.GetEnvironmentVariable("PostgresConnectionString") ?? throw new ArgumentNullException("PostgresConnectionString is missing");

// Convert URI to Npgsql format
string npgsqlConnectionString = ConvertPostgresUriToNpgsql(postgresUri);

builder.Services.AddDbContextPool<jira_clone_backend.Data.JiraContext>(opt =>
  opt.UseNpgsql(npgsqlConnectionString));

// Helper method
static string ConvertPostgresUriToNpgsql(string postgresUri)
{
    var uri = new Uri(postgresUri);

    var host = uri.Host;
    var port = uri.Port;
    var database = uri.AbsolutePath.TrimStart('/');
    var username = uri.UserInfo.Split(':')[0];
    var password = uri.UserInfo.Split(':')[1];

    return $"Host={host};Port={port};Database={database};Username={username};Password={password}";
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    //app.UseSwagger();
//    //app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
