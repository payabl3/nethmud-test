using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Mud;
using Nethereum.Mud.Contracts.Core.Tables;
using Nethereum.Web3;
using System.Collections.Generic;
using System.Numerics;

namespace NethMud.Contracts.NethMudTest.Tables
{
    public partial class ItemTableService : TableService<ItemTableRecord, ItemTableRecord.ItemKey, ItemTableRecord.ItemValue>
    { 
        public ItemTableService(IWeb3 web3, string contractAddress) : base(web3, contractAddress) {}
    }
    
    public partial class ItemTableRecord : TableRecord<ItemTableRecord.ItemKey, ItemTableRecord.ItemValue> 
    {
        public ItemTableRecord() : base("nethmudtest", "Item")
        {
        
        }

        public partial class ItemKey
        {
            [Parameter("uint32", "id", 1)]
            public virtual uint Id { get; set; }
        }

        public partial class ItemValue
        {
            [Parameter("uint32", "value", 1)]
            public virtual uint Value { get; set; }          
        }
    }
}
