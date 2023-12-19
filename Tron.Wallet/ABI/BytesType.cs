namespace Tron.Wallet.ABI {
    using Decoders;
    using Encoders;

    public class BytesType : ABIType {
        public BytesType() : base("bytes") {
            Decoder = new BytesTypeDecoder();
            Encoder = new BytesTypeEncoder();
        }

        public override int FixedSize => -1;
    }
}