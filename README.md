# Tron.Wallet

## Get Started
### NuGet 

You can run the following command to install the `Tron.Wallet` in your project.

```
PM> Install-Package Tron.Wallet
```

### Configuration

```c#
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConsoleApp1;

using Tron.Wallet;

public record TronRecord(IServiceProvider ServiceProvider, ITronClient? TronClient, IOptions<TronNetOptions>? Options);

public static class TronServiceExtension {
    private static IServiceProvider AddTronNet() {
        IServiceCollection services = new ServiceCollection();
        services.AddTronNet(x => {
            x.Network = TronNetwork.MainNet;
            x.Channel = new GrpcChannelOption { Host = "grpc.trongrid.io", Port = 50051 };
            x.SolidityChannel = new GrpcChannelOption { Host = "grpc.trongrid.io", Port = 50052 };
            x.ApiKey = "80a8b20f-a917-43a9-a2f1-809fe6eec0d6";
        });
        services.AddLogging();
        return services.BuildServiceProvider();
    }

    public static TronRecord GetRecord() {
        var provider = AddTronNet();
        var client = provider.GetService<ITronClient>();
        var options = provider.GetService<IOptions<TronNetOptions>>();

        return new TronRecord(provider, client, options);
    }
}

```

### Sample

#### Sample 1: Generate Address Offline

```c#
namespace ConsoleApp1;

using Tron.Wallet;

internal class Program {    
    private static void Main() {
        var tronEcKey = TronECKey.GenerateKey(TronNetwork.MainNet);
        var privateKey = tronEcKey.GetPrivateKey();
        var address = tronEcKey.GetPublicAddress();

        Console.WriteLine($"{privateKey}\t{address}");
    }
}

```

#### Sample 2: Transfer TRX/USDT and account permission update
```c#
namespace ConsoleApp1;

using Microsoft.Extensions.DependencyInjection;

using Tron.Wallet;
using Tron.Wallet.Accounts;
using Tron.Wallet.Contracts;

internal class Program {
    private static async Task Main() {
        var privateKey = "your private key here.";
        var toAddress = "your receive address here.";
        var amount = 1_000_000L;

        //transfer out trx
        var result = await TrxTransferAsync(privateKey, toAddress, amount);
        Console.WriteLine(result.TransactionId);

        //transfer out usdt
        var decimalAmount = new decimal(1);
        var transactionId = await EtherTransferAsync(privateKey, toAddress, decimalAmount, null);
        Console.WriteLine(transactionId);

        //Account permission update
        var ownerAddress = "your owner address here.";
        var r = await AccountPermissionUpdateAsync(privateKey, ownerAddress);
        Console.WriteLine(r.TransactionId);
    }


    private static async Task<dynamic> TrxTransferAsync(string privateKey, string to, long amount) {
        var record = TronServiceExtension.GetRecord();
        var transactionClient = record.TronClient?.GetTransaction();

        var account = new TronAccount(privateKey, TronNetwork.MainNet);

        var transactionExtension = await transactionClient?.CreateTransactionAsync(account.Address, to, amount)!;

        var transactionId = transactionExtension.Txid.ToStringUtf8();

        var transactionSigned = transactionClient.GetTransactionSign(transactionExtension.Transaction, privateKey);
        var returnObj = await transactionClient.BroadcastTransactionAsync(transactionSigned);

        return new { Result = returnObj.Result, Message = returnObj.Message, TransactionId = transactionId };
    }

    private static async Task<string> EtherTransferAsync(string privateKey, string toAddress, decimal amount, string? memo) {
        const string contractAddress = "TR7NHqjeKQxGTCi8q8ZY4pL8otSzgjLj6t";

        var record = TronServiceExtension.GetRecord();
        var contractClientFactory = record.ServiceProvider.GetService<IContractClientFactory>();
        var contractClient = contractClientFactory?.CreateClient(ContractProtocol.TRC20);

        var account = new TronAccount(privateKey, TronNetwork.MainNet);

        const long feeAmount = 60 * 1000000L;

        return await contractClient.TransferAsync(contractAddress, account, toAddress, amount, memo, feeAmount);
    }

    private static async Task<dynamic> AccountPermissionUpdateAsync(string privateKey, string ownerAddress) {
        var record = TronServiceExtension.GetRecord();
        var transactionClient = record.TronClient?.GetTransaction();
        var account = new TronAccount(privateKey, TronNetwork.MainNet);

        var transactionExtention = await transactionClient?.CreateAccountPermissionUpdateTransactionAsync(account.Address, ownerAddress)!;

        var transactionSigned = transactionClient.GetTransactionSign(transactionExtention.Transaction, privateKey);
        var returnObj = await transactionClient.BroadcastTransactionAsync(transactionSigned);

        return new { Result = returnObj.Result, Message = returnObj.Message, TransactionId = transactionExtention.Transaction.GetTxid() };
    }
}
```
