namespace Tron.Wallet {
    using Grpc.Core;

    public interface IGrpcChannelClient {

        Channel GetProtocol();

        Channel GetSolidityProtocol();
    }
}
