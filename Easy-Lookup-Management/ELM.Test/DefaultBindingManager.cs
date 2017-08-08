using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy_Lookup_Management.Binding;
using Easy_Lookup_Management.Enums;
using Easy_Lookup_Management.Services;

namespace Test
{
    class DefaultBindingManager : BindingManager
    {
        public override void Load()
        {
            Bind<Model.UserType>().To<Enum.UserType>();
            Map<Enum.UserType>().SetSchemaName("Account").SetTableName("UserType").SetCodeColumn("Type").SetNameColumn("Type").SetIdColumn("ID");
        }
    }

    public class TestContext : DbContext, ILookupRepository
    {
        private IEnumerable<LookUpModel> _lookUpModels;

        public TestContext(string connectionstring) : base(connectionstring)
        {

        }

        #region Lookup Models DbSets
         
        public DbSet<Model.UserType> UserTypes { get; set; }


        #endregion


        #region Implementation of ILookupRepository

        public IQueryable<TType> Get<TType>() where TType : class
        {
            return Set<TType>();
        }

        public ICollection<LookUpModel> GetLookups<TEnum>(EnumDescriptor enumDescriptor) where TEnum : IConvertible
        {
            return _lookUpModels.Where(s => s.SchemaName == enumDescriptor.SchemaName && s.TableName == enumDescriptor.TableName).ToList();
        }

        public ICollection<LookUpModel> GetLookups<TEnum>() where TEnum : IConvertible
        {
            var enumDescriptor = BindingManager.GetEnumDescriptor<TEnum>();

            if (_lookUpModels == null) CacheAllLookups();

            return _lookUpModels != null
                ? _lookUpModels.Where(
                    s => s.SchemaName == enumDescriptor.SchemaName && s.TableName == enumDescriptor.TableName).ToList()
                : null;

        }

        private void CacheAllLookups()
        {
            var query = "";
            foreach (var enumDiscriptor in BindingManager.EnumDescriptors.Values)
            {
                query += String.Format(" Select '{3}' SchemaName,'{4}' TableName , {0} ID,{1} Name,{2} Code from {3}.{4} ",
                    enumDiscriptor.ID,
                    enumDiscriptor.Name,
                    enumDiscriptor.Code,
                    enumDiscriptor.SchemaName,
                    enumDiscriptor.TableName);
                query += (BindingManager.EnumDescriptors.Values.Last() == enumDiscriptor) ? "" : "UNION";
            }

            var data = Database.SqlQuery<LookUpModel>(query);  
            _lookUpModels = data.ToList();
        }

        #endregion

        public BindingManager BindingManager { get; set; }
    }
}
