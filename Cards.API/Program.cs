using Cards.API.TokenHelper;
using Cards.API.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Database configuration
builder.Services.ConfigureApplicationDatabase(builder: builder);
//JWT Token configuration
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("Jwt"));
var jwt = builder.Configuration.GetSection("Jwt").Get<JwtConfig>();
builder.Services.ConfigureJwtToken(config: jwt);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureApplicationSwagger();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddApplicationServices();

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
