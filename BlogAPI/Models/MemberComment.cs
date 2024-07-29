using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class MemberComment
    { 
        // Comment classındaki parent id null ise yani yorumun yorumu değil; gönderiye atılmış bir yorumsa buraya kaydedilmeli

        public string MemberId { get; set; } = "";

        public int CommentId { get; set; }

        [ForeignKey(nameof(MemberId))]  
        public AppUser? AppUser { get; set; }

        [ForeignKey(nameof(CommentId))]
        public Comment? Comment { get; set; }
    }
}
