using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Qualification.Data.IRepositories;
using Qualification.Data.Repositories;
using Qualification.Service.AvloniyClient;
using Qualification.Service.Interfaces;
using Qualification.Service.Mappers;
using Qualification.Service.Services;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace Qualification.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentRequestRepository, PaymentRequestRepository>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<IFileUploadRepository, FileUploadRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IQuizQuestionRepository, QuizQuestionRepository>();
        services.AddScoped<ISubmissionRepository, SubmissionRepository>();
        services.AddScoped<IQuizResultRepository, QuizResultRepository>();

        services.AddScoped<IAvloniyClientService, AvloniyClientService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<ISchoolService, SchoolService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IFileUploadService, FileUploadService>();
        services.AddScoped<IQuizService, QuizService>();
        services.AddScoped<IPaymentRequestService, PaymentRequestService>();
        services.AddScoped<IQuizResultService, QuizResultService>();
        services.AddScoped<ISubmissionService, SubmissionService>();
        services.AddScoped<ISertificateService, SertificateService>();

        services.AddScoped<IReportService, ReportService>();

        services.AddAutoMapper(typeof(MapperProfile));
    }

    public static void AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("avloniy", config =>
        {
            var username = configuration.GetSection("ERP:username").Value;
            var password = configuration.GetSection("ERP:password").Value;

            config.BaseAddress = new Uri(configuration.GetSection("ERP:base_address").Value);
            var svcCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
            config.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);
        });
    }

    public static void AddJwtService(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication();

        // Jwt bearer configuration
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]))
            };
        });
        services.AddMvc().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    }

    public static void AddCorsService(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });
    }

    public static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Qualification.Api", Version = "v1" });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
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
                        }
                    },
                    new string[] { }
                }
            });
        });
    }
}