using FluentValidation;
using Marten;
using Marten.Services.Events;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Npgsql;
using UserManagement.Application.Abstractions.Commands;
using UserManagement.Application.Decorators;

namespace UserManagement.Api
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "User management api",
                    Version = "v1",
                    Description = "A simple api for managing users"
                });
            });

            return services;
        }

        //public static IServiceCollection AddPostgresHealthCheck(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddHealthChecks()
        //        .AddNpgSql(GetConnectionString(configuration));

        //    return services;
        //}

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            AssemblyScanner
                .FindValidatorsInAssembly(typeof(ICommand).Assembly)
                .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

            return services;
        }

        public static IServiceCollection AddCommandHandlerDecorators(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingCommandHandlerDecorator<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationCommandHandlerDecorator<,>));

            return services;
        }

        public static IServiceCollection AddMartenDB(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = GetConnectionString(configuration);
            var options = new StoreOptions();
            options.Connection(connectionString);
            options.Events.UseAggregatorLookup(AggregationLookupStrategy.UsePrivateApply);
            //options.Events.InlineProjections.AggregateStreamsWith<>();
            services.AddMarten(options);

            return services;
        }
        
        private static string GetConnectionString(IConfiguration configuration)
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = configuration.GetValue<string>("POSTGRES_HOST", "ec2-23-21-186-85.compute-1.amazonaws.com"),
                Port = int.Parse(configuration.GetValue<string>("POSTGRES_PORT", "5432")),
                SslMode = SslMode.Prefer,
                Username = configuration.GetValue<string>("POSTGRES_USERNAME", "fhzmnoqgvnyhyf"),
                Password = configuration.GetValue<string>("POSTGRES_PASSWORD", "924481d786611181e088becf789451da49ff11cd98695bd78d12358c9ae2ab52"),
                Database = configuration.GetValue<string>("POSTGRES_DB_NAME", "d7b8j75lpk5l8e"),
                TrustServerCertificate = true
            };

            return  connectionStringBuilder.ConnectionString;
        }
    }
}
