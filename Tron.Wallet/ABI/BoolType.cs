namespace Tron.Wallet.ABI {
    using Decoders;
    using Encoders;

    public class BoolType : ABIType {
        public BoolType() : base("bool") {
            Decoder = new BoolTypeDecoder();
            Encoder = new BoolTypeEncoder();
        }
    }
}