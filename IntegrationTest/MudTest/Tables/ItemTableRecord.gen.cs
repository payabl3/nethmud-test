using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Mud;
using Nethereum.Mud.Contracts.Core.Tables;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace NethMud.Contracts.NethMudTest.Tables
{
    public partial class ItemTableService : TableService<ItemTableRecord, ItemTableRecord.ItemKey, ItemTableRecord.ItemValue>
    { 
        public ItemTableService(IWeb3 web3, string contractAddress) : base(web3, contractAddress) {}
    
        public virtual Task<ItemTableRecord> GetTableRecordAsync(uint id, BlockParameter blockParameter = null)
        {
            Console.WriteLine("id: " + id);
            var key = new ItemTableRecord.ItemKey();
            key.Id = id;
            return GetTableRecordAsync(key, blockParameter);
        }
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
