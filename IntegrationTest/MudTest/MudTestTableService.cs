using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NethMud.Contracts.Core.Tables;
using NethMud.IntegrationTest.MudTest.Tables;
using Nethereum.Web3;

namespace NethMud.IntegrationTest.MudTest
{
    public class MudTestTableServices : TablesServices
    {
        public CounterTableService CounterTableService { get; protected set; }
        public ItemTableService ItemTableService { get; protected set; }
        public ConfigTableService ConfigTableService { get; protected set; }

        public MudTestTableServices(IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
            CounterTableService = new CounterTableService(web3, contractAddress);
            ItemTableService = new ItemTableService(web3, contractAddress);
            ConfigTableService = new ConfigTableService(web3, contractAddress);

            TableServices = new List<ITableServiceBase> { CounterTableService, ItemTableService, ConfigTableService };
        }
    }
}