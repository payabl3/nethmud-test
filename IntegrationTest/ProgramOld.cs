// using Nethereum.Util;
// using Nethereum.Web3;
// // using Nethereum.Mud.Contracts;
// // using Nethereum.Mud.Contracts.World;
// // using Nethereum.Mud.Contracts.Core.StoreEvents;
// // using Nethereum.Mud.TableRepository;
// // using Nethereum.Mud.Contracts.World.Tables;
// // using Nethereum.Mud.Contracts.World.Systems.RegistrationSystem;
// // using Nethereum.Mud.EncodingDecoding;
// // using Nethereum.Mud.Contracts.World.Systems.RegistrationSystem.ContractDefinition;

// using Nethereum.Contracts;
// using Nethereum.Mud.Contracts.World;
// using Nethereum.Mud.EncodingDecoding;
// using Nethereum.Mud.TableRepository;
// using Nethereum.RPC.Eth.DTOs;
// // using Nethereum.XUnitEthereumClients;
// using Nethereum.Hex.HexConvertors.Extensions;
// using System.Diagnostics;
// // using Nethereum.Mud.IntegrationTest.MudTest.Tables;
// // using Nethereum.Mud.IntegrationTest.MudTest.Systems;
// // using Nethereum.Mud.IntegrationTest.MudTest.Systems.IncrementSystem;
// // using Nethereum.Mud.IntegrationTest.MudTest.Systems.IncrementSystem.ContractDefinition;
// using Nethereum.Mud.Contracts;
// using Nethereum.Mud.Contracts.Core.StoreEvents;
// using Nethereum.Mud.Contracts.World.Systems.RegistrationSystem;
// using Nethereum.Mud.Contracts.World.Systems.RegistrationSystem.ContractDefinition;
// using Nethereum.Mud.Contracts.World.Tables;
// using Nethereum.Mud.Contracts.Store.Tables;
// using Nethereum.Mud.Contracts.Core.Systems;
// using Nethereum.Mud.Contracts.Core.Namespaces;
// using System.Net;
// using Nethereum.Mud.Contracts.Store;
// using Nethereum.Mud.IntegrationTest.MudTest;
// using Nethereum.Mud.Contracts.ContractHandlers;

// using NethMud.Contracts.NethMudTest.Tables;
// using NethMud.Contracts.NethMudTest.Systems;
// using NethMud.Contracts.NethMudTest.Systems.IncrementSystem;
// using NethMud.Contracts.NethMudTest.Systems.IncrementSystem.ContractDefinition;

// // See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

// var counter = new CounterTableRecord();
// var encoded = counter.GetSchemaEncoded();
// Console.WriteLine(encoded);

// var web3 = new Web3();
// web3.TransactionManager.UseLegacyAsDefault = true;

// var random = new Random();
// //random salt in case we have an existing contract deployed
// var salt = Nethereum.Util.Sha3Keccack.Current.CalculateHash(random.Next(0, 1000000).ToString());
// var create2DeterministicDeploymentProxyService = web3.Eth.Create2DeterministicDeploymentProxyService;

//             var proxyCreate2Deployment = await create2DeterministicDeploymentProxyService.GenerateEIP155DeterministicDeploymentUsingPreconfiguredSignatureAsync();
//             var addressDeployer = await create2DeterministicDeploymentProxyService.DeployProxyAndGetContractAddressAsync(proxyCreate2Deployment);
           
//             var worldFactoryDeployerService = new WorldFactoryDeployService();
//             var worldFactoryAddresses = await worldFactoryDeployerService.DeployWorldFactoryContractAndSystemDependenciesAsync(web3, addressDeployer, salt);

//             var worldEvent = await worldFactoryDeployerService.DeployWorldAsync(web3, salt, worldFactoryAddresses);
//             var worldAddress = worldEvent.NewContract;
//             var world = new WorldNamespace(web3, worldAddress);
//             var version = await world.WorldService.StoreVersionQueryAsStringAsync();
//             // Assert.Equal("2.0.0", version);

//             var store = new StoreNamespace(web3, worldAddress);

//             var storeLogProcessingService =  store.StoreEventsLogProcessingService;
//             var inMemoryStore = new InMemoryTableRepository();
//             await storeLogProcessingService.ProcessAllStoreChangesAsync(inMemoryStore, null, null, CancellationToken.None);

//             var systemRecords = await world.Tables.SystemsTableService.GetRecordsFromRepository(inMemoryStore);

//             // Assert.True(systemRecords.ToList().Count > 0);

//             var namespaceRecords = await world.Tables.NamespaceOwnerTableService.GetRecordsFromRepository(inMemoryStore);
//             var resourceIds = await world.Tables.SystemRegistryTableService.GetRecordsFromRepository(inMemoryStore);

//             var mudTest = new MudTestNamespace(web3, worldAddress);
//             //note this may need a wait
//             await mudTest.RegisterNamespaceRequestAndWaitForReceiptAsync();  
//             //note this may need a wait
//             var receipt =  await mudTest.Tables.BatchRegisterAllTablesRequestAndWaitForReceiptAsync();
         
//             var tables = await store.Tables.TablesTableService.GetRecordsFromLogsAsync();

//             var counterTableSchema = tables.Where(x => x.Keys.GetTableIdResource().Name == "Counter").FirstOrDefault();

//             var counterTableResourceId = counterTableSchema.Keys.GetTableIdResource();
//             // Assert.True(counterTableResourceId.IsRoot());
//             var field = counterTableSchema.Values.GetValueSchemaFields().ToList()[0];
//             // Assert.Equal("uint32", field.Type);
//             // Assert.Equal(1, field.Order);
//             // Assert.Equal("value", field.Name);

//             Console.WriteLine("field.Type" + field.Type);
//             Console.WriteLine("field.Order" + field.Order);
//             Console.WriteLine("field.Name" + field.Name);
