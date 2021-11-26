# IncludeNestedControllers
 Tells ASP .NET Core (and 5.0 and 6.0) to include controllers that are nested inside other classes. By default, controllers like these will be ignored, which means that you can't normally do this:
 
 ```
 public static class PlaceOrder
 {
     public class Controller : ApiController
     {
     
     }
 }
 ```
 
 Well now you can! Simply install the Nuget package and then, in your Startup.cs file, add:
 
 ```
public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(includePublicNestedControllers: true);

//...
        }
```
