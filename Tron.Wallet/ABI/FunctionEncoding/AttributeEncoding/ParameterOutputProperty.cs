using System.Collections.Generic;
using System.Reflection;

namespace Tron.Wallet.ABI.FunctionEncoding.AttributeEncoding {
    public class ParameterOutputProperty : ParameterOutput {
        public PropertyInfo PropertyInfo { get; set; }

        public List<ParameterOutputProperty> ChildrenProperties { get; set; }
    }
}