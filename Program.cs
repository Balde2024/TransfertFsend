using System.Reflection;
using System.Text;
using ForSendKH.Configurations;
using ForSendKH.Contracts;
using ForSendKH.Data.DataContext;
using ForSendKH.Middleware;
using ForSendKH.Models;
using ForSendKH.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_MyAllowSpecificOrigins";
var ConnectionString = builder.Configuration.GetConnectionString("TransfertDbConnectionString");
// Add services to the container.

builder.Services.AddDbContext<TransfertDbContext>(options =>
{
    options.UseSqlServer(ConnectionString);
});

builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<ApiUser>>("TransfertForSend")
    .AddEntityFrameworkStores<TransfertDbContext>().AddDefaultTokenProviders();

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen( options =>
{
    options.SwaggerDoc( "v1" ,
        new OpenApiInfo {
            Version = "v1",
            Title = "API ForSendKHTransfert",
            Description = "Application de transfert d'argent",
            TermsOfService = new Uri("https://ForSendKHT.com/transfert"),
            Contact = new OpenApiContact
            {
                Name = "Contactez notre equipe au +33767672825",
                Url = new Uri("https://contactForSendKHTransact.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "@2023 KH Transactions Licence",
                Url = new Uri("https://Khtransaction.com/license")
            }
        });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorisation header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in then text input below.
                        Example: 'Bearer 1234Sabcdef'
                        ",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement( new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "0auth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },

            new List<string>()
        }
    });


    // defini le chemin des commentaires pour l'interface du swagger.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins ,policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver")
        );
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    }
    );

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration)
    ) ;

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IExpediteurRepository, ExpediteurRepository>();
builder.Services.AddScoped<ITransfertRepository, TransfertRepository>();
builder.Services.AddScoped<IBeneficiereRepository, BeneficiereRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,//Permet de connaitre l’auteur du jeton en lui affectant une Url, un nom de serveur, d’application ou tout autres informations permettant d’en connaitre l’origine.
        ValidateAudience = true,/*Permet de connaitre le type d’audience ciblée par exemple le type de client comme par exemple mobile ou web ou la nature de l’application qui peut être en développement ou en production. L’interprétation de l’audience est propre à l’application.*/
        ValidateLifetime = true,//Valider les valeurs d’expiration
        ClockSkew = TimeSpan.Zero, //Pas de tolérance pour la date d’expiration
        ValidIssuer= builder.Configuration["JwtSettings:Issuer"],
        ValidAudience= builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),

    };
});

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024;
    options.UseCaseSensitivePaths = true;
});

builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().OrderBy();

});

builder.Services.AddMvc().AddXmlSerializerFormatters();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.UseResponseCaching();
app.Use(async (context , next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
      new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
      {
          Public = true,
          MaxAge = TimeSpan.FromSeconds(10)
      };
    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
    new string[] { "Accept-Encoding" };

    await next();
});

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();