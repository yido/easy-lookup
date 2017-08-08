using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Model
{
    [Table("UserType")]
    public class UserType
    {
        /// <summary>
        /// Constant to represent the possible User Types
        /// </summary>
        public class Constants
        {
            public static int READER;
            public static int EDITOR;
            public static int ADMIN;
            public static int SUPER_ADMINISTRATOR;
            public static int FINANCE; 
        }

        [Key]
        [Column("ID")]
        public int UserTypeID { get; set; }
        public string Type { get; set; }
        [NotMapped]
        public string UserTypeCode { get { return Type; } }
        [NotMapped]
        public string Name { get { return Type; } }
        [NotMapped]
        public string Description { get { return Type; } }
        [NotMapped]
        public bool IsActive { get { return true; } }
    }
}
