using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Domain.Repositories;
using Talabat.Repository;
using Microsoft.Extensions.DependencyInjection;
using Talabat.Domain;
using Talabat.Domain.Services;
using Talabat.Services;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public  static IServiceCollection  AddApplicationServices (this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfiles));
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors = ActionContext.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };

            });
            Services.AddScoped<IBasketRepository,BasketRepository>();
            Services.AddScoped<IUnitOfWork,UnitOfWork>();   
            Services.AddScoped<IOrderService,OrderService>();   



            return Services;
        }

    }
}
 