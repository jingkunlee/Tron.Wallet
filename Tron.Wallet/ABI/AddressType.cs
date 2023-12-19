namespace Tron.Wallet.ABI {
    using Decoders;
    using Encoders;

    public class AddressType : ABIType {
        public AddressType() : base("address") {
            //this will need to be only a string type one, converting to hex
            Decoder = new AddressTypeDecoder();
            Encoder = new AddressTypeEncoder();
        }
    }
}