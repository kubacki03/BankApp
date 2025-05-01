
using BankApp.Server.Interfaces;
using BankApp.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BankContextConnection") ?? throw new InvalidOperationException("Connection string  not found."); ;
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
/*
//dodaje serwisy
builder.Services.AddScoped<AuthService, AuthService>();
builder.Services.AddScoped<TransferServices, TransferServices>();
builder.Services.AddScoped<AccountDetailsService, AccountDetailsService>();
// Register your IRepository interface with its implementation
builder.Services.AddScoped<IRepository, RepositoryService>();
builder.Services.AddScoped<RepositoryService>(); // needed if any service uses RepositoryService directly

builder.Services.AddScoped<ITransfer, TransferServices>();
builder.Services.AddScoped<TransferServices>(); // needed if any service uses TransferServices directly

builder.Services.AddScoped<AccountDetailsService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Authorization");

    });
});
var app = builder.Build();
app.UseCors("AllowAll");
app.UseDefaultFiles();
app.MapStaticAssets();



app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
