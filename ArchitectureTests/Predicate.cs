using NetArchTest.Rules;
using System.Reflection;

namespace ArchitectureTests;

internal static class Predicate
{
    public static PredicateList ImplementInterface<TInterface>(IEnumerable<Assembly> Assembly) 
    {
        var interfaceType = typeof(TInterface);
        return ImplementInterface(Assembly, interfaceType);
    }

    public static PredicateList ImplementInterface(IEnumerable<Assembly> Assembly, Type interfaceType)
    {
        ArgumentNullException.ThrowIfNull(Assembly);
        if (!interfaceType.IsInterface)
        {
            throw new ArgumentException(interfaceType.Name + " is not inteface");
        }
        return Types.InAssemblies(Assembly).That().ImplementInterface(interfaceType);
    }

    public static PredicateList Inherit<TClass>(IEnumerable<Assembly> Assembly) where TClass : class 
    {
        return Inherit(Assembly, typeof(TClass));
    }

    public static PredicateList Inherit(IEnumerable<Assembly> Assembly, Type TClass) 
    {
        ArgumentNullException.ThrowIfNull(Assembly);
        return Types.InAssemblies(Assembly).That().Inherit(TClass);
    }

    public static PredicateList ImplementOrInerit(IEnumerable<Assembly> assemblies, Type Ttype)
    {
        if (Ttype.IsInterface)
        {
            return ImplementInterface(assemblies, Ttype);
        }

        return Inherit(assemblies, Ttype);
    }
}
