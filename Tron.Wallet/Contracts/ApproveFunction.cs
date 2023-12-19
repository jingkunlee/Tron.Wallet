using System.Numerics;
using Tron.Wallet.ABI.FunctionEncoding.Attributes;

namespace Tron.Wallet.Contracts {
    [Function("approve", "bool")]
    public class ApproveFunction : FunctionMessage
    {
        [Parameter("address", "_spender", 1)]
        public string Spender { get; set; }

        [Parameter("uint256", "_value", 2)]
        public BigInteger TokenAmount { get; set; }
    }
}
