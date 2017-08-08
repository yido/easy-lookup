using System;
using System.Collections.Generic;
using System.Linq;
using Easy_Lookup_Management.Enums;
using Easy_Lookup_Management.Enums.Attributes;

namespace Easy_Lookup_Management.Binding
{
    public abstract class BindingManager
    {
        #region Dictionary
        protected Dictionary<Type, Type> _bindings;
        protected Dictionary<Type, EnumDescriptor> _enumDescription;  
        #endregion

        #region Methods
        public abstract void Load();

        protected BindType<TModelType> Bind<TModelType>() where TModelType : class
        {
            return new BindType<TModelType>(this);
        }

        protected EnumDescriptor Map<TEnum>() where TEnum:IConvertible
        {

            return GetEnumDescriptor<TEnum>();
        }

        public Type Get<TType>()
        {
            return Bindings.SingleOrDefault(m => m.Key == typeof (TType)).Value;
        }
        #endregion

        public EnumDescriptor GetEnumDescriptor<TEnum>()
        {
            var enumDescriptor = EnumDescriptors.SingleOrDefault(m => m.Key == typeof (TEnum)).Value;

            string tablename;
            var attribute = (CodeColumnAttribute)Attribute.GetCustomAttribute(typeof(TEnum), typeof(CodeColumnAttribute));
            if (attribute != null)
            {
                tablename = attribute.CodeColumn;
                tablename = tablename.Substring(0, tablename.Length - 4);
            }
            else
            {
                tablename = typeof (TEnum).Name;
                tablename = tablename.Substring(0, tablename.Length - 1);
            }



            if(enumDescriptor == null)
            {
                enumDescriptor = (new EnumDescriptor()).SetTableName(tablename);
                EnumDescriptors.Add(typeof(TEnum),enumDescriptor);
            }
            return enumDescriptor;
        }
        #region Properties
        public Dictionary<Type,Type> Bindings
        {
            get { return _bindings ?? (_bindings = new Dictionary<Type, Type>()); }
        }

        public Dictionary<Type,EnumDescriptor> EnumDescriptors
        {
            get { return _enumDescription ?? (_enumDescription = new Dictionary<Type, EnumDescriptor>()); }
        } 

        #endregion
    }
}
