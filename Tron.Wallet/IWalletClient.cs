namespace Tron.Wallet {
    using Accounts;
    using Google.Protobuf;
    using Grpc.Core;
    using Protocol;

    public interface IWalletClient {
        Wallet.WalletClient GetProtocol();

        WalletSolidity.WalletSolidityClient GetSolidityProtocol();

        ITronAccount GenerateAccount();

        ITronAccount GetAccount(string privateKey);

        ByteString ParseAddress(string address);

        Metadata GetHeaders();
    }
}