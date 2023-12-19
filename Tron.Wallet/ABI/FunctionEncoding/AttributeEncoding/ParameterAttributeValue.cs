using System.Reflection;
using Tron.Wallet.ABI.FunctionEncoding.Attributes;

namespace Tron.Wallet.ABI.FunctionEncoding.AttributeEncoding {
    public class ParameterAttributeValue {
        public ParameterAttribute ParameterAttribute { get; set; }
        public object Value { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }
}