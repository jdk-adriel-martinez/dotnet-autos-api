using Autos.Api;

namespace Autos.Tests.Tests;

public class SmokeTests
{
    [Fact]
    public void ApiAssembly_IsAvailableToTests()
    {
        Assert.Equal("Autos.Api.Program", typeof(Program).FullName);
    }
}
