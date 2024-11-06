using Common.Domain.DomainEntity;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace ArchitectureTests.Domain;

public class DomainTests
{

    private static string DomainEvent = "DomainEvent";

    [Fact]
    public void DomainEvents_Should_Be_Sealed()
    {
        var result = TestHelper.AreSealed(AssemblyList.DomainAssemlby, typeof(IDomainEvent));

        Assert.True(result);
    }


    [Fact]
    public void DomainEvents_Should_HaveDomainEventPostFix()
    {
        var result = TestHelper.EndWithPostFix(AssemblyList.DomainAssemlby, typeof(IDomainEvent), DomainEvent);

        Assert.True(result);
    }

    [Fact]
    public void DomainProject_Should_HaveDomainSufix()
    {
        var failingTypes = TestHelper.GetTypesInProjectWithoutPostFix(AssemblyList.DomainAssemlby, typeof(Entity), "."+AssemblyList.Domain);

        Assert.Empty(failingTypes);
    }

    [Fact]
    public void Entities_Should_HavePrivatePrivateParameterlessConstructor()
    {
        var entityTypes = TestHelper.GetTypes(AssemblyList.DomainAssemlby, typeof(Entity));
        var failingTypes = new List<Type>();
        foreach(var type in entityTypes)
        {
            var constructor = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            if(constructor.Any( c=> c.IsPrivate && c.GetParameters().Length == 0))
            {
                failingTypes.Add(type);
            }
        }

        Assert.Empty(failingTypes);
    }

    [Fact]
    public void DomainShouldNotHaveOtherReference()
    {

        var result = TestHelper.NotHaveReference(AssemblyList.DomainAssemlby,
                                                 AssemblyList.ApplicationAssembly,
                                                 AssemblyList.InfrastructureAssembly,
                                                 AssemblyList.EndPointsAssembly);
        Assert.True(result);
    }
}
