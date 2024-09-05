using NethMud.Contracts.Core.Systems;
using NethMud.IntegrationTest.MudTest.Systems.IncrementSystem;
using Nethereum.Web3;

namespace NethMud.IntegrationTest.MudTest
{
    public class MudTestSystemServices : SystemsServices
    {
        public IncrementSystemService IncrementSystemService { get; protected set; }
        public MudTestSystemServices(IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
            IncrementSystemService = new IncrementSystemService(web3, contractAddress);
            SystemServices = new List<ISystemService> { IncrementSystemService };
        }
    }
}