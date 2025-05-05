using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OfficeNet.Domain.Entities;
using OfficeNet.Exceptions;
using OfficeNet.Extensions;
using OfficeNet.Infrastructure.Context;
using OfficeNet.Infrastructure.Mapping;
using OfficeNet.Service.TokenService;
using OfficeNet.Service.UserService;
using OfficeNet.Service;
using OfficeNet.Service.Roles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Auth", Version = "v1", Description = "Services to Authenticate user" });


    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter a valid token in the following format: {your token here} do not add the word 'Bearer' before it."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});



// Adding Database context 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbConnection"));
});

// Adding Identity

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()  this commented because i changed Identitu role to applicatioin role
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Adding Services  
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<ITokenService, TokenServiceImple>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IRoleService, RoleImple>();


// Regsitering AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp",
//        builder => builder
//            .WithOrigins("http://localhost:4500") // your frontend URL
//            .AllowAnyHeader()
//            .AllowAnyMethod());
//});


// Adding Jwt from extension method
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureCors();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
