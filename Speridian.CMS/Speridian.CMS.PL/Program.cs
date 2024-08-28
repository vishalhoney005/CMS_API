using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Speridian.CMS.BL;
using Speridian.CMS.DAL.Repositories;
using Speridian.CMS.PL.Config;
using Speridian.CMS.PL.Models;
using Speridian.CMS.Exceptions;
using Speridian.CMS.Helpers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<MenuBL>();
builder.Services.AddScoped<CustomerBL>();
builder.Services.AddScoped<FeedbackBL>();
builder.Services.AddScoped<InvoiceBL>();
builder.Services.AddScoped<UserBL>();
builder.Services.AddScoped<ReportBL>();
builder.Services.AddScoped<LoginBL>();
builder.Services.AddScoped<RoleBL>();
builder.Services.AddScoped<MenuDAL>();
builder.Services.AddScoped<RoleDAL>();
builder.Services.AddScoped<CustomerDAL>();
builder.Services.AddScoped<FeedbackDAL>();
builder.Services.AddScoped<InvoiceDAL>();
builder.Services.AddScoped<UserDAL>();
builder.Services.AddScoped<ReportDAL>();
builder.Services.AddScoped<LoginDAL>();
builder.Services.AddScoped<AuthorizeService>();
builder.Services.AddDbContext<CmsDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CMS WebApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please Insert Token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
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
});
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(b =>
{
    b.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});

//builder.Services.AddAuthentication(a =>
//{
//    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(o =>
//{
//    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
//    o.SaveToken = true;
//    o.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateAudience = true,
//        ValidateIssuer = true,
//        ValidateIssuerSigningKey = true,
//        ValidateLifetime = true,
//        ValidIssuer = builder.Configuration["JWT:Issuer"],
//        ValidAudience = builder.Configuration["JWT:Audience"],
//        ClockSkew = TimeSpan.Zero,
//        IssuerSigningKey = new SymmetricSecurityKey(key)
//    };
//    o.Events = new JwtBearerEvents()
//    {
//        OnAuthenticationFailed = context =>
//        {
//            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
//            {
//                context.Response.Headers.Add("SecurityToken Experied", "true");
//            }
//            else if (context.Exception.GetType() == typeof(SecurityTokenInvalidSignatureException))
//            {
//                context.Response.Headers.Add("SecurityToken Signature Tampered", "true");
//            }
//            return Task.CompletedTask;
//        }
//    };
//});
Log.Logger = new LoggerConfiguration().MinimumLevel.Error().WriteTo.File("Log/Logging.txt", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
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
app.UseCors("AllowAll");
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();

app.Run();
