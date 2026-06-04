
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtlasAPI.Models
{
    [Table("TblUserLogin")]
    public class User
    {
        [Key]
        [Column("UL_PK")]
        public int Id { get; set; }

        [Column("UL_UserName")]
        public string UserName { get; set; }

        [Column("UL_Role")]
        public string Role { get; set; }

        [Column("UL_Email")]
        public string Email { get; set; }

        [Column("UL_PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("UL_Password")]
        public string Password { get; set; }

        [Column("UL_Branch")]
        public string Branch { get; set; }

        [Column("UL_CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Column("UL_IsActive")]
        public bool IsActive { get; set; }
    }

      public class LoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}