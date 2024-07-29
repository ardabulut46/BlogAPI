using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class UserLikeDislike
    {
        public string MemberId { get; set; } = "";

        public int EntryId { get; set; }

        public bool Like { get; set; }   // if true the post has been liked else it's disliked 

        [ForeignKey(nameof(MemberId))]
        public AppUser? AppUser { get; set; }

        [ForeignKey(nameof(EntryId))]   
        public Entry? Entry { get; set; }

        
    }
}
