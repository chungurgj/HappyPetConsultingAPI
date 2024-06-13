using HP.API.BackgroundServices;
using HP.API.Data;
using HP.API.Hubs;
using HP.API.Mappings;
using HP.API.Models.Domain;
using HP.API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHostedService<ConsultationUpdaterService>();
//builder.Services.AddHostedService<AppointmentNotifierService>();

builder.Logging.AddConsole();

builder.Services.AddDbContext<HPAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection")));

builder.Services.AddDbContext<HPDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPetRepository, SQLPetRepository>();
builder.Services.AddScoped<IProductRepository, SQLProductRepository>();
builder.Services.AddScoped<ITokenRepository,TokenRepository>();
builder.Services.AddScoped<IVetVisitRepository, SQLVetVisitRepository>();
builder.Services.AddScoped<IConsultationRepository, SQLConsultationRepository>();

builder.Services.AddSingleton<IDictionary<string, string>>(new Dictionary<string, string>());

builder.Services.AddTransient<IEmailSender,EmailSender>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>("HP")
    .AddEntityFrameworkStores<HPAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotifyHub>("/notify");

app.Run();
