using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Mud.Contracts.Core.Tables;
// using Nethereum.Mud.IntegrationTest.MudTest.Tables;
using NethMud.Contracts.NethMudTest.Tables;
using Nethereum.Web3;

namespace Nethereum.Mud.IntegrationTest.MudTest
{
    public class MudTestTableServices : TablesServices
    {
        public CounterTableService CounterTableService { get; protected set; }
        
        public MudTestTableServices(IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
            CounterTableService = new CounterTableService(web3, contractAddress);
            
            TableServices = new List<ITableServiceBase> { CounterTableService };
        }
    }
}