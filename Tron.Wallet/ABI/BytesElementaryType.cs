namespace Tron.Wallet.ABI {
    using Decoders;
    using Encoders;

    public class BytesElementaryType : ABIType {
        public BytesElementaryType(string name, int size) : base(name) {
            Decoder = new BytesElementaryTypeDecoder(size);
            Encoder = new BytesElementaryTypeEncoder(size);
        }
    }
}