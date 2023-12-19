namespace Tron.Wallet.ABI {
    using Decoders;
    using Encoders;

    public class StringType : ABIType {
        public StringType() : base("string") {
            Decoder = new StringTypeDecoder();
            Encoder = new StringTypeEncoder();
        }

        public override int FixedSize => -1;
    }
}