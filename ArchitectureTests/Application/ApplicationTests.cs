using Common.Application.Command;
using NetArchTest.Rules;
using Xunit;

namespace ArchitectureTests.Application;

public class ApplicationTests
{
    private static string Handler = "Handler"; 
    private static string Command = "Command"; 
    [Fact]
    public void ApplicationProject_Should_HaveApplicationSufix()
    {
        var failingTypes = TestHelper.GetTypesInProjectWithoutPostFix(AssemblyList.ApplicationAssembly, typeof(ICommandHandler<,>), AssemblyList.Application);

        Assert.Empty(failingTypes);
    }

    [Fact]
    public void CommandHandler_Should_Be_Sealed()
    {
        var result = TestHelper.AreSealed(AssemblyList.ApplicationAssembly, typeof(ICommandHandler<,>));

        Assert.True(result);
    }

    [Fact]
    public void CommandHandler_Should_HaveHandlerPostFix()
    {
        var result = TestHelper.EndWithPostFix(AssemblyList.ApplicationAssembly, typeof(ICommandHandler<,>), Handler);

        Assert.True(result);
    }

    [Fact]
    public void Command_Should_Be_Sealed()
    {
        var result = TestHelper.AreSealed(AssemblyList.ApplicationAssembly, typeof(ICommand<>));

        Assert.True(result);
    }

    [Fact]
    public void Command_Should_HaveCommandPostFix()
    {
        var result = TestHelper.EndWithPostFix(AssemblyList.ApplicationAssembly, typeof(ICommand<>), Command);

        Assert.True(result);
    }

    [Fact]
    public void Application_Should_HaveTheCorrectDomain()
    {
        var result = true;
        var application = AssemblyList.ApplicationAssembly;
        var domain = AssemblyList.DomainAssemlby
            .Where(a => !a.GetName().Name!.StartsWith(AssemblyList.Common, StringComparison.InvariantCultureIgnoreCase))
            .Select(a => a.GetName().Name)
            .ToArray();

        foreach (var assembly in application)
        {
            var assemblyName = assembly.GetName().Name;
            var subject = assemblyName!.Split('.').FirstOrDefault();
            var otherDomain = domain.Where(d => !d!.StartsWith(subject!, StringComparison.InvariantCultureIgnoreCase)).ToArray();
            result &= Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAny(otherDomain).GetResult().IsSuccessful;
        }

        Assert.True(result);
    }

    [Fact]
    public void Application_Should_HaveNoDipendcyonInfrastructureAndEndPoints()
    { 
        var result = TestHelper.NotHaveReference(AssemblyList.ApplicationAssembly, AssemblyList.InfrastructureAssembly, AssemblyList.EndPointsAssembly);
        Assert.True(result);
    }
}
