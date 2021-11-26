namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for the service collection.
/// </summary>
public static class NestedControllersServiceCollectionExtensions
{
    /// <summary>
    /// Adds services for controllers to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="includePublicNestedControllers">Whether or not to include controllers that are public and nested
    /// inside another class.</param>
    /// <returns>An MVC builder.</returns>
    public static IMvcBuilder AddControllers(this IServiceCollection services, bool includePublicNestedControllers)
    {
        return services
            .AddMvc()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new NestedControllerFeatureProvider(includePublicNestedControllers));
            });
    }
}