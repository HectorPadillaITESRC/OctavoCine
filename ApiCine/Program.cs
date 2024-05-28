using ApiCine.Helpers;
using ApiCine.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NoticiasOctavoAPI.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        var issuer = builder.Configuration.GetSection("Jwt").GetValue<string>("Issuer");
        var audience = builder.Configuration.GetSection("Jwt").GetValue<string>("Audience");
        var secret = builder.Configuration.GetSection("Jwt").GetValue<string>("Secret");

        x.TokenValidationParameters = new()
        {
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret ?? "")),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true
        };

    }
);

var connectionString = builder.Configuration.GetConnectionString("NoticiasConnectionString");

builder.Services.AddDbContext<ItesrcneOctavoContext>(x =>
    x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddSingleton<JwtHelper>();

var app = builder.Build();



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
