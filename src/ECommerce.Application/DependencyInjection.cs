using ECommerce.Application.Commands;
using ECommerce.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateProductHandler>();
            services.AddScoped<GetAllProductsHandler>();
            services.AddScoped<GetProductByIdHandler>();
            services.AddScoped<UpdateProductHandler>();
            services.AddScoped<DeleteProductHandler>();

            return services;
        }
    }
}