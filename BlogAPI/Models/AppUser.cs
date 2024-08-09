using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class AppUser : IdentityUser
    {
     
            public long IdNumber { get; set; }


            public bool Gender { get; set; }

            [Required]
            [Range(-4000, 2100)]
            public int BirthDate { get; set; }

            //public byte Status { get; set; }

            [NotMapped]
            [StringLength(100, MinimumLength = 6)]
            public string Password { get; set; } = "";

            [NotMapped]
            [Compare(nameof(Password))]
            public string ConfirmPassword { get; set; } = "";
        


    }
    public class Member
    {
        [Key]
        public string Id { get; set; } = "";

        [StringLength(100, MinimumLength = 1)]
        [Column(TypeName = "varchar(100)")]
        public string FullName { get; set; } = "";

        [ForeignKey(nameof(Id))]
        public AppUser? AppUser { get; set; }

        public List<MemberEntry>? MemberEntries { get; set; }

        


      
    }


}
