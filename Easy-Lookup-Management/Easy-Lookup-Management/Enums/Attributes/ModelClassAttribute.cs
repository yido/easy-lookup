using System;

namespace Easy_Lookup_Management.Enums.Attributes
{
    [AttributeUsage(AttributeTargets.Enum,AllowMultiple = false)]
    public class ModelClassAttribute : Attribute
    {
        private readonly Type _modelClass;

        public ModelClassAttribute(Type modelClass)
        {
            _modelClass = modelClass;
        }

        public Type ModelClass
        {
            get { return _modelClass; }
        }
    }
}
