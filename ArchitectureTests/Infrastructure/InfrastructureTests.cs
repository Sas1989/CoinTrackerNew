using Common.Application.Installers;
using Common.Domain.DomainEntity;
using Common.Infrastructure.ServicesInstallers;
using NetArchTest.Rules;
using Xunit;

namespace ArchitectureTests.Infrastructure;

public class InfrastructureTests
{
    private static string Installer = "Installer";
    [Fact]
    public void InfrastructureProject_Should_HaveInfrastructureSufix()
    {
        var failingTypes = TestHelper.GetTypesInProjectWithoutPostFix(AssemblyList.InfrastructureAssembly, typeof(IApplicationInstaller), AssemblyList.Infrastructure);

        Assert.Empty(failingTypes);
    }

    [Fact]
    public void Installers_Should_Be_Sealed()
    {
        var result = TestHelper.AreSealed(AssemblyList.InfrastructureAssembly, typeof(IApplicationInstaller));

        Assert.True(result);
    }

    [Fact]
    public void Installers_Should_HaveInstallerEventPostFix()
    {
        var result = TestHelper.EndWithPostFix(AssemblyList.InfrastructureAssembly, typeof(IApplicationInstaller), Installer);

        Assert.True(result);
    }

    [Fact]
    public void ServiceInstallers_Should_HaveInstallerEventPostFix()
    {
        var result = TestHelper.EndWithPostFix(AssemblyList.InfrastructureAssembly, typeof(IServiceInstaller), Installer);

        Assert.True(result);
    }

    [Fact]
    public void ServiceInstallers_Should_Be_Sealed()
    {
        var result = TestHelper.AreSealed(AssemblyList.InfrastructureAssembly, typeof(IServiceInstaller));

        Assert.True(result);
    }
}
