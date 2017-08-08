using Easy_Lookup_Management.Services;
using Easy_Lookup_Management.Binding;
using Test.Model;

namespace Test
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //~ Set your constant values for latter use ~// 
            UserType.Constants.ADMIN = LookupKernel.GetLookup(Enum.UserType.System_Admin).ID;
        }
    }
}
