using System;
using System.Collections.Generic;

namespace Easy_Lookup_Management.Enums
{
    public interface ILookupKernel
    {
        TEnum GetEnum<TEnum>(int id) where TEnum : IConvertible;
        ICollection<LookUpModel> GetLookups<TEnum>() where TEnum : IConvertible;
        LookUpModel GetLookup<TEnum>(TEnum value) where TEnum : IConvertible;
        ILookupService<TType> From<TType>() where TType : class;
    }
}
