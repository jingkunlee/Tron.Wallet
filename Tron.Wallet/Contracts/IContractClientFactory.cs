namespace Tron.Wallet.Contracts {
    public interface IContractClientFactory {
        IContractClient CreateClient(ContractProtocol protocol);
    }
}
