using System;

namespace Easy_Lookup_Management.Enums.Attributes
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    public class CodeColumnAttribute : Attribute
    {
        private readonly string _codeColumn;

        public CodeColumnAttribute(string codeColumn)
        {
            _codeColumn = codeColumn;
        }

        public string CodeColumn
        {
            get { return _codeColumn; }
        }
    }
}
