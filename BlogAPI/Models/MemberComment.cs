using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class MemberComment
    { 
        public int Id { get; set; } 
        
        public string MemberId { get; set; } = "";

        public int CommentId { get; set; }

        [ForeignKey(nameof(MemberId))]  
        public AppUser? AppUser { get; set; }

        [ForeignKey(nameof(CommentId))]
        public Comment? Comment { get; set; }
    }
}
