using Nethereum.Contracts;
using Nethereum.Mud.Contracts.World;
using Nethereum.Mud.TableRepository;
using Nethereum.Mud.Contracts;
using Nethereum.Mud.Contracts.Store;
using Nethereum.Mud.IntegrationTest.MudTest;

namespace Nethereum.Mud.IntegrationTests
{

    public class WorldDeploymentTests
    {
        public async Task ShouldDeployWorldContractRegisterTablesSystemAndInteractSimplified()
        {
            var worldServiceTests = new WorldServiceTests();
            var web3 = worldServiceTests.GetWeb3();
            web3.TransactionManager.UseLegacyAsDefault = true;
            var random = new Random();
            //random salt in case we have an existing contract deployed
            var salt = Util.Sha3Keccack.Current.CalculateHash(random.Next(0, 1000000).ToString());

            var create2DeterministicDeploymentProxyService = web3.Eth.Create2DeterministicDeploymentProxyService;

            var proxyCreate2Deployment = await create2DeterministicDeploymentProxyService.GenerateEIP155DeterministicDeploymentUsingPreconfiguredSignatureAsync();
            var addressDeployer = await create2DeterministicDeploymentProxyService.DeployProxyAndGetContractAddressAsync(proxyCreate2Deployment);
           
            var worldFactoryDeployerService = new WorldFactoryDeployService();
            var worldFactoryAddresses = await worldFactoryDeployerService.DeployWorldFactoryContractAndSystemDependenciesAsync(web3, addressDeployer, salt);

            var worldEvent = await worldFactoryDeployerService.DeployWorldAsync(web3, salt, worldFactoryAddresses);
            var worldAddress = worldEvent.NewContract;
            var world = new WorldNamespace(web3, worldAddress);
            var version = await world.WorldService.StoreVersionQueryAsStringAsync();

            var store = new StoreNamespace(web3, worldAddress);

            var storeLogProcessingService =  store.StoreEventsLogProcessingService;
            var inMemoryStore = new InMemoryTableRepository();
            await storeLogProcessingService.ProcessAllStoreChangesAsync(inMemoryStore, null, null, CancellationToken.None);

            var systemRecords = await world.Tables.SystemsTableService.GetRecordsFromRepository(inMemoryStore);

            var namespaceRecords = await world.Tables.NamespaceOwnerTableService.GetRecordsFromRepository(inMemoryStore);
            var resourceIds = await world.Tables.SystemRegistryTableService.GetRecordsFromRepository(inMemoryStore);

            var mudTest = new MudTestNamespace(web3, worldAddress);
            //note this may need a wait
            await mudTest.RegisterNamespaceRequestAndWaitForReceiptAsync();  
            //note this may need a wait

            var tables = await store.Tables.TablesTableService.GetRecordsFromLogsAsync();

            var counterTableSchema = tables.Where(x => x.Keys.GetTableIdResource().Name == "Counter").FirstOrDefault();
            var itemTableSchema = tables.Where(x => x.Keys.GetTableIdResource().Name == "Item").FirstOrDefault();
            try
            {
                // var deployAllResult = await mudTest.Systems.DeployAllCreate2ContractSystemsRequestAsync(addressDeployer, salt);
                // var registerAllReceipt = await mudTest.Systems.BatchRegisterAllSystemsRequestAndWaitForReceiptAsync(addressDeployer, salt);

                // await mudTest.Systems.IncrementSystemService.IncrementRequestAndWaitForReceiptAsync();

                // THIS DOES NOT DISPLAY THE CORRECT VALUES
                var counterRecord = await mudTest.Tables.CounterTableService.GetTableRecordAsync();
                var itemRecord = await mudTest.Tables.ItemTableService.GetTableRecordAsync(1);
                Console.WriteLine(itemRecord.Values.Value);
                Console.WriteLine(counterRecord.Values.Value);
            }
            catch (SmartContractCustomErrorRevertException e)
            {
                var error = mudTest.FindCustomErrorException(e);
                mudTest.HandleCustomErrorException(e);

            }
        }
    }

}
   
