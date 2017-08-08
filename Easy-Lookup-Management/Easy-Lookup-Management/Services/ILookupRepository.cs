using System;
using System.Collections.Generic;
using System.Linq;
using Easy_Lookup_Management.Binding;
using Easy_Lookup_Management.Enums;

namespace Easy_Lookup_Management.Services
{
    public interface ILookupRepository
    {
        IQueryable<TType> Get<TType>()where TType:class;
        ICollection<LookUpModel> GetLookups<TEnum>(EnumDescriptor enumDescriptor) where TEnum : IConvertible;
        ICollection<LookUpModel> GetLookups<TEnum>() where TEnum : IConvertible;
        BindingManager BindingManager { get; set; }
    }
}
