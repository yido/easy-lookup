using System.Threading.Tasks;
using Easy_Lookup_Management.Enums.Attributes;

namespace Test.Enum
{
    [CodeColumn("UserTypeCode")]
    public enum UserType
    {
        [TableCode("System Admin")]
        System_Admin
    }
}
