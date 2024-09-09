using Nethereum.Mud.Contracts.World;

namespace Nethereum.Mud.IntegrationTests {
    public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Calling Program");
        var worldServiceTests = new WorldServiceTests();
        await worldServiceTests.ShouldGetRecordUsingTable();
        
    }
}
}
