# Project: Api.Core.DynamicController
A lightweight project, that allows you to dynamically create controllers at runtime.

# Why to use
When creating highly modular microservices - we often find that we have a large number of similar "boiler-plate" controllers.

One example is for data access.  I often have a layer of controllers that allow the same operations (Insert/Update/Delete...) for a large number of database entities.
Rather than create a seperate controller for each (or a single dynamic controller, which may result in slower performance), we can define a list of controllers and dynamically create them at startup.


# How to use

First we must define the controllers what we want to create

```csharp
private IList<ControllerCreationOptions> GetListOfObjectsToCreateControllers()
{
    return new List<ControllerCreationOptions>
    {
	new ControllerCreationOptions
	{
	    ControllerEntityType = typeof(MyClassA),
	    ControllerRouting = "api/MyClassARoute",
	    ControllerTemplateType = typeof(YourConcreteControllerClassA<>),
	},
	new ControllerCreationOptions
	{
	    ControllerEntityType = typeof(MyClassB),
	    ControllerRouting = "api/MyClassBRoute",
	    ControllerTemplateType = typeof(YourConcreteControllerClassA<>)

	},
	new ControllerCreationOptions
	{
	    ControllerEntityType = typeof(MyClassC),
	    ControllerRouting = "api/MyClassCRoute",
	    ControllerTemplateType = typeof(YourConcreteControllerClassB<>)

	}
    };
}

```

Then we simply add the list of controllers to the ConfigureServices section of the Startup.cs file.

```csharp
public void ConfigureServices(IServiceCollection services)
{

  //fetch a list of controllers you want to dynamically create at startup
  var controllersToAdd = GetListOfObjectsToCreateControllers();

  services.AddMvc
    (o =>
        o.Conventions.Add(new ControllerRouteConvention(controllersToAdd))
        //more mvc options
     )
     .ConfigureApplicationPartManager
        (m => 
            m.FeatureProviders.Add(new ControllerFeatureProvider(controllersToAdd)
            //more feature providers
      )); 
 }
```
