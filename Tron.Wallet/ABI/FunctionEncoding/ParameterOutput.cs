using Tron.Wallet.ABI.Model;

namespace Tron.Wallet.ABI.FunctionEncoding {
    public class ParameterOutput {
        public Parameter Parameter { get; set; }
        public int DataIndexStart { get; set; }
        public object Result { get; set; }

    }
}