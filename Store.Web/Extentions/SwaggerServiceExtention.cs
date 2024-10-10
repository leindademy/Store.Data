using Microsoft.OpenApi.Models;

namespace Store.Web.Extentions
{
    public static class SwaggerServiceExtention
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Store Api ",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Route Academy",
                        Email = "RouteAcademy@gmail.com",
                        Url = new Uri("https://eg.linkedin.com/company/route-academy?trk=public_profile_experience-item_result-card_image-click")
                    }
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header, // In header request
                    Type = SecuritySchemeType.ApiKey, // Type of APIKey
                    Scheme = "Bearer", // Authorization type
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme 
                    }
                };
                options.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {securitySchema , new[] {"Bearer"}}
                };

                options.AddSecurityRequirement(securityRequirement);
            });

            return services;


        }
    }
}
