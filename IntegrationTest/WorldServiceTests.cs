using Nethereum.Contracts;
using Nethereum.Mud.Contracts.World;
using Nethereum.Mud.EncodingDecoding;
using Nethereum.Mud.TableRepository;
using System.Diagnostics;
using Nethereum.Mud.Contracts.Core.StoreEvents;
using Nethereum.Mud.Contracts.World.Systems.RegistrationSystem.ContractDefinition;
using Nethereum.Mud.Contracts.World.Tables;
using Nethereum.Mud.Contracts.Store.Tables;
using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Mud.Contracts.World.Systems.AccessManagementSystem;
using NethMud.Contracts.NethMudTest.Tables;

namespace Nethereum.Mud.IntegrationTests
{
    //This tests are using a vanilla world contract (using vanilla template) deployed to anvil
    //and this configuration
    //is used for the tests
    /*
      export default defineWorld({
           tables: {
             Counter: {
               schema: {
                 value: "uint32",
               },
               key: [],
             },
             Item:{
               schema:{
                 id:"uint32",
                 price:"uint32",
                 name:"string",
                 description:"string",
                 owner:"string",
               },
               key:["id"]
             }
           },
         });
     */
    public class WorldServiceTests
    {
        public const string WorldAddress = "0x77a881a6239b77a204491db17632908ad1f76376";
        public const string WorldUrl = "http://localhost:8545";
        //using default anvil private key
        public const string OwnerPK = "0xac0974bec39a17e36ba4a6b4d238ff944bacb478cbed5efcae784d7bf4f2ff80";
        public const string UserPK = "0x59c6995e998f97a5a0044966f0945389dc9e86dae88c7a8412f4603b6b78690d";
        public const string UserAccount = "0x70997970C51812dc3A010C7d01b50e0d17dc79C8";

        public WorldService GetWorldService()
        {
            var web3 = new Nethereum.Web3.Web3(new Nethereum.Web3.Accounts.Account(OwnerPK), WorldUrl);
            return new WorldService(web3, WorldAddress);
        }

        public IWeb3 GetWeb3()
        {
            return new Nethereum.Web3.Web3(new Nethereum.Web3.Accounts.Account(OwnerPK), WorldUrl);
        }   

        public WorldService GetUserWorldService()
        {
            var web3 = new Nethereum.Web3.Web3(new Nethereum.Web3.Accounts.Account(UserPK), WorldUrl);
            return new WorldService(web3, WorldAddress);
        }

        public async Task ShouldReturnRightVersion()
        {
            var worldService = GetWorldService();
            var storeVersion = await worldService.StoreVersionQueryAsStringAsync();
            Console.WriteLine(storeVersion);
        }


        [Function("increment")]
        public class IncrementFunction : FunctionMessage
        {
        }

        public async Task ShouldGetRecord()
        {
            try
            {
                var worldService = GetWorldService();
                var receipt = await worldService.ContractHandler.SendRequestAndWaitForReceiptAsync(new IncrementFunction());
            }
            catch (SmartContractCustomErrorRevertException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        public async Task ShouldGetRecordUsingTable()
        {
            var worldService = GetWorldService();
            // var receipt = await worldService.ContractHandler.SendRequestAndWaitForReceiptAsync(new IncrementFunction());
            var record = await worldService.GetRecordTableQueryAsync<CounterTableRecord, CounterTableRecord.CounterValue>(new CounterTableRecord());
            Console.WriteLine("record value: " + record.Values.Value);
        }

        public async Task ShouldGetAllChanges()
        {
            var web3 = GetWeb3();
            var storeLogProcessingService = new StoreEventsLogProcessingService(web3, WorldAddress);
            var inMemoryStore = new InMemoryTableRepository();
            var tableId = new CounterTableRecord().ResourceIdEncoded;
            await storeLogProcessingService.ProcessAllStoreChangesAsync(inMemoryStore, null, null, CancellationToken.None);
            var results = await inMemoryStore.GetTableRecordsAsync<CounterTableRecord>(tableId);

            var resultsSystems = await inMemoryStore.GetTableRecordsAsync<SystemsTableRecord>(new SystemsTableRecord().ResourceIdEncoded);
            
            foreach (var result in resultsSystems)
            {
                Debug.WriteLine(ResourceEncoder.Decode(result.Keys.SystemId).Name);
                Debug.WriteLine(ResourceEncoder.Decode(result.Keys.SystemId).Namespace);
            }

            var resultsAccess = await inMemoryStore.GetTableRecordsAsync<ResourceAccessTableRecord>(new ResourceAccessTableRecord().ResourceIdEncoded);
            
            foreach (var result in resultsAccess)
            {
                Debug.WriteLine(ResourceEncoder.Decode(result.Keys.ResourceId).Name);
                Debug.WriteLine(result.Keys.Caller);
                Debug.WriteLine(result.Values.Access);
            }


            //the world factory is the owner of the store and world namespaces
            var namespaceOwner = await inMemoryStore.GetTableRecordsAsync<NamespaceOwnerTableRecord>(new NamespaceOwnerTableRecord().ResourceIdEncoded);
            
            foreach (var result in namespaceOwner)
            {
                Debug.WriteLine(ResourceEncoder.Decode(result.Keys.NamespaceId).Name);
                Debug.WriteLine(ResourceEncoder.Decode(result.Keys.NamespaceId).Namespace);
                Debug.WriteLine(result.Values.Owner);
            }

            var itemTableResults = await inMemoryStore.GetTableRecordsAsync<ItemTableRecord>(new ItemTableRecord().ResourceIdEncoded);
            
            foreach (var result in itemTableResults)
            {
                Console.WriteLine(result.Keys.Id);
                Console.WriteLine(result.Values.Value);
            }

        }
    }
}