using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiniQr.API;
using MiniQr.Application.Commands.CriarCobranca;
using MiniQr.Application.Services.Authorization;
using MiniQr.Domain.Models;
using MiniQr.Domain.Services.IntegracaoExterna;
using MiniQr.Persistence;
using MiniQr.Utils.Constants;
using MiniQr.Utils.Exceptions;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//base de dados
var connectionStringMiniQr = builder.Configuration.GetConnectionString("MiniQrConnection");
builder.Services.AddDbContext<MiniQrContext>(options => options
    .UseSqlServer(connectionStringMiniQr));

//identity
builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<MiniQrContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
});

//mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//configuração de cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiniQrAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<AuthResponsesFilter>();
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//autenticacao
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>
  {
      var symmetricSecurityKey = builder.Configuration.GetValue<string>(ConfiguracaoConstants.SymmetricSecurityKey) ?? throw new ConfiguracaoAusenteException(ConfiguracaoConstants.SymmetricSecurityKey);
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricSecurityKey)),
          ValidateAudience = false,
          ValidateIssuer = false,
          ClockSkew = TimeSpan.Zero
      };
  });

//mediatr
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(CriarCobrancaHandler).Assembly);
});

//services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<CriptografiaService>();
builder.Services.AddScoped<ComunicaNovaCobrancaService>();
builder.Services.AddScoped<ComunicaCancelamentoCobrancaService>();
builder.Services.AddScoped<ComunicaPagamentoCobrancaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddValidatorsFromAssembly(typeof(CriarCobrancaValidator).Assembly);
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseCors();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//cria e popula base de dados
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<MiniQrContext>();

if (context.Database.GetPendingMigrations().Any())
{
    context.Database.Migrate();
}
await ApplicationDbInitializer.SeedUserMasterAsync(services);

app.Run();
