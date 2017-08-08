using System;
using System.Linq;

namespace Easy_Lookup_Management.Binding
{
    public class BindType<TModelType> where TModelType: class
    {
        private readonly BindingManager _bindingManager;

        public BindType(BindingManager bindingManager)
        {
            _bindingManager = bindingManager;
        }
        public void To<TType>() where TType : struct, IComparable, IConvertible
        {
            if (_bindingManager.Bindings.Any(m => m.Key == typeof(TModelType)))
                throw new Exception("Multiple Type bindings.");
            _bindingManager.Bindings.Add(typeof(TModelType), typeof(TType));
        }
    }
}
