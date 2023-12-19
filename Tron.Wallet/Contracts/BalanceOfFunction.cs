namespace Tron.Wallet.Contracts {
    using ABI.FunctionEncoding.Attributes;

    [Function("balanceOf", "uint256")]
    public class BalanceOfFunction : FunctionMessage {
        [Parameter("address", "owner", 1)]
        public string Owner { get; set; }

    }

    [FunctionOutput]
    public class BalanceOfFunctionOutput : IFunctionOutputDTO {
        [Parameter("uint256", 1)]
        public long Balance { get; set; }
    }
}
