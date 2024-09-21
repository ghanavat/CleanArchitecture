using System.Reflection;
using CleanArchitecture.Infrastructure;
using SampleApi.Controllers;

namespace SampleApi.Tests;

#pragma warning disable CS1591
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_ShouldFail_When_InfrastructureAssembly_ReferencedOutsideOfProgram()
    {
        /* Fetching the name of the assembly that we want to test it's not used outside the Program class */
        var infrastructureAssembly = Assembly.GetAssembly(typeof(ServiceRegistration))!.GetName().Name;
        
        /* List of assemblies referenced by SampleApi project */
        var hasInfrastructureReferencedInSampleApi = Assembly.GetAssembly(typeof(SampleController))!
            .GetReferencedAssemblies().Where(x => x.Name == infrastructureAssembly);

        var xxx = Assembly.GetAssembly(typeof(SampleController))!.GetTypes();
    }
}
