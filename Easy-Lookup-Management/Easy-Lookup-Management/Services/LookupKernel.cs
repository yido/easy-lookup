using System;
using System.Collections.Generic;
using System.Linq;
using Easy_Lookup_Management.Binding;
using Easy_Lookup_Management.Enums;
using Easy_Lookup_Management.Enums.Attributes;

namespace Easy_Lookup_Management.Services
{


    public class LookupKernel : ILookupKernel
    {
        #region Fields
        private readonly ILookupRepository _lookupRepository;
        private readonly BindingManager _bindingManager;
        #endregion

        #region Constructors

        private LookupKernel(ILookupRepository dbContext)
        {
            _lookupRepository = dbContext;
        }

        public LookupKernel(ILookupRepository dbContext, BindingManager bindingManager)
            : this(dbContext)
        {
            _bindingManager = bindingManager;
            _bindingManager.Load(); // Load Bindings
            dbContext.BindingManager = _bindingManager;
        }

        #endregion
        
        #region ILookup Kernel Implementation
        public ILookupService<TType> From<TType>() where TType : class
        {
            if(_bindingManager.Get<TType>() == null)
                throw new Exception();
            var dbSet = _lookupRepository.Get<TType>();
            return new LookupService<TType>(dbSet,_bindingManager);
        }
        
       
        public TEnum GetEnum<TEnum>(int id) where TEnum:IConvertible
        {
            var lookupModel = _lookupRepository.GetLookups<TEnum>(_bindingManager.GetEnumDescriptor<TEnum>()).SingleOrDefault(s=>s.ID == id);
            if (lookupModel != null)
                return GetEnum<TEnum>(lookupModel.Code);
            throw new Exception("Lookup does not exist for this Type, on the DataBaseTable");
        }

        private TEnum GetEnum<TEnum>(string code)
        {
            var enums = typeof(TEnum).GetMembers();
            foreach (var enumeration in enums)
            {

                var attribute = enumeration.GetCustomAttributes(typeof(TableCodeAttribute), false);
                if (attribute.Length > 0 && ((TableCodeAttribute)attribute[0]).TableCode == code)
                {
                    return (TEnum)Enum.Parse(typeof(TEnum), enumeration.Name); ;
                }

            }
            throw new Exception("Code does not exist for this enum Type");
        }

        public ICollection<LookUpModel> GetLookups<TEnum>() where TEnum: IConvertible
        {
            return _lookupRepository.GetLookups<TEnum>(_bindingManager.GetEnumDescriptor<TEnum>());
        } 
        
        public LookUpModel GetLookup<TEnum>(TEnum value) where TEnum:IConvertible
        {
            return
                _lookupRepository.GetLookups<TEnum>()
                    .SingleOrDefault(s => s.Code == value.GetCode());
        }

        #endregion
    }
}
