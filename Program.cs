using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductApi.Configration;
using ProductApi.Model;
using ProductApi.Servies.SCategores;
using ProductApi.Servies.SProduct;
using ProductApi.Servies.SUser;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<FluentValidations>();
builder.Services.AddSwaggerGen(s =>
{
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT like: Bearer {token}"
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});// Add services to the container.

builder.Services.AddDbContext<AppDbContexests>(
    option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("FC")));
builder.Services.AddControllers();
builder.Services.AddScoped<IRUser, RUser>();
builder.Services.AddScoped<IRCategores, RCategores>();
builder.Services.AddScoped<IRProduct, RProduct>();
builder.Services.AddScoped<ISProduct, SProduct>();
builder.Services.AddScoped<ISCategores, SCategores>();
builder.Services.AddScoped<ISUser, SUsers>();
builder.Services.AddAutoMapper(typeof(Program));
var settingJwt = builder.Configuration.GetSection("JWT");
var Key = Encoding.UTF8.GetBytes(settingJwt["Key"]);
builder.Services.Configure<JWT>(settingJwt);
builder.Services.AddAuthentication(S =>
{
    S.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    S.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).
AddJwtBearer(s => s.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,

    ValidAudience = settingJwt["Audience"],
    ValidIssuer = settingJwt["Issuer"],
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Key),

});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
