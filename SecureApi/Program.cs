using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SecureApi.Authorization.Handlers;
using SecureApi.Authorization.Requirements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAuthorizationHandler, IsDudeHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsDude", policy =>
    {
        policy.AddRequirements(new IsDudeRequirement());
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "http://localhost:8080";
        options.Audience = "account";
        options.RequireHttpsMetadata = false;
        options.MetadataAddress = "http://localhost:8080/realms/Dude/.well-known/openid-configuration";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            /* Setting this allows us to use [Authorize(Roles = "Role")], even though
               the "role" claim doesn't exist, which is what .NET looks for by default. */
            RoleClaimType = "groups",
        };
    });

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
