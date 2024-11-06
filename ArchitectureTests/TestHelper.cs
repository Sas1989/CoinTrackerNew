using System.Reflection;
using NetArchTest.Rules;

namespace ArchitectureTests;

internal static class TestHelper
{
    public static IEnumerable<Type> GetTypes(IEnumerable<Assembly> assemblies, Type Ttype)
    {
        IEnumerable<Type> types = Predicate.ImplementOrInerit(assemblies, Ttype).GetTypes();
        return types;
    }

    public static List<Type> GetTypesInProjectWithoutPostFix(IEnumerable<Assembly> assemblies,Type Ttype, string postFix)
    {
        var types = GetTypes(assemblies, Ttype);
        var failingTypes = new List<Type>();
        foreach (var type in types)
        {
            if (!type.Assembly.GetName().Name!.EndsWith(postFix, StringComparison.InvariantCultureIgnoreCase))
            {
                failingTypes.Add(type);
            }
        }

        return failingTypes;
    }

    public static bool AreSealed(IEnumerable<Assembly> assemblies, Type Ttype)
    {
        var result = Predicate.ImplementOrInerit(assemblies, Ttype).Should().BeSealed().GetResult();

        return result.IsSuccessful;
    }

    public static bool EndWithPostFix(IEnumerable<Assembly> assemblies, Type type, string postFix)
    {
        //regular expression need to avoid generic types like Type`1
        var result = Predicate.ImplementOrInerit(assemblies, type).Should().HaveNameMatching($@"{postFix}(\`\d+)?$").GetResult();

        return result.IsSuccessful;
    }

    public static bool NotHaveReference(IEnumerable<Assembly> assemblies, params IEnumerable<Assembly>[] referencies)
    {
        var otherReferences = referencies.SelectMany(a => a).Select(a => a.GetName().Name).ToArray();

        return Types.InAssemblies(assemblies).ShouldNot().HaveDependencyOnAny(otherReferences).GetResult().IsSuccessful;
    }
}
