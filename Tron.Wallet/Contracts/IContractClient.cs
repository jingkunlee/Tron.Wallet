namespace Tron.Wallet.Contracts {
    using Accounts;
    using Protocol;

    public interface IContractClient {
        ContractProtocol Protocol { get; }

        Task<string> TransferAsync(string contractAddress, ITronAccount ownerAccount, string toAddress, decimal amount, string memo, long feeLimit);

        Task<string> TransferFromAsync(string contractAddress, ITronAccount ownerAccount, string fromAddress, string toAddress, decimal amount, string memo, long feeLimit);

        Task<string> ApproveAsync(string contractAddress, ITronAccount ownerAccount, string spenderAddress, decimal amount, long feeLimit);

        Task<decimal> BalanceOfAsync(string contractAddress, ITronAccount ownerAccount);

        Task<TransactionExtention> CreateAccountPermissionUpdateTransactionAsync(string fromAddress, string toAddress);

    }
}
