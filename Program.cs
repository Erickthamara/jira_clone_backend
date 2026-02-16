using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



Env.Load();

string PosgresConnectionString = Environment.GetEnvironmentVariable("PostgresConnectionString") ?? throw new ArgumentNullException("PostgresConnectionString is missing");


builder.Services.AddDbContextPool<jira_clone_backend.Data.JiraContext>(opt =>
  opt.UseNpgsql(PosgresConnectionString));

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
