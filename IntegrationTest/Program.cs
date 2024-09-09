using Nethereum.Mud.Contracts.World;

namespace Nethereum.Mud.IntegrationTests {
    public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Calling Service");
        var worldServiceTests = new WorldServiceTests();
        // await worldServiceTests.ShouldGetRecordUsingTable();
        await worldServiceTests.ShouldGetAllChanges();

        Console.WriteLine("Calling World Deployment Tests");
        var worldDeploymentTests = new WorldDeploymentTests();
        await worldDeploymentTests.ShouldDeployWorldContractRegisterTablesSystemAndInteractSimplified();
        Console.WriteLine("End of Program");
    }
}
}
