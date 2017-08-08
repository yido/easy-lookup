# Easy-Lookup-Management
Easy way to retrieve and use lookups/dropdown across your application. 


        Bind<Models.UserType>().To<Enums.UserType>();
        Map<Enums.UserType>().SetSchemaName("Account").SetTableName("UserType").SetCodeColumn("Type").SetNameColumn("Type").SetIdColumn("ID");


Lets cache our lookups on the first load.

     public ICollection<LookUpModel> GetLookups<TEnum>() where TEnum : IConvertible
        {
            var enumDescriptor = BindingManager.GetEnumDescriptor<TEnum>();

            if (_lookUpModels == null) CacheAllLookups(); 

        }

Lets Build Simple plain SQL Query by using EnumDescriptors 

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

Set your constant values for latter use.

         BLL.UserType.Constants.ADMIN = Program.LookupKernel.GetLookup(Enums.UserType.Admin).ID;      
         
 There you go!        


