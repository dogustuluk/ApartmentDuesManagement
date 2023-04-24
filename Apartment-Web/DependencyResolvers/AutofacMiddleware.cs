using Autofac;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Apartment_Web.DependencyResolvers
{
    public class AutofacMiddleware
    {
        private readonly RequestDelegate _next;

        public AutofacMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILifetimeScope lifetimeScope)
        {
            using (var scope = lifetimeScope.BeginLifetimeScope(builder =>
            {
                builder.RegisterType<ApartmentDuesManagementContext>()
                    .AsSelf()
                    .InstancePerLifetimeScope();
            }))
            {
                await _next(context);
            }
        }
    }
}
