using System.Numerics;
using Tron.Wallet.ABI.FunctionEncoding.Attributes;

namespace Tron.Wallet.Contracts {
    [Function("transferFrom", "bool")]
    public class TransferFromFunction : FunctionMessage
    {
        [Parameter("address", "_from", 1)]
        public string From { get; set; }

        [Parameter("address", "_to", 1)]
        public string To { get; set; }

        [Parameter("uint256", "_value", 2)]
        public BigInteger TokenAmount { get; set; }
    }
}
