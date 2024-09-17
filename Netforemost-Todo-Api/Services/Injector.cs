using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Netforemost_Todo_Api.Services
{
    public static class Injector
    {
        public static void AddAllServices(this IServiceCollection services, Assembly assembly)
        {
            var baseServiceType = typeof(BaseService<>);

            var typesToRegister = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType != null
                    && t.BaseType.IsGenericType
                    && t.BaseType.GetGenericTypeDefinition() == baseServiceType);

            foreach (var type in typesToRegister)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault(i =>
                    i.IsTypeDefinition);

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
            }
        }
    }
}
