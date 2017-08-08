using System;

namespace Easy_Lookup_Management.Enums
{
    public interface ILookupService<TType> where TType : class
    {
        TType Get(IConvertible value);
    }
}
