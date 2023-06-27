using System.Reflection;

namespace Workflow.StaticMethod;

public static class ServiceRegistrationExtensions
{
    public static void RegisterServicesInAssembly(this IServiceCollection services, string assemblyName,
        string fileEndName)
    {
        Assembly assembly = Assembly.LoadFile(assemblyName);

        foreach (var type in assembly.GetTypes().Where(x => x.IsClass && x.Name.EndsWith(fileEndName)))
            services.AddScoped(type.GetInterfaces()[0], type);
    }
}