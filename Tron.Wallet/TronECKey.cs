﻿using System;

namespace Tron.Wallet {
    using Crypto;

    public class TronECKey {
        private readonly ECKey _ecKey;
        private string _publicAddress = null;
        private readonly TronNetwork _network = TronNetwork.MainNet;
        private string _privateKeyHex = null;
        public TronECKey(string privateKey, TronNetwork network) {
            _ecKey = new ECKey(privateKey.HexToByteArray(), true);
            _network = network;
        }

        public TronECKey(byte[] vch, bool isPrivate, TronNetwork network) {
            _ecKey = new ECKey(vch, isPrivate);
            _network = network;
        }

        internal TronECKey(ECKey ecKey, TronNetwork network) {
            _ecKey = ecKey;
            _network = network;
        }

        internal TronECKey(TronNetwork network) {
            _ecKey = new ECKey();
            _network = network;
        }

        public static TronECKey GenerateKey(TronNetwork network) {
            return new TronECKey(network);
        }

        internal byte GetPublicAddressPrefix() {
            return _network == TronNetwork.MainNet ? (byte)0x41 : (byte)0xa0;
        }

        public static string GetPublicAddress(string privateKey, TronNetwork network = TronNetwork.MainNet) {
            var key = new TronECKey(privateKey.HexToByteArray(), true, network);

            return key.GetPublicAddress();
        }

        public string GetPublicAddress() {
            if (!string.IsNullOrWhiteSpace(_publicAddress)) return _publicAddress;

            var initaddr = _ecKey.GetPubKeyNoPrefix().ToKeccakHash();
            var address = new byte[initaddr.Length - 11];
            Array.Copy(initaddr, 12, address, 1, 20);
            address[0] = GetPublicAddressPrefix();

            var hash = Base58Encoder.TwiceHash(address);
            var bytes = new byte[4];
            Array.Copy(hash, bytes, 4);
            var addressChecksum = new byte[25];
            Array.Copy(address, 0, addressChecksum, 0, 21);
            Array.Copy(bytes, 0, addressChecksum, 21, 4);

            if (_network == TronNetwork.MainNet) {
                _publicAddress = Base58Encoder.Encode(addressChecksum);
            } else {
                _publicAddress = addressChecksum.ToHex();
            }
            return _publicAddress;
        }

        public string GetPrivateKey() {
            if (string.IsNullOrWhiteSpace(_privateKeyHex)) {
                _privateKeyHex = _ecKey.PrivateKey.D.ToByteArrayUnsigned().ToHex();
            }
            return _privateKeyHex;
        }

        public byte[] GetPubKey() {
            return _ecKey.GetPubKey();
        }
    }
}
