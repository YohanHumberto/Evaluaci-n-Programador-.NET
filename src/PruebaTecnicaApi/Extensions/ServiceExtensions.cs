using Application.Interfaces;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using PruebaTecnicaApi.MapperProfiles;
using Repository;
using Repository.ExternalServices;
using Repository.Repositories;
using System.Text;

namespace PruebaTecnicaApi.Extensions
{
    public static class ServiceExtensions
    {
        #region Register Custom Services

        /// <summary> 
        ///		Registers application services in the dependency injection container. 
        /// </summary>
        /// <param name="services"></param>
        public static void AddServices(this IServiceCollection services)
        {

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJsonPlaceholderService, JsonPlaceholderService>();

            #endregion Repositories

            #region Services

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IPostService, PostService>();

            #endregion Services

            #region Validators

            services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CreatePostRequestValidator>();

            #endregion
        }

        #endregion Register Custom Services


        #region Services Configs

        /// <summary>
        ///   Configures external services, such as HTTP clients, for the application.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddHttpClient<
                IJsonPlaceholderService,
                JsonPlaceholderService>(client =>
                {
                    client.BaseAddress =
                        new Uri("https://jsonplaceholder.typicode.com/");
                });

            return services;
        }

        /// <summary>
        ///  Add
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Ingrese el token JWT."
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });
            });

            return services;
        }

        /// <summary>
        ///    Configures JWT authentication for the application.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                configuration["Jwt:Key"]!))
                    };
                });

            return services;
        }

        /// <summary>
        ///     Add AutoMapper configuration and profiles.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile<MapperProfile>(),
                AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        ///     DB contexts setup.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthContext>(options =>
            {
                options.UseInMemoryDatabase("AuthDB");
            });
        }

        #endregion Services Configs


    }
}
