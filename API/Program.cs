using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//1.
// builder.Services.AddDbContext<DC>(opt => 
// {
//     opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
// });
// //2.
// builder.Services.AddCors();

// //3.
// builder.Services.AddScoped<ITokenService, TokenService>();
//4. 

builder.Services.AddApplicationService(builder.Configuration);

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//   .AddJwtBearer(options => 
//   {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//       ValidateIssuerSigningKey = true,
//       IssuerSigningKey = new SymmetricSecurityKey(Encoding
//       .UTF8.GetBytes(builder.Configuration["TokenKey"])),
//       ValidateIssuer = false,
//       ValidateAudience = false
//     };
//   }
//   );


builder.Services.AddIdentityService(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  //  app.UseSwagger();
    //app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

//2.
app.UseCors(builder=>builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
//4.
app.UseAuthentication();
//5.
app.UseAuthorization();

app.MapControllers();

app.Run();
