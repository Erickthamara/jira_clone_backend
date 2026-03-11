using jira_clone_backend.Services.JWTService;
using jira_clone_backend.Services.ProjectService;
using jira_clone_backend.Services.TaskService;
using jira_clone_backend.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



DotNetEnv.Env.Load();

string postgresUri = Environment.GetEnvironmentVariable("PostgresConnectionString") ?? throw new ArgumentNullException("PostgresConnectionString is missing");
string jwtIssuer = Environment.GetEnvironmentVariable("JWT_Issuer") ?? throw new ArgumentNullException("JWT_Issuer is missing");
string jwtAudience = Environment.GetEnvironmentVariable("JWT_Audience") ?? throw new ArgumentNullException("JWT_Audience is missing");
string jwtToken = Environment.GetEnvironmentVariable("JWT_Token") ?? throw new ArgumentNullException("JWT_Token is missing");

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

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
)


    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidAudience = jwtAudience,
            // The default for this is 5 minutes, so if you're using short-lived tokens, set this to zero
            ClockSkew = TimeSpan.Zero, // <-- IMPORTANT
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken)),
            ValidateIssuerSigningKey = true,
        };
        //options.Events = new JwtBearerEvents
        //{
        //    OnMessageReceived = context =>
        //    {
        //        var jwtCookie = context.Request.Cookies["jwt_token"];
        //        if (!string.IsNullOrEmpty(jwtCookie))
        //        {
        //            context.Token = jwtCookie;
        //        }

        //        return Task.CompletedTask;
        //    }
        //};
    });

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
