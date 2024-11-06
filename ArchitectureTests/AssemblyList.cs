using System.Reflection;

namespace ArchitectureTests;

internal static class AssemblyList
{
    public static string Domain = "Domain";
    public static string Application = "Application";
    public static string Infrastructure = "Infrastructure";
    public static string EndPoints = "EndPoints";
    public static string Common = "Common";

    public static readonly IEnumerable<Assembly> ProjectAssembly = AppDomain.CurrentDomain.GetAssemblies().ToList();
    public static IEnumerable<Assembly> DomainAssemlby => GetProjectCategory(Domain);
    public static IEnumerable<Assembly> ApplicationAssembly => GetProjectCategory(Application);
    public static IEnumerable<Assembly> InfrastructureAssembly => GetProjectCategory(Infrastructure);
    public static IEnumerable<Assembly> EndPointsAssembly => GetProjectCategory(EndPoints);

    private static IEnumerable<Assembly> GetProjectCategory(string projectCategory)
    {
        return ProjectAssembly.Where(a => a.GetName().Name!.EndsWith(projectCategory, StringComparison.InvariantCultureIgnoreCase));

    }
}
