namespace Tron.Wallet {
    using Crypto;
    using Google.Protobuf;

    public static class TransactionExtension {
        public static string GetTxid(this Protocol.Transaction transaction) {
            return transaction.RawData.ToByteArray().ToSHA256Hash().ToHex();
        }
    }
}
