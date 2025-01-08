namespace Burpless.Services;

internal class SimpleServiceProvider : IServiceProvider
{
    public object GetService(Type serviceType)
    {
        return Resolve(serviceType, new Dictionary<Type, object>(), new Stack<Type>());
    }

    private object Resolve(Type type, Dictionary<Type, object> created, Stack<Type> activating)
    {
        if (created.TryGetValue(type, out var value))
        {
            return value;
        }

        ValidateResolution(type, activating);

        activating.Push(type);

        created[type] = value = Create(type, created, activating);

        activating.Pop();

        return value;
    }

    private object Create(Type type, Dictionary<Type, object> created, Stack<Type> activating)
    {
        var constructor = type
            .GetConstructors()
            .OrderByDescending(x => x.GetParameters().Length)
            .FirstOrDefault();

        if (constructor == null)
        {
            throw new InvalidOperationException($"Cannot resolve {type}, no public constructors found");
        }

        var parameters = constructor
            .GetParameters()
            .Select(x => Resolve(x.ParameterType, created, activating))
            .ToArray();

        return Activator.CreateInstance(type, parameters)!;
    }

    private void ValidateResolution(Type type, Stack<Type> activating)
    {
        if (activating.Contains(type))
        {
            throw new InvalidOperationException($"Cannot resolve {type} as it would result in a circular dependency");
        }

        if (type.IsInterface || type.IsAbstract)
        {
            throw new InvalidOperationException($"Type {type} cannot be constructed");
        }
    }
}
