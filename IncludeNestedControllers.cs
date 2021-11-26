namespace Microsoft.AspNetCore.Mvc.Controllers;

/// <summary>
/// Discovers controllers whose access modifiers are public but nested inside other public classes, which the default
/// <see cref="ControllerFeatureProvider"/> would miss out.
/// </summary>
public class NestedControllerFeatureProvider : ControllerFeatureProvider
{
    private const string ControllerTypeNameSuffix = "Controller";
    private readonly bool _includeNestedControllers;

    public NestedControllerFeatureProvider(bool includeNestedControllers = false)
    {
        _includeNestedControllers = includeNestedControllers;
    }

    protected override bool IsController(TypeInfo typeInfo)
    {
        if (!_includeNestedControllers)
            return base.IsController(typeInfo);

        if (!typeInfo.IsClass)
        {
            return false;
        }

        if (typeInfo.IsAbstract)
        {
            return false;
        }

        // IsPublic returns false for nested classes, regardless of visibility modifiers.
        // We wish to include nested controllers, so we only return false if IsPublic is false AND IsNestedPublic is false.
        if (!typeInfo.IsPublic && !typeInfo.IsNestedPublic)
        {
            return false;
        }

        if (typeInfo.ContainsGenericParameters)
        {
            return false;
        }

        if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
        {
            return false;
        }

        if (!typeInfo.Name.EndsWith(ControllerTypeNameSuffix, StringComparison.OrdinalIgnoreCase) &&
            !typeInfo.IsDefined(typeof(ControllerAttribute)))
        {
            return false;
        }

        return true;
    }
}