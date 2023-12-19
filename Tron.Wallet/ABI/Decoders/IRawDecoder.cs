namespace Tron.Wallet.ABI.Decoders {
    public interface ICustomRawDecoder<T> {
        T Decode(byte[] output);
    }
}