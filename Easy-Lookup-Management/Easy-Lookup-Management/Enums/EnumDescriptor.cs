namespace Easy_Lookup_Management.Enums
{
    public class EnumDescriptor
    {
        private string _name;
        private string _code;
        private string _tableName;
        private string _id;
        private string _schemaName;

        public string SchemaName
        {
            get { return _schemaName??"dbo"; }
        }

        public string TableName
        {
            get { return _tableName; }
        }

        public string ID
        {
            get { return _id?? TableName + "ID"; }
        }

        public string Name
        {
            get { return _name ?? "Name"; }
        }

        public string Code
        {
            get { return _code?? TableName + "Code"; }
        }

        public EnumDescriptor SetTableName(string tableName)
        {
            _tableName = tableName;
            return this;
        }
        public EnumDescriptor SetIdColumn(string columnName)
        {
            _id = columnName;
            return this;
        }
        public EnumDescriptor SetNameColumn(string columnName)
        {
            _name = columnName;
            return this;
        }
        public EnumDescriptor SetCodeColumn(string columnName)
        {
            _code = columnName;
            return this;
        }
        public EnumDescriptor SetSchemaName(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }

    }
}
